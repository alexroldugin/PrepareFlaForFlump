﻿using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Schema;

using Ionic.Zip;

using FlaPreparator.Mapping;
using FlaPreparator.Commands;

class Utf8StringWriter : StringWriter {
    public override Encoding Encoding { get { return Encoding.UTF8; } }
}

namespace FlaPreparator {
  public class Preparator {

      public void Process(String fla, List<ICommand> commands) {
          /*ZipFile zip2 = ZipFile.Read(fla.Replace(".fla", "2.fla"));
          using (ZipFile zip = ZipFile.Read(fla)) {
              var lib = new Library();
              lib.CleanUp(zip);
              lib.Load(zip);
              lib.RemoveLoadedData(zip2);
              foreach (var command in commands) { if (!command.Run(lib)) { break; } }
              lib.Save(zip2);
          }
          zip2.Save(zip2.Name);
          zip2.Dispose();*/
          using (ZipFile zip = ZipFile.Read(fla)) {
              var lib = new Library();
              lib.CleanUp(zip);
              lib.Load(zip);
              lib.RemoveLoadedData(zip);
              var commandsResult = true;
              foreach (var command in commands) { if (!command.Run(lib)) { commandsResult = false; break; } }

              if (commandsResult) { lib.Save(zip); }
          }
      }


  }
}
