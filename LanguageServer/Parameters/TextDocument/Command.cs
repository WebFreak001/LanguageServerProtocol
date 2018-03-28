using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters.TextDocument
{
    public class Command
    {
        public string Title { get; set; }

        [JsonProperty("command")]
        public string Name { get; set; }

        public dynamic[] Arguments { get; set; }
    }
}
