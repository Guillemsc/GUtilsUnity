namespace GUtilsUnity.Features.ToasterTextsUi.Interactors
{
    public sealed class NopToasterTextsUiInteractor : IToasterTextsUiInteractor
    {
        public static readonly NopToasterTextsUiInteractor Instance = new();

        NopToasterTextsUiInteractor()
        {

        }

        public void Show(string message) { }
    }
}
