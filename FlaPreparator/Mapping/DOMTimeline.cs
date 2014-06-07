using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    [XmlType("DOMTimeline")]
    public class DOMTimeline {
        [XmlArray("layers")]
        public List<DOMLayer> layers { get; set; }

        [XmlAttribute("name")]
        public String name { get; set; }

        [XmlAttribute("currentFrame")]
        public String currentFrame { get; set; }
    }
}
