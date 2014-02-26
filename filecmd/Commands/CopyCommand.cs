using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;

namespace filecmd.Commands
{
    public class CopyCommand: AbstractCommand
    {
        public CopyCommand(List<FileFilter> filters, StringDictionary destinations)
            : base(filters, destinations)
        {
        }

        protected override void fileOperations()
        {
            
            Files.ToList().ForEach(file => 
                file.CopyTo(Path.Combine(
                    Destinations[TARGET_DIRECTORY_TYPE_NAME].ToString(), file.Name), 
                    true));
        }
    }
}
