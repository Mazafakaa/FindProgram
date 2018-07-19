using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FindProgram
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(StartForm());
        }
        static Timer timer = new Timer();
        static bool isPaused = false;
        static Finder finder;
        static Form1 form;
        static Thread thread;
        private static Form1 StartForm()
        {
            form = new Form1();
            return form;
        }
        public static void AddFindFile(string name)
        {

            string path = "";
            string[] str = name.Split(Char.Parse(@"\"));
            path += str[0];
            for(int i = 1; i < str.Length; i++)
            {
                if(str[i] != "")
                {
                    path += @"\" + str[i];
                }
            }
            
            form.AddFindItem(path);
        }


        public static void FileNameOutlut(string name)
        {
            string path = "";
            string[] str = name.Split(Char.Parse(@"\"));
            path += str[0];
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] != "")
                {
                    path += @"\" + str[i];
                }
            }
            form.FileNameOutlut(path);
        }


        public static void Pause()
        {
            if (!isPaused)
            {
                isPaused = true;
                thread.Suspend();
                timer.Stop();
            }
            
            
        }


        public static void Play()
        {
            if(isPaused)
            {
                isPaused = false;
                thread.Resume();
                timer.Start(timer.time);
            }
            
            
        }
        public static void Stop()
        {
            if(thread != null)
            {

                Play();
                timer.Stop();
                thread.Abort();
                thread = null;
                
            }
                
            
        }
        public static void Start(string path, string template_name, string template_text)
        {
            
            Stop();
            finder = new Finder(path, template_name, template_text);
            if(thread == null)
            {
                thread = new Thread(finder.Start);
                thread.Start();
                XMLWorker.Write(path, template_name, template_text);
                timer.Start();
            }
            
        }
        public static void StartDefult()
        {
            form.StartDefault();
        }
        

        public static void Tick(int time)
        {
            form.Tick(time);
        }
    }
}
