﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class CodeActionParams
    {
        public TextDocumentIdentifier TextDocument { get; set; }

        public Range Range { get; set; }

        public CodeActionContext Context { get; set; }
    }
}
