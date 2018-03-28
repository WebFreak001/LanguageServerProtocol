using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters
{
    public class TextEdit
    {
        public Range Range { get; set; }

        public string NewText { get; set; }
    }
}
