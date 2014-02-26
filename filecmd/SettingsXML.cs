using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Collections.Specialized;
using filecmd.Commands;

namespace filecmd
{
    class SettingsXML
    {
        public static List<AbstractCommand> load(string fileName)
        {
            XDocument settingsDocument = XDocument.Load(fileName);
            List<XElement> xmlCommannds = settingsDocument.Root
                .Elements().ToList<XElement>();
            List<AbstractCommand> commands = new List<AbstractCommand>();
            foreach (XElement xElement in xmlCommannds)
            {
                // Получаем элементы всех секций files данной команды
                List<XElement> xFilters = new List<XElement>();
                xElement.Elements("files").ToList()
                    .ForEach(eFiles => 
                        xFilters = xFilters.Union<XElement>(eFiles.Elements()).ToList());
                // Создаём фильтры
                List<FileFilter> filters = new List<FileFilter>();
                filters = xFilters.Select(xFilter => new FileFilter(xFilter)).ToList();

                // Получаем папки назначения
                List<XElement> xDestinations = new List<XElement>();
                xElement.Elements("destinations").ToList().ForEach(dir => 
                    xDestinations = xDestinations.Union<XElement>(dir.Elements()).ToList());
                StringDictionary destinations = new StringDictionary();
                xDestinations.ForEach(dir => 
                    destinations.Add(dir.Attribute("type").Value, dir.Value));
                
                // Создание объектов-команд
                string commandTypeName = xElement.Attribute("name").Value;
                Type type = CommandType.type(commandTypeName);
                AbstractCommand command = Activator.CreateInstance(
                    type, new object[] { filters, destinations }) as AbstractCommand;
                command.Name = commandTypeName;
                string commandDescription = xElement.Attribute("description").Value;
                command.Description = commandDescription;
                commands.Add(command);
            }
            return commands;
        }
    }
}
