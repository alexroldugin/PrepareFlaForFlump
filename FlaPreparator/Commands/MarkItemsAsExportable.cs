using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlaPreparator.Mapping;

namespace FlaPreparator.Commands {
    class MarkItemsAsExportable : ICommand {
        bool ICommand.Run(Library lib) {
            foreach (var symbol in lib.Symbols) {
                setupSymbolExportable(symbol);
            }
            foreach (var symbol in lib.Sprites) {
                symbol.isSpriteSubclass = "true";
                symbol.linkageBaseClass = "flash.display.Sprite";
                setupSymbolExportable(symbol);
            }
            return true;
        }

        void setupSymbolExportable(DOMSymbolItem symbol) {
            symbol.linkageExportForAS = "true";
            var path = symbol.name.Replace(" ", "").Split('/');
            symbol.linkageClassName = path.Last();
        }
    }
}
