using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.Workspace
{
    public class FileEvent
    {
        public Uri Uri { get; set; }
        public FileChangeType Type { get; set; }
    }
}
