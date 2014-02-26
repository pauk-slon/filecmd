using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace filecmd.Commands
{
    public struct CommandTypeItem
    {
        public System.Type Type { get; set; }
        public string Name { get; set; }
    }

    class CommandType
    {
        static readonly List<CommandTypeItem> _list = new List<CommandTypeItem>
        {
            new CommandTypeItem{Type = typeof(SyncWithFolderCommand), Name = "syncWithFolder"},
            new CommandTypeItem{Type = typeof(DeleteCommand), Name = "delete"},
            new CommandTypeItem{Type = typeof(Arch7zCommand), Name = "arch7z"}
        };

        public static System.Type type(string name)
        {
            Type result = null;
            foreach (CommandTypeItem item
                in _list.Where(item => item.Name.Trim().ToUpper() == name.Trim().ToUpper()))
                result = item.Type;
            return result;
        }
    }
}
