using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class TextDocumentPositionParams
    {
        public TextDocumentIdentifier TextDocument { get; set; }

        public Position Position { get; set; }
    }
}
