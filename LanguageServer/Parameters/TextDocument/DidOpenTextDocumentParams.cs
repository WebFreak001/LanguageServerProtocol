using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DidOpenTextDocumentParams
    {
        public TextDocumentItem TextDocument { get; set; }
    }
}
