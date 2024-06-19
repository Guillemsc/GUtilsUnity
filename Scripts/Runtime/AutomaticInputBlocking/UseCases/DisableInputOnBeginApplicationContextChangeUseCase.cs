using GUtilsUnity.UiFrame.Services;

namespace GUtilsUnity.AutomaticInputBlocking.UseCases
{
    public class DisableInputOnBeginApplicationContextChangeUseCase
    {
        readonly IUiFrameService _uiFrameService;
        readonly object _activeHandle;

        public DisableInputOnBeginApplicationContextChangeUseCase(IUiFrameService uiFrameService, object activeHandle)
        {
            _uiFrameService = uiFrameService;
            _activeHandle = activeHandle;
        }

        public void Execute()
        {
            _uiFrameService.InteractableActiveSource.SetActive(_activeHandle, false);
        }
    }
}
