namespace GUtilsUnity.Logging.Style
{
    public static class LoggingStyleUtils
    {
        public static string ColoredLog<T>(string htmlColor, T toLog)
        {
            return $"<color={htmlColor}>{toLog}</color>";
        }
    }
}
