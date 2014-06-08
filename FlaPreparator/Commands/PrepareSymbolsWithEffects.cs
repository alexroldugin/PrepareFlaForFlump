using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlaPreparator.Mapping;

namespace FlaPreparator.Commands {
    class PrepareSymbolsWithEffects : ICommand {
        bool ICommand.Run(Library lib) {
            var result = true;
            var symbols = getSymbolsWithEffects(lib);
            lib.CreateFolder("rasterize");
            foreach (var symbol in symbols) {
                lib.RenameSymbol(symbol, "rasterize/" + symbol.name.Replace("export/", ""));
            }
            return result;
        }

        private List<DOMSymbolItem> getSymbolsWithEffects(Library lib) {
            var symbolsWithFilters = new List<DOMSymbolItem>();
            foreach (var symbol in lib.Symbols) {
                if (hasEffects(symbol)) { symbolsWithFilters.Add(symbol); }
            }
            return symbolsWithFilters;
        }

        private bool hasEffects(DOMSymbolItem symbol) {
            if (symbol.timeline != null && symbol.timeline[0].layers != null) {
                if (symbol.timeline[0].layers.Count == 1 && symbol.timeline[0].layers[0].frames.Count == 1) {
                    var frame = symbol.timeline[0].layers[0].frames[0];
                    if (frame.elements !=null && frame.elements.Count == 1 && frame.elements[0] is DOMSymbolInstance) {
                        return (frame.elements[0] as DOMSymbolInstance).HasFilters;
                    }
                }
            }
            return false;
        }
    }
}
