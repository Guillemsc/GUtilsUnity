namespace GUtilsUnity.Features.AlertUi.Interactor
{
    public interface IAlertUiInteractor
    {
        void SetVisible(bool visible);
        void PlayAlert();
        void PlayAlertOnce();
    }
}
