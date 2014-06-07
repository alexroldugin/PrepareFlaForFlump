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

        public Point Clone() {
            var p = new Point();
            p.x = x == null ? "0" : x;
            p.y = y == null ? "0" : y;
            return p;
        }
    }
}
