using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class Point {
        [XmlAttribute("x")]
        public String x { get; set; }

        [XmlAttribute("y")]
        public String y { get; set; }
    }
}
