using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.Window
{
    public class LogMessageParams
    {
        public MessageType Type { get; set; }
        public string Message { get; set; }
    }
}
