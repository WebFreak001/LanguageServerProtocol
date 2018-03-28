using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters
{
    // textDocument/rename & workspace/applyEdit
    public class WorkspaceEdit
    {
        public Dictionary<Uri, TextEdit[]> Changes { get; set; }

        public TextDocumentEdit[] DocumentChanges { get; set; }
    }
}
