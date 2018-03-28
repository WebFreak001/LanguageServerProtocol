using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.Client
{
    public class Registration
    {
        public string Id { get; set; }

        public string Method { get; set; }

        // TODO: TextDocumentRegistrationOptions type
        public dynamic RegisterOptions { get; set; }
    }
}
