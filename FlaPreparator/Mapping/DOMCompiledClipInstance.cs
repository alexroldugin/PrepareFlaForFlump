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
    }
}
