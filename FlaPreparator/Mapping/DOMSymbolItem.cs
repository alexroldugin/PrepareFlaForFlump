using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    [XmlRoot(ElementName = "DOMSymbolItem")]
    public class DOMSymbolItem {
        [XmlArray("timeline")]
        public List<DOMTimeline> timeline { get; set; }

        [XmlAttribute("name")]
        public String name { get; set; }

        [XmlAttribute("itemID")]
        public String itemID { get; set; }

        [XmlAttribute("lastModified")]
        public String lastModified { get; set; }

        [XmlAttribute("lastUniqueIdentifier")]
        public String lastUniqueIdentifier { get; set; }


        [XmlAttribute("linkageExportForAS")]
        public String linkageExportForAS { get; set; }

        [XmlAttribute("linkageBaseClass")]
        public String linkageBaseClass { get; set; }

        [XmlAttribute("linkageClassName")]
        public String linkageClassName { get; set; }

        [XmlAttribute("isSpriteSubclass")]
        public String isSpriteSubclass { get; set; }

        public bool IsSprite() {
            foreach (var layers in timeline[0].layers) {
                foreach (var frame in layers.frames) {
                    var shape = frame.elements.FirstOrDefault((item) => { return item is DOMShape; });
                    if (shape != null) { return true; }
                }
            }
            return false;
        }
    }
}
