using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace filecmd.Commands
{
    public class FileFilter
    {
        public string Name { get; set; }
        public TimeSpan TimeCreationTimeout { get; set; }

        public FileFilter(XElement xFilter)
        {
            Name = xFilter.Attribute("name") != null ?
                xFilter.Attribute("name").Value : string.Empty;
            TimeCreationTimeout = xFilter.Attribute("timeout") != null ?
                TimeSpan.Parse(xFilter.Attribute("timeout").Value) : new TimeSpan(0);
        }

        public bool isMatch(FileInfo file)
        {
            //DateTime dt = file.LastWriteTime + TimeCreationTimeout;
            return 
                ((Name == string.Empty)||(Regex.IsMatch(file.Name, Name)))
                && (
                    (TimeCreationTimeout.Ticks == 0) ||
                    ((file.LastWriteTime + TimeCreationTimeout < DateTime.Now) && (TimeCreationTimeout.Ticks > 0)) || 
                    ((file.LastWriteTime - TimeCreationTimeout >= DateTime.Now) && (TimeCreationTimeout.Ticks < 0)));
        }

    }
}
