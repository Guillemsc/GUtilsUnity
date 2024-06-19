using GUtilsUnity.Repositories;
using GUtilsUnity.Sequencing.Instructions;
using GUtilsUnity.UiFrame.Services;
using GUtilsUnity.UiStack.Entries;

namespace GUtilsUnity.UiStack.Instructions
{
    public sealed class MoveCurrentToForegroundInstruction : InstantInstruction
    {
        readonly IUiFrameService _uiFrameService;
        readonly IKeyValueRepository<object, UiStackEntry> _entriesRepository;
        readonly ISingleRepository<object> _currentContextRepository;

        public MoveCurrentToForegroundInstruction(
            IUiFrameService uiFrameService,
            IKeyValueRepository<object, UiStackEntry> entriesRepository,
            ISingleRepository<object> currentContextRepository
            )
        {
            _uiFrameService = uiFrameService;
            _entriesRepository = entriesRepository;
            _currentContextRepository = currentContextRepository;
        }

        protected override void OnInstantExecute()
        {
            bool hasCurrentContext = _currentContextRepository.TryGet(out object context);

            if (!hasCurrentContext)
            {
                UnityEngine.Debug.LogError($"Tried to MoveCurrentToForeground " +
                    $"but it there was not a current view context, at {nameof(MoveCurrentToForegroundInstruction)}");
                return;
            }

            bool found = _entriesRepository.TryGet(context, out UiStackEntry entry);

            if (!found)
            {
                UnityEngine.Debug.LogError($"Tried to MoveCurrentToForeground {nameof(UiStackEntry)} of type {context}, " +
                    $"but it was not registered, at {nameof(MoveCurrentToForegroundInstruction)}");

                return;
            }

            _uiFrameService.MoveToForeground(entry.Transform);
        }
    }
}
