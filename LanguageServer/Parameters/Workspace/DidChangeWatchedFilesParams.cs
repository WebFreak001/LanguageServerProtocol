using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.Workspace
{
    public class DidChangeWatchedFilesParams
    {
        public FileEvent[] Changes { get; set; }
    }
}
