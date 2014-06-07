using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class Matrix {
        [XmlAttribute("tx")]
        public String tx { get; set; }

        [XmlAttribute("ty")]
        public String ty { get; set; }

        [XmlAttribute("a")]
        public String a { get; set; }

        [XmlAttribute("b")]
        public String b { get; set; }

        [XmlAttribute("c")]
        public String c { get; set; }

        [XmlAttribute("d")]
        public String d { get; set; }
    }
}
