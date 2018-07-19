using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using FindProgram.Models;

namespace FindProgram
{
    static class XMLWorker
    {
        public static FindInfo Read()
        {
            
            try
            {
                FindInfo info = new FindInfo();
                XmlDocument doc = new XmlDocument();
                doc.Load("Findinfo.xml");
                XmlElement root = doc.DocumentElement;
                XmlNode fileinfo = root["fileinfo"];
                info.FilePath = fileinfo["path"].InnerText;
                info.Template_name = fileinfo["template_name"].InnerText;
                info.Template_text = fileinfo["template_text"].InnerText;
                return info;
            }
            catch(Exception)
            {
                //Значит файл не создан, ничего страшного, при запуске создастся
                return null;
            }
            
        }
        public static void Write(string path, string template_name, string template_text)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Findinfo.xml");
                XmlElement root = doc.DocumentElement;
                XmlNode fileinfo = root["fileinfo"];
                fileinfo["path"].InnerText = path;
                fileinfo["template_name"].InnerText = template_name;
                fileinfo["template_text"].InnerText = template_text;
                doc.Save("Findinfo.xml");

            }
            catch(Exception)
            {
                XmlDocument doc = new XmlDocument();
                var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(xmlDeclaration);
                var root = doc.CreateElement("root");
                var fileinfo = doc.CreateElement("fileinfo");
                var _path = doc.CreateElement("path");
                var _template_name = doc.CreateElement("template_name");
                var _template_text = doc.CreateElement("template_text");
                fileinfo.AppendChild(_path);
                fileinfo.AppendChild(_template_name);
                fileinfo.AppendChild(_template_text);
                root.AppendChild(fileinfo);
                doc.AppendChild(root);
                doc.Save("FindInfo.xml");
                Write(path, template_name, template_text);
            }
        }
    }
}
