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
            var symbolsWithFilters = new List<DOMSymbolItem>();
            foreach (var symbol in lib.Symbols) {
                if (hasEffects(symbol)) { symbolsWithFilters.Add(symbol); }
            }
            return result;
        }

        private bool hasEffects(DOMSymbolItem symbol) {
            foreach (var timeline in symbol.timeline) {
                foreach (var layer in timeline.layers) {
                    foreach (var frame in layer.frames) {
                        if(frame.elements.FirstOrDefault((el) => { return el is DOMSymbolInstance && (el as DOMSymbolInstance).HasFilters; }) != null) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
