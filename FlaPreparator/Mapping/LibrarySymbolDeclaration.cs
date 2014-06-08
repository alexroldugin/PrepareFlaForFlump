using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class LibrarySymbolDeclaration {
        [XmlAttribute("href")]
        public String href { get; set; }

        [XmlAttribute("itemID")]
        public String itemID { get; set; }

        [XmlAttribute("lastModified")]
        public String lastModified { get; set; }
    }
}
