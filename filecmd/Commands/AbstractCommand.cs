using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Specialized;

namespace filecmd.Commands
{
    public abstract class AbstractCommand
    {
        protected const string SOURCE_DIRECTORY_TYPE_NAME = "source";
        protected const string TARGET_DIRECTORY_TYPE_NAME = "target";

        private IEnumerable<FileFilter> FileFilters { get; set; }

        protected IDictionary<string, DirectoryInfo> Destinations { get; set; }
        protected IEnumerable<FileInfo> Files { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public AbstractCommand(IEnumerable<FileFilter> filters, StringDictionary destinations)
        {
            FileFilters = filters.ToList();

            Destinations = new Dictionary<string, DirectoryInfo>();
            string sourcePath = destinations[SOURCE_DIRECTORY_TYPE_NAME];
            Destinations[SOURCE_DIRECTORY_TYPE_NAME] = new DirectoryInfo(sourcePath);
            if (destinations.ContainsKey(TARGET_DIRECTORY_TYPE_NAME))
            {
                string targetPath = destinations[TARGET_DIRECTORY_TYPE_NAME];
                Destinations[TARGET_DIRECTORY_TYPE_NAME] = new DirectoryInfo(targetPath);
            }
            
        }
        protected virtual void createFilesList()
        {
            IEnumerable<FileInfo> files = 
                Destinations[SOURCE_DIRECTORY_TYPE_NAME].GetFiles();
            Files = files.Where(file => 
                FileFilters.Any(filter => filter.isMatch(file)));
        }
        
        protected abstract void fileOperations();

        public void execute()
        {
            createFilesList();
            fileOperations();
        }
    }
}
