using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Schema;

using Ionic.Zip;

using FlaPreparator.Mapping;

class Utf8StringWriter : StringWriter {
    public override Encoding Encoding { get { return Encoding.UTF8; } }
}

namespace FlaPreparator {
  public class Preparator {
      public void Process(String fla) {
          using (ZipFile zip = ZipFile.Read(fla)) {
              var lib = new Library();
              lib.CleanUp(zip);
              lib.Load(zip);
              lib.items.ToString();
          }
      }

  }
}
