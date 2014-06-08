using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace FlaPreparator.Mapping {
    [XmlRoot(ElementName = "DOMDocument")]
    public class DOMDocument {
        [XmlAttribute("width")]
        public String width { get; set; }

        [XmlAttribute("height")]
        public String height { get; set; }

        [XmlAttribute("frameRate")]
        public String frameRate { get; set; }

        [XmlAttribute("currentTimeline")]
        public String currentTimeline { get; set; }

        [XmlAttribute("xflVersion")]
        public String xflVersion { get; set; }

        [XmlAttribute("creatorInfo")]
        public String creatorInfo { get; set; }

        [XmlAttribute("platform")]
        public String platform { get; set; }

        [XmlAttribute("versionInfo")]
        public String versionInfo { get; set; }

        [XmlAttribute("majorVersion")]
        public String majorVersion { get; set; }

        [XmlAttribute("buildNumber")]
        public String buildNumber { get; set; }

        [XmlAttribute("guidesVisible")]
        public String guidesVisible { get; set; }

        [XmlAttribute("timelineLabelWidth")]
        public String timelineLabelWidth { get; set; }

        [XmlAttribute("viewAngle3D")]
        public String viewAngle3D { get; set; }

        [XmlAttribute("nextSceneIdentifier")]
        public String nextSceneIdentifier { get; set; }

        [XmlAttribute("playOptionsPlayLoop")]
        public String playOptionsPlayLoop { get; set; }

        [XmlAttribute("playOptionsPlayPages")]
        public String playOptionsPlayPages { get; set; }

        [XmlAttribute("playOptionsPlayFrameActions")]
        public String playOptionsPlayFrameActions { get; set; }

        [XmlAttribute("autoSaveHasPrompted")]
        public String autoSaveHasPrompted { get; set; }



        [XmlArray("folders")]
        public List<DOMFolderItem> folders { get; set; }

        [XmlElement("media")]
        public object media { get; set; }

        [XmlArray("symbols")]
        [XmlArrayItem("Include")]
        public List<LibrarySymbolDeclaration> symbols { get; set; }

        [XmlElement("timelines")]
        public object timelines { get; set; }

        [XmlElement("swatchLists")]
        public object swatchLists { get; set; }

        [XmlElement("swcCache")]
        public object swcCache { get; set; }
    }
}
