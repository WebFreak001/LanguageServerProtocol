using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class TextDocumentSaveRegistrationOptions : TextDocumentRegistrationOptions
    {
        public bool? IncludeText { get; set; }
    }
}
