using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class CompletionRegistrationOptions : TextDocumentRegistrationOptions
    {
        public string[] TriggerCharacters { get; set; }

        public bool? ResolveProvider { get; set; }
    }
}
