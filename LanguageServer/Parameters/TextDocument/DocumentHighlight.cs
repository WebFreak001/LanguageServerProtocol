﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DocumentHighlight
    {
        public Range Range { get; set; }

        public DocumentHighlightKind? Kind { get; set; }
    }
}
