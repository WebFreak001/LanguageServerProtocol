using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class DocumentLink
    {
        public Range Range { get; set; }

        public Uri Target { get; set; }
    }
}
