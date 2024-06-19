using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Repositories;
using GUtilsUnity.UiFrame.Services;
using GUtilsUnity.UiStack.Entries;
using GUtilsUnity.UiStack.Enums;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.UiStack.UseCases
{
    public sealed class ShowUseCase
    {
        readonly IUiFrameService _uiFrameService;
        readonly IKeyValueRepository<object, UiStackEntry> _entriesRepository;
        readonly ISingleRepository<object> _currentUiRepository;
        readonly IListRepository<object> _currentPopupsRepository;

        public ShowUseCase(
            IUiFrameService uiFrameService,
            IKeyValueRepository<object, UiStackEntry> entriesRepository,
            ISingleRepository<object> currentUiRepository,
            IListRepository<object> currentPopupsRepository
        )
        {
            _uiFrameService = uiFrameService;
            _entriesRepository = entriesRepository;
            _currentUiRepository = currentUiRepository;
            _currentPopupsRepository = currentPopupsRepository;
        }

        public async Task Execute(
            object entryId,
            bool behindForeground,
            bool instantly,
            CancellationToken cancellationToken
            )
        {
            bool found = _entriesRepository.TryGet(entryId, out UiStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to Show {nameof(UiStackEntry)} of type {entryId.GetType().Name}, " +
                                           $"but it was not registered, at {nameof(ShowUseCase)}");
                return;
            }

            if (entry.Transform == null)
            {
                UnityEngine.Debug.LogError($"Tried to Show {nameof(UiStackEntry)} of type {entryId.GetType().Name}, " +
                                           $"but provided transform was null, at {nameof(ShowUseCase)}");
                return;
            }

            entry.Transform.gameObject.SetInteractable(false);

            if(!entry.IsPopup)
            {
                _currentUiRepository.Set(entry.Id);
            }
            else
            {
                _currentPopupsRepository.Add(entry.Id);
            }

            UiStackEntryUtils.Refresh(entry, RefreshType.BeforeShow);

            if (behindForeground)
            {
                _uiFrameService.MoveBehindForeground(entry.Transform);
            }
            else
            {
                _uiFrameService.MoveToForeground(entry.Transform);
            }

            await entry.Visible.SetVisible(visible: true, instantly, cancellationToken);

            UiStackEntryUtils.Refresh(entry, RefreshType.AfterShow);

            if (entry.Transform == null)
            {
                UnityEngine.Debug.LogError($"Tried to Show {nameof(UiStackEntry)} of type {entryId.GetType().Name}, " +
                                           $"but provided transform was null, at {nameof(ShowUseCase)}");
                return;
            }

            entry.Transform.gameObject.SetInteractable(true);
        }
    }
}
