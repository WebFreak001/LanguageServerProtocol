﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DidChangeTextDocumentParams
    {
        public VersionedTextDocumentIdentifier TextDocument { get; set; }

        public TextDocumentContentChangeEvent[] ContentChanges { get; set; }
    }
}
