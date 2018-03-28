using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters
{
    public class Location
    {
        public Uri Uri { get; set; }
        public Range Range { get; set; }
    }
}
