﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DocumentOnTypeFormattingRegistrationOptions : TextDocumentRegistrationOptions
    {
        public string FirstTriggerCharacter { get; set; }

        public string[] MoreTriggerCharacter { get; set; }
    }
}
