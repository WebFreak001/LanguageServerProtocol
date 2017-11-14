using System.IO;
using System.Text;

namespace LanguageServer
{
    public static class Log
    {
        public static void Initialize(string logFilePath, string threshold)
        {
            try
            {
                if (!string.IsNullOrEmpty(logFilePath))
                {
                    var stream = File.OpenWrite(logFilePath);
                    Log.Writer = new StreamWriter(stream, Encoding.UTF8);
                }
            }
            catch
            {
                // ignore
            }
            Log.Threshold = LogLevel.FromString(threshold);
        }

        private static TextWriter _writer = TextWriter.Null;
        public static TextWriter Writer
        {
            get => _writer ?? TextWriter.Null;
            set => _writer = value ?? TextWriter.Null;
        }
        public static LogLevel Threshold { get; set; } = LogLevel.None;
        public static void Error(string message) => WriteLine(LogLevel.Error, message);
        public static void Warn(string message) => WriteLine(LogLevel.Warn, message);
        public static void Info(string message) => WriteLine(LogLevel.Info, message);
        public static void Debug(string message) => WriteLine(LogLevel.Debug, message);
        internal static void WriteLine(LogLevel level, string message)
        {
            if (level.Exceeds(Log.Threshold))
            {
                try
                {
                    Log.Writer.WriteLine(message);
                }
                catch
                {
                    var errorWriter = Log.Writer;
                    if (errorWriter != null) errorWriter.Dispose();
                    Log.Writer = TextWriter.Null;
                }
            }
        }
    }

    public struct LogLevel
    {
        internal int priority;
        private LogLevel(int priority)
        {
            this.priority = priority;
        }
        public static LogLevel FromString(string name)
        {
            if (name != null)
            {
                name = name.ToLowerInvariant();
            }
            switch (name)
            {
                case "error":
                    return LogLevel.Error;
                case "warn":
                    return LogLevel.Warn;
                case "info":
                    return LogLevel.Info;
                case "debug":
                    return LogLevel.Debug;
                default:
                    return LogLevel.None;
            }
        }
        public static readonly LogLevel None = new LogLevel(0);
        public static readonly LogLevel Error = new LogLevel(100);
        public static readonly LogLevel Warn = new LogLevel(200);
        public static readonly LogLevel Info = new LogLevel(300);
        public static readonly LogLevel Debug = new LogLevel(400);
        public bool Exceeds(LogLevel target) => this.priority <= target.priority;
    }
}
