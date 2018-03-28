using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DocumentSymbolParams
    {
        public TextDocumentIdentifier TextDocument { get; set; }
    }
}
