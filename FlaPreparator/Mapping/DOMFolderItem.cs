using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class DOMFolderItem {
        [XmlAttribute("name")]
        public String name { get; set; }

        [XmlAttribute("itemID")]
        public String itemID { get; set; }
    }
}
