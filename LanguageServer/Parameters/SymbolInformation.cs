using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageServer.Parameters
{
    // textDocument/documentSymbol & workspace/symbol
    public class SymbolInformation
    {
        public string Name { get; set; }
        public SymbolKind Kind { get; set; }
        public Location Location { get; set; }
        public string ContainerName { get; set; }
    }
}
