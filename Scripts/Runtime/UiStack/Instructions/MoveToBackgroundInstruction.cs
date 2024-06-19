using System;
using GUtilsUnity.Repositories;
using GUtilsUnity.Sequencing.Instructions;
using GUtilsUnity.UiFrame.Services;
using GUtilsUnity.UiStack.Entries;

namespace GUtilsUnity.UiStack.Instructions
{
    public sealed class MoveToBackgroundInstruction : InstantInstruction
    {
        readonly IUiFrameService _uiFrameService;
        readonly IKeyValueRepository<object, UiStackEntry> _entriesRepository;
        readonly object _entryId;

        public MoveToBackgroundInstruction(
            IUiFrameService uiFrameService,
            IKeyValueRepository<object, UiStackEntry> entriesRepository,
            object entryId
            )
        {
            _uiFrameService = uiFrameService;
            _entriesRepository = entriesRepository;
            _entryId = entryId;
        }

        protected override void OnInstantExecute()
        {
            bool found = _entriesRepository.TryGet(_entryId, out UiStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to MoveToBackground {nameof(UiStackEntry)} of type {_entryId.GetType().Name}, " +
                                           $"but it was not registered, at {nameof(MoveToBackgroundInstruction)}");

                return;
            }

            _uiFrameService.MoveToBackground(entry.Transform);
        }
    }
}
