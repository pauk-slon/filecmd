using filecmd.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace filecmd
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args.Aggregate("settings.xml", ((acc, arg) => arg));
            if (!System.IO.File.Exists(fileName))
            {
                Console.WriteLine("Can't find file '{0}'", fileName);
                return;
            }
            try
            {
                Console.Write("Read config file '{0}'... ", fileName);
                List<AbstractCommand> commands = SettingsXML.load(fileName);
                Console.WriteLine("OK");
                commands.ForEach(command => command.execute());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: '{0}'", e.Message);
            }
        }
    }
}
