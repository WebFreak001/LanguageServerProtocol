using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters
{
    public class TextDocumentEdit
    {
        public VersionedTextDocumentIdentifier TextDocument { get; set; }

        public TextEdit[] Edits { get; set; }
    }
}
