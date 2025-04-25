using System.Collections.Generic;
using System.Threading;
using GUtils.Extensions;
using GUtils.Tasks.Sequencing.Sequencer;
using GUtilsUnity.Repositories;
using GUtilsUnity.UiFrame.Layers;
using GUtilsUnity.UiFrame.Services;
using GUtilsUnity.UiStack.Builder;
using GUtilsUnity.UiStack.Entries;
using GUtilsUnity.UiStack.UseCases;
using GUtilsUnity.Extensions;


namespace GUtilsUnity.UiStack.Services
{
    /// <inheritdoc />
    public sealed class UiStackService : IUiStackService
    {
        readonly IKeyValueRepository<object, UiStackEntry> _entriesRepository = new KeyValueRepository<object, UiStackEntry>();
        readonly ISingleRepository<object> _currentUiRepository = new SingleRepository<object>();
        readonly IListRepository<object> _currentPopupsRepository = new ListRepository<object>();
        readonly List<object> _viewStack = new();
        readonly ITaskSequencer _sequencer = new TaskSequencer();

        readonly IUiFrameService _uiFrameService;

        readonly ShowUseCase _showUseCase;
        readonly HideUseCase _hideUseCase;

        public UiStackService(IUiFrameService uiFrameService)
        {
            _uiFrameService = uiFrameService;

            _showUseCase = new ShowUseCase(
                uiFrameService,
                _entriesRepository,
                _currentUiRepository,
                _currentPopupsRepository
            );

            _hideUseCase = new HideUseCase(
                _entriesRepository,
                _currentUiRepository,
                _currentPopupsRepository,
                _viewStack
            );
        }

        public void Register(UiStackEntry entry)
        {
            Register(UiFrameLayer.Default, entry);
        }

        public void Register(UiFrameLayer layer, UiStackEntry entry)
        {
            bool idAlreadyAdded = _entriesRepository.Contains(entry.Id);

            if(idAlreadyAdded)
            {
                UnityEngine.Debug.LogError($"{nameof(UiStackEntry)} with id {entry.Id} already registered");
                return;
            }

            entry.Visible.SetVisible(visible: true, instantly: true, CancellationToken.None).FireAndForget();;
            entry.Visible.SetVisible(visible: false, instantly: true, CancellationToken.None).FireAndForget();;

            _entriesRepository.Add(entry.Id, entry);

            _uiFrameService.Register(layer, entry.Transform);
        }

        public void Unregister(UiStackEntry entry)
        {
            bool found = _entriesRepository.Remove(entry.Id);

            if (found)
            {
                entry.Visible.SetVisible(visible: false, instantly: true, CancellationToken.None).FireAndForget();
            }

            if (!entry.IsPopup)
            {
                bool hasCurrentUi = _currentUiRepository.TryGet(out object viewContext);

                if (hasCurrentUi)
                {
                    bool entryIsOwnerOfCurrentContext = entry.Id == viewContext;

                    if (entryIsOwnerOfCurrentContext)
                    {
                        _currentUiRepository.Clear();
                    }
                }
            }
            else
            {
                _currentPopupsRepository.Remove(entry.Id);
            }

            _viewStack.RemoveAll(entry.Id);

            _uiFrameService.Unregister(entry.Transform);
        }

        public void SetNotInteractableNow<T>(T instance)
        {
            bool found = _entriesRepository.TryGet(instance, out UiStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to SetNotInteractableNow {nameof(UiStackEntry)} of type {instance.GetType().Name}, " +
                    $"but it was not registered, at {nameof(SetNotInteractableNow)}");
                return;
            }

            entry.Transform.gameObject.SetInteractable(false);
        }

        public IUiStackSequenceBuilder New()
        {
            return new UiStackSequenceBuilder(
                _uiFrameService,
                _entriesRepository,
                _currentUiRepository,
                _currentPopupsRepository,
                _viewStack,
                _sequencer,
                _showUseCase,
                _hideUseCase
            );
        }
    }
}
