using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Schema;

using Ionic.Zip;

namespace FlaPreparator.Mapping {
    public class Library {
          List<String> _processedUsed = new List<String>();
          String _xmlns = "http://ns.adobe.com/xfl/2008/";

          Dictionary<String, DOMSymbolItem> items = new Dictionary<String, DOMSymbolItem>();
          public Dictionary<String, DOMSymbolItem> Items { get { return items; } }

          public IEnumerable<String> SymbolNames {
              get {
                  foreach(String name in items.Keys) {
                      var symbol = items[name];
                      if (!symbol.IsSprite()) { yield return name; }
                  }
              }
          }

          public IEnumerable<DOMSymbolItem> Symbols {
              get {
                  foreach (var symbol in items.Values) {
                      if (!symbol.IsSprite()) { yield return symbol; }
                  }
              }
          }

          public IEnumerable<String> SpriteNames {
              get {
                  foreach (String name in items.Keys) {
                      var symbol = items[name];
                      if (symbol.IsSprite()) { yield return name; }
                  }
              }
          }

          public IEnumerable<DOMSymbolItem> Sprites {
              get {
                  foreach (var symbol in items.Values) {
                      if (symbol.IsSprite()) { yield return symbol; }
                  }
              }
          }

          public void CleanUp(ZipFile zip) {
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

          public void Load(ZipFile zip) {
              foreach (var name in _processedUsed) {
                  DOMSymbolItem symbol = deserializeSymbol(zip, name);
                  items.Add(name, symbol);
              }
          }

          public void Save(ZipFile zip) {
              foreach (var name in SymbolNames) {
                  var symbol = items[name];
                  var content = serializeSymbol(symbol);
                  saveContentFile(zip, content, getLibrarySymbolFile(name));
              }
          }

          private String getLibrarySymbolFile(String name) {
              return "LIBRARY/{{NAME}}.xml".Replace("{{NAME}}", name);
          }

          private DOMSymbolItem deserializeSymbol(ZipFile zip, String name) {
              using (MemoryStream stream = new MemoryStream()) {
                  ZipEntry entry = zip[getLibrarySymbolFile(name)];
                  entry.Extract(stream);
                  stream.Seek(0, SeekOrigin.Begin);
                  DOMSymbolItem symbol = null;
                  try {
                      System.Xml.Serialization.XmlSerializer deserializer = new System.Xml.Serialization.XmlSerializer(typeof(DOMSymbolItem), _xmlns);
                      TextReader textReader = new StreamReader(stream);
                      symbol = (DOMSymbolItem)deserializer.Deserialize(textReader);
                      textReader.Close();
                  } catch (Exception e) {
                      e.ToString();
                  }
                  return symbol;
              }
          }

          private String serializeSymbol(DOMSymbolItem symbol) {
              String content = null;
              using (MemoryStream stream = new MemoryStream()) {
                  try {
                      System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DOMSymbolItem), _xmlns);
                      TextWriter textWriter = new StreamWriter(stream, Encoding.UTF8);
                      serializer.Serialize(textWriter, symbol);
                      stream.Position = 0;
                      var textReader = new StreamReader(stream, Encoding.UTF8);
                      content = textReader.ReadToEnd();
                      textWriter.Close();
                  } catch (Exception e) {
                      e.ToString();
                  }
              }
              return content;
          }

          private void clearTimeline(XmlDocument content) {
              XmlNamespaceManager nm = getNamespaceManager(content);
              XmlNode timelines = content.SelectSingleNode("/fl:DOMDocument/fl:timelines", nm);
              timelines.RemoveAll();
          }

          private void saveXmlFile(ZipFile zip, XmlDocument content, String file) {
              StringWriter writer = new Utf8StringWriter();
              content.Save(writer);

              saveContentFile(zip, writer.GetStringBuilder().ToString(), file);
          }

          private void saveContentFile(ZipFile zip, String content, String file) {
              zip.RemoveEntry(file);
              zip.AddEntry(file, content, Encoding.UTF8);
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
