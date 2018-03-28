using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DidCloseTextDocumentParams
    {
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
