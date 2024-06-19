using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GUtilsUnity.Repositories;
using GUtilsUnity.UiStack.Entries;
using GUtilsUnity.UiStack.Enums;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.UiStack.UseCases
{
    public sealed class HideUseCase
    {
        readonly IKeyValueRepository<object, UiStackEntry> _entriesRepository;
        readonly ISingleRepository<object> _currentContextRepository;
        readonly IListRepository<object> _currentPopupsRepository;
        readonly List<object> _viewStackQueue;

        public HideUseCase(
            IKeyValueRepository<object, UiStackEntry> entriesRepository,
            ISingleRepository<object> currentContextRepository,
            IListRepository<object> currentPopupsRepository,
            List<object> viewStackQueue
        )
        {
            _entriesRepository = entriesRepository;
            _currentContextRepository = currentContextRepository;
            _currentPopupsRepository = currentPopupsRepository;
            _viewStackQueue = viewStackQueue;
        }

        public async Task Execute(
            object instance,
            bool pushToViewQueue,
            bool instantly,
            CancellationToken cancellationToken
            )
        {
            bool found = _entriesRepository.TryGet(instance, out UiStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to Hide {nameof(UiStackEntry)} of type {instance.GetType().Name}, " +
                                           $"but it was not registered, at {nameof(HideUseCase)}");

                return;
            }

            if (entry.Transform == null)
            {
                UnityEngine.Debug.LogError($"Tried to Hide {nameof(UiStackEntry)} of type {instance.GetType().Name}, " +
                                           $"but provided transform was null, at {nameof(HideUseCase)}");
                return;
            }

            entry.Transform.gameObject.SetInteractable(false);

            if (!entry.IsPopup)
            {
                bool hasCurrentContext = _currentContextRepository.TryGet(out object context);

                if (!hasCurrentContext)
                {
                    UnityEngine.Debug.LogError($"Tried to Hide {nameof(entry.Id)} as Popup, " +
                                               $"but it there was not a current view context, at {nameof(HideUseCase)}");
                    return;
                }

                if (context == instance)
                {
                    _currentContextRepository.Clear();
                }

                if (pushToViewQueue)
                {
                    _viewStackQueue.Add(instance);
                }
            }
            else
            {
                _currentPopupsRepository.Remove(instance);
            }

            await Hide(entry, instantly, cancellationToken);
        }

        async Task Hide(UiStackEntry uiStackEntry, bool instantly, CancellationToken cancellationToken)
        {
            UiStackEntryUtils.Refresh(uiStackEntry, RefreshType.BeforeHide);

            await uiStackEntry.Visible.SetVisible(visible: false, instantly, cancellationToken);

            UiStackEntryUtils.Refresh(uiStackEntry, RefreshType.AfterHide);
        }
    }
}
