using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class SignatureInformation
    {
        public string Label { get; set; }

        public string Documentation { get; set; }

        public ParameterInformation[] Parameters { get; set; }
    }
}
