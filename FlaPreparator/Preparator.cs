using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Schema;

using Ionic.Zip;

class Utf8StringWriter : StringWriter {
    public override Encoding Encoding { get { return Encoding.UTF8; } }
}

namespace FlaPreparator {
  public class Preparator {
      List<String> _processedUsed = new List<String>();
      String _xmlns = "http://ns.adobe.com/xfl/2008/";

      public void Process(String fla) {
          using (ZipFile zip = ZipFile.Read(fla)) {
            XmlDocument content = loadXmlFile(zip, "DOMDocument.xml");
            List<String> includedSymbols = getIncludedSymbols(content);
            List<String> exportedSymbols = includedSymbols.FindAll((item) => {
                return item.StartsWith("export/");
            });
            processAllUsedSymbols(zip, exportedSymbols);
            filterIncludedSymbols(content, _processedUsed);
            clearTimeline(content);
            saveXmlFile(zip, content, "DOMDocument.xml");
          }
      }

      private void clearTimeline(XmlDocument content) {
          XmlNamespaceManager nm = getNamespaceManager(content);
          XmlNode timelines = content.SelectSingleNode("/fl:DOMDocument/fl:timelines", nm);
          timelines.RemoveAll();
      }

      private void saveXmlFile(ZipFile zip, XmlDocument content, String file) {
          ZipEntry entry = zip[file];
          zip.RemoveEntry(file);
          StringWriter writer = new Utf8StringWriter();
          content.Save(writer);

          zip.AddEntry(file, writer.GetStringBuilder().ToString());
          zip.Save(zip.Name);
      }

      private void filterIncludedSymbols(XmlDocument content, List<String> neededSymbols) {
          XmlNamespaceManager nm = getNamespaceManager(content);
          XmlNodeList nodes = content.SelectNodes("/fl:DOMDocument/fl:symbols/fl:Include", nm);
          foreach (XmlNode node in nodes) {
              if (!neededSymbols.Contains(node.Attributes["href"].Value.Replace(".xml", ""))) {
                  node.ParentNode.RemoveChild(node);
              }
          }
      }

    protected void processAllUsedSymbols(ZipFile zip, List<String> symbols) {
        HashSet<String> hash = new HashSet<String>();
        foreach (String symbol in symbols) {
            if (!_processedUsed.Contains(symbol)) {
                _processedUsed.Add(symbol);
                List<string> usedSymbols = getUsedSymbols(zip, symbol);
                processAllUsedSymbols(zip, usedSymbols);
            }
        }
    }

    protected List<String> getUsedSymbols(ZipFile zip, String symbol) {
        XmlDocument content = loadXmlFile(zip, "LIBRARY/" + symbol + ".xml");
        XmlNamespaceManager nm = getNamespaceManager(content);
        XmlNodeList nodes = content.SelectNodes("/fl:DOMSymbolItem/fl:timeline/fl:DOMTimeline/fl:layers/fl:DOMLayer/fl:frames/fl:DOMFrame/fl:elements/fl:DOMSymbolInstance", nm);
        List<String> symbols = new List<string>();
        foreach (XmlNode node in nodes) {
            symbols.Add(node.Attributes["libraryItemName"].Value);
        }
        return symbols;
    }

    protected List<String> getIncludedSymbols(XmlDocument content) {
        XmlNamespaceManager nm = getNamespaceManager(content);
        XmlNodeList nodes = content.SelectNodes("/fl:DOMDocument/fl:symbols/fl:Include", nm);
        List<String> symbols = new List<string>();
        foreach(XmlNode node in nodes) {
            symbols.Add(node.Attributes["href"].Value.Replace(".xml", ""));
        }
        return symbols;
    }

    protected XmlNamespaceManager getNamespaceManager(XmlDocument content) {
        XmlNamespaceManager nm = new XmlNamespaceManager(content.NameTable);
        nm.AddNamespace("fl", _xmlns);
        return nm;
    }

    protected XmlDocument loadXmlFile(ZipFile zip, String file) {
      using (MemoryStream stream = new MemoryStream()) {
          ZipEntry entry = zip[file];
          entry.Extract(stream);
          stream.Seek(0, SeekOrigin.Begin);
          XmlDocument xml = new XmlDocument();
          xml.Load(stream);
          return xml;
      }
    }
  }
}
