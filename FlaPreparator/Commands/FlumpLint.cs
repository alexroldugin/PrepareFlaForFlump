using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlaPreparator.Mapping;

namespace FlaPreparator.Commands {
    class FlumpLint : ICommand {
        bool ICommand.Run(Library lib) {
            var result = true;
            foreach (var symbol in lib.Symbols) {
                if (!lintSymbol(symbol)) { result = false; }
            }
            return result;
        }

        bool lintSymbol(DOMSymbolItem symbol) {
            bool result = true;
            foreach (var timeline in symbol.timeline) {
                foreach (var layer in timeline.layers) {
                    if ("folder".Equals(layer.layerType)) {
                        result = false;
                        logError("Symbol " + symbol.name + " has folder '" + layer.name + "'. Remove it.");
                    }

                    foreach (var frame in layer.frames) {
                        if (frame.elements.Count > 1) {
                            logError("Symbol " + symbol.name + " has more than one element on '" + layer.name + "' layer. Check it.");
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        void logError(String message) {
            Console.WriteLine("[ERROR] " + message);
        }
    }
}
