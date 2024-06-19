namespace GUtilsUnity.Logging.Style
{
    public static class LoggingStyleConstants
    {
        static readonly string ValueDark = "#CC84FD";
        static readonly string ValueLight = "#9F21F5";
        public static string ValueColor => PopcoreCoreApplication.IsUsingEditorProSkin ? ValueDark : ValueLight;

        static readonly string VariableDark = "#75F791";
        static readonly string VariableLight = "#12D63C";
        public static string VariableColor => PopcoreCoreApplication.IsUsingEditorProSkin ? VariableDark : VariableLight;
    }
}
