using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace filecmd.Commands
{
    class SyncWithFolderCommand: CopyCommand
    {
        public SyncWithFolderCommand(List<FileFilter> filters, StringDictionary destinations)
            : base(filters, destinations)
        {
        }

        protected override void createFilesList()
        {
            base.createFilesList();
            List<FileInfo> targetFiles = Destinations[TARGET_DIRECTORY_TYPE_NAME].GetFiles().ToList();
            Files = Files.Where(sourceFile => targetFiles.All<FileInfo>(targetFile =>
                sourceFile.Name != targetFile.Name || 
                (sourceFile.Name == targetFile.Name && sourceFile.Length != targetFile.Length))).ToList();
        }
     }
}
