﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class TextDocumentChangeRegistrationOptions : TextDocumentRegistrationOptions
    {
        public TextDocumentSyncKind SyncKind { get; set; }
    }
}
