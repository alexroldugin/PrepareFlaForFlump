using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class DOMLayer {
        [XmlArray("frames")]
        public List<DOMFrame> frames { get; set; }

        [XmlAttribute("name")]
        public String name { get; set; }

        [XmlAttribute("color")]
        public String color { get; set; }

        [XmlAttribute("current")]
        public String current { get; set; }

        [XmlAttribute("isSelected")]
        public String isSelected { get; set; }

        [XmlAttribute("autoNamed")]
        public String autoNamed { get; set; }

        public bool IsEmpty { get { return frames.Count == 0; } }
    }
}
