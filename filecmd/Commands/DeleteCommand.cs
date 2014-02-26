using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace filecmd.Commands
{
    class DeleteCommand : AbstractCommand
    {
        public DeleteCommand(List<FileFilter> filters, StringDictionary destinations)
            : base(filters, destinations)
        {
        }

        protected override void fileOperations()
        {
            Files.ToList().ForEach(file => file.Delete());
        }
    }
    
}
