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

        [XmlElement("filters")]
        public object filters { get; set; }

        override public DOMElement Clone() {
            var el = new DOMSymbolInstance();
            el.libraryItemName = libraryItemName;
            el.matrix = new List<Matrix>();
            if (matrix.Count != 0) { el.matrix.Add(matrix[0].Clone()); }
            else { el.matrix.Add(new Matrix()); }

            el.transformationPoint = new List<Point>();
            if (transformationPoint.Count != 0) { el.transformationPoint.Add(transformationPoint[0].Clone()); }
            else {
                var p = new Point();
                p.x = "0"; p.y = "0";
                el.transformationPoint.Add(p);
            }
            return el;
        }
    }
}
