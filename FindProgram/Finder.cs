using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace FindProgram
{
    class Finder : IDisposable
    {
        private string path;
        private string template_name;
        private string template_text;
        public Finder(string path, string template_name, string template_text)
        {
            this.path = path;
            this.template_name = template_name;
            this.template_text = template_text;
        }
        public void Start()
        {
            Find(path, template_name, template_text);
        }
        public void Find(string path, string template_name, string template_text)
        {
            DirectoryInfo info;
            DirectoryInfo[] dirs;
            FileInfo[] files;

            
            if (path == "")
            {
                string[] drives = Environment.GetLogicalDrives();
                dirs = new DirectoryInfo[drives.Length];
                for (int i = 0; i < drives.Length; i++)
                {
                    Find(drives[i], template_name, template_text);
                    
                }
                files = null;
            }
            else
            {
                info = new DirectoryInfo(path);
                dirs = info.GetDirectories();
                files = info.GetFiles();
                int dirCount = 0;
                while (dirs != null && dirs.Length > dirCount)
                {
                    try
                    {
                        if (dirs[dirCount].ToString()[0] != '$')
                        {
                            Find(path + @"\" + dirs[dirCount].ToString(), template_name, template_text);
                        }
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        dirCount++;
                    }
                }

                foreach (var t in files)
                {



                    if (template_text != null && template_name != null)
                    {
                        if (t.Length < 20971520)
                        {


                            Program.FileNameOutlut(path + @"\" + t);
                            string content = File.ReadAllText(path + @"\" + t, Encoding.UTF8);
                            if (content.Contains(template_text))
                            {
                                Program.AddFindFile(path + @"\" + t);
                            }
                            else if ((Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(content))).Contains(template_text))
                            {
                                Program.AddFindFile(path + @"\" + t);
                            }
                            content = null;
                        }
                    }
                    else if (template_text == null && template_name != null)
                    {
                        if (t.ToString().Contains(template_name))
                        {
                            Program.AddFindFile(path + @"\" + t);
                        }

                    }
                    else
                    {
                        if (t.Length < 20971520)
                        {
                            string content = File.ReadAllText(path + @"\" + t, Encoding.UTF8);
                            if (content.Contains(template_text))
                            {
                                Console.WriteLine(path + @"\" + t);
                            }
                            else if ((Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(content))).Contains(template_text))
                            {
                                Console.WriteLine(path + @"\" + t);
                            }
                            content = null;
                        }
                    }

                }
            }


        }
        public void Dispose()
        {
            if (this != null)
            {
                Dispose();
            }
        }
    }
}
