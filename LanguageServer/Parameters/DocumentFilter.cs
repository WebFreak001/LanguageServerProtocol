using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters
{
    public class DocumentFilter
    {
        public string Language { get; set; }

        public string Scheme { get; set; }

        public string Pattern { get; set; }
    }
}
