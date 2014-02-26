using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;

namespace filecmd.Commands
{
    class Arch7zCommand : AbstractCommand
    {
        public Arch7zCommand(List<FileFilter> filters, StringDictionary destinations)
            : base(filters, destinations)
        {
        }

        protected override void fileOperations()
        {
            string archName = string.Format("{0}.7z", DateTime.Now.ToString("yyyyMMddhhssmm"));
            string archPath = Destinations[TARGET_DIRECTORY_TYPE_NAME].ToString().Trim();
            string archFullName = Path.Combine(archPath, archName);

            if (Files.Count() > 0)
            {
                string arguments = string.Format("a {0} {1}",
                    archFullName,
                    Files.Aggregate<FileInfo, string>(string.Empty, (acc, file) => acc + " " + file.FullName));
                // TODO: убрать путь к 7-Zip из кода!!!
                Process process = Process.Start(@"C:\Program Files\7-Zip\7z.exe", arguments);
                process.WaitForExit();
            }
        }
    }
}
