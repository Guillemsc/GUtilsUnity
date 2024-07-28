using GUtils.Tasks.Sequencing.Instructions;
using GUtilsUnity.Repositories;
using GUtilsUnity.UiFrame.Services;
using GUtilsUnity.UiStack.Entries;
using GUtilsUnity.Extensions;

namespace GUtilsUnity.UiStack.Instructions
{
    public sealed class SetInteractableInstruction : InstantInstruction
    {
        readonly IUiFrameService _uiFrameService;
        readonly IKeyValueRepository<object, UiStackEntry> _entriesRepository;
        readonly object _entryId;
        readonly bool _interactable;

        public SetInteractableInstruction(
            IUiFrameService uiFrameService,
            IKeyValueRepository<object, UiStackEntry> entriesRepository,
            object entryId,
            bool interactable
            )
        {
            _uiFrameService = uiFrameService;
            _entriesRepository = entriesRepository;
            _entryId = entryId;
            _interactable = interactable;
        }

        protected override void OnInstantExecute()
        {
            bool found = _entriesRepository.TryGet(_entryId, out UiStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to SetInteractable {nameof(UiStackEntry)} of type {_entryId.GetType().Name}, " +
                                           $"but it was not registered, at {nameof(SetInteractableInstruction)}");
                return;
            }

            if (entry.Transform == null)
            {
                UnityEngine.Debug.LogError($"Tried to SetInteractable {nameof(UiStackEntry)} of type {_entryId.GetType().Name}, " +
                                           $"but provided transform was null, at {nameof(SetInteractableInstruction)}");
                return;
            }

            entry.Transform.gameObject.SetInteractable(_interactable);
        }
    }
}
