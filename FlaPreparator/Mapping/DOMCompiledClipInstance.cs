using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class DOMCompiledClipInstance : DOMElement {
        [XmlAttribute("libraryItemName")]
        public String libraryItemName { get; set; }

        [XmlAttribute("uniqueID")]
        public String uniqueID { get; set; }

        [XmlArray("matrix")]
        public List<Matrix> matrix { get; set; }

        [XmlElement("dataBindingXML")]
        public String dataBindingXML { get; set; }

        override public DOMElement Clone() {
            var el = new DOMCompiledClipInstance();
            el.libraryItemName = libraryItemName;
            el.uniqueID = uniqueID;
            el.dataBindingXML = dataBindingXML;
            el.matrix = new List<Matrix>();
            el.matrix.Add(matrix[0].Clone());
            return el;
        }
    }
}
