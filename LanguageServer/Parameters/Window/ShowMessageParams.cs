using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.Window
{
    public class ShowMessageParams
    {
        public MessageType Type { get; set; }
        public string Message { get; set; }
    }
}
