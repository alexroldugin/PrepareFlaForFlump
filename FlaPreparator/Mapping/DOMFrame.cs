using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    public class DOMFrame {
        [XmlArray("elements")]
        [XmlArrayItem(typeof(DOMSymbolInstance)),
         XmlArrayItem(typeof(DOMShape)),
         XmlArrayItem(typeof(DOMCompiledClipInstance))]
        public List<DOMElement> elements { get; set; }

        [XmlAttribute("index")]
        public String index { get; set; }

        [XmlAttribute("name")]
        public String name { get; set; }

        [XmlAttribute("keyMode")]
        public String keyMode { get; set; }

        [XmlAttribute("duration")]
        public String duration { get; set; }

        [XmlAttribute("labelType")]
        public String labelType { get; set; }

        [XmlAttribute("tweenType")]
        public String tweenType { get; set; }

        [XmlAttribute("motionTweenSnap")]
        public String motionTweenSnap { get; set; }

        public bool IsEmpty { get { return elements.Count == 0; } }
        public DOMFrame Clone() {
            var frame = new DOMFrame();
            frame.index = index;
            frame.keyMode = keyMode;
            frame.duration = duration;
            frame.labelType = labelType;
            frame.tweenType = tweenType;
            frame.motionTweenSnap = motionTweenSnap;
            frame.elements = new List<DOMElement>();
            foreach (var element in elements) {
                frame.elements.Add(element.Clone());
            }
            return frame;
        }
    }
}
