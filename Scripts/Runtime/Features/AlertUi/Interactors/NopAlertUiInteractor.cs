namespace GUtilsUnity.Features.AlertUi.Interactor
{
    public sealed class NopAlertUiInteractor : IAlertUiInteractor
    {
        public static readonly NopAlertUiInteractor Instance = new ();

        NopAlertUiInteractor()
        {

        }

        public void SetVisible(bool visible) { }
        public void PlayAlert() { }
        public void PlayAlertOnce() { }
    }
}
