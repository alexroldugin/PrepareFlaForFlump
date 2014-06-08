using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using FlaPreparator.Commands;

namespace FlaPreparator {
  internal class Program {
    public Program() {
    }

    private static void error(string message) {
      Console.WriteLine(message);
      Console.WriteLine();
      Program.help();
    }

    private static void help() {
      Console.WriteLine("Usage: ");
      Console.WriteLine(" FlaPreparator <fla file>");
    }

    private static void Main(string[] args) {
      if (args.Length < 1) { help(); }
      else {
        String file = args[0];
        Preparator preparator = new Preparator();
        preparator.Process(file, new List<ICommand> {
            new FlumpLint(),
            new DistributeTweeners(),
            new MarkItemsAsExportable()
        });
      }
    }
  }
}