namespace GUtilsUnity.Logging.Style
{
    public readonly struct LoggingStyledValue<TValue>
    {
        public static readonly LoggingStyledValue<TValue> EmptyInstance = new();

        readonly string _htmlColor;
        readonly TValue _value;

        LoggingStyledValue(string htmlColor, TValue value)
        {
            _htmlColor = htmlColor;
            _value = value;
        }

        public override string ToString()
        {
            return LoggingStyleUtils.ColoredLog(_htmlColor, _value);
        }

        public static LoggingStyledValue<TValue> New(string htmlColor, TValue value)
        {
            return !PopcoreCoreApplication.IsDebug ? EmptyInstance : new LoggingStyledValue<TValue>(htmlColor, value);
        }
    }
}
