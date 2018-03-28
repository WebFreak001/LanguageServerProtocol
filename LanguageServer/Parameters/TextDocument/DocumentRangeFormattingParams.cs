using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DocumentRangeFormattingParams
    {
        public TextDocumentIdentifier TextDocument { get; set; }

        public Range Range { get; set; }

        public FormattingOptions Options { get; set; }
    }
}
