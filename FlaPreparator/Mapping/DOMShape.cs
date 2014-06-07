using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class DOMShape : DOMElement {
        [XmlAttribute("isFloating")]
        public String isFloating { get; set; }

        [XmlAttribute("selected")]
        public String selected { get; set; }
    }
}
