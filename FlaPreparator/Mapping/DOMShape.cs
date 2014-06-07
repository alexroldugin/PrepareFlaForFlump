using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class DOMShape : DOMElement {
        [XmlElement]
        public object fills { get; set; }

        [XmlElement]
        public object edges { get; set; }

        [XmlElement]
        public object strokes { get; set; }

        [XmlAttribute("isFloating")]
        public String isFloating { get; set; }

        [XmlAttribute("selected")]
        public String selected { get; set; }

        override public DOMElement Clone() {
            var el = new DOMShape();
            el.fills = fills;
            el.edges = edges;
            el.strokes = strokes;
            el.isFloating = isFloating;
            el.selected = selected;
            return el;
        }
    }
}
