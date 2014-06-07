using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class DOMSymbolInstance : DOMElement {
        [XmlArray("matrix")]
        public List<Matrix> matrix { get; set; }

        [XmlArray("transformationPoint")]
        public List<Point> transformationPoint { get; set; }

        [XmlAttribute("libraryItemName")]
        public String libraryItemName { get; set; }
    }
}
