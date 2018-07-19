using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FindProgram
{
    class Timer
    {
        Thread thread = null;
        public int time;
        public void Start(int starttime = 0)
        {
            time = starttime;
            thread = new Thread(Run);
            thread.Start();
        }
        private void Run()
        {
            while(true)
            {
                Thread.Sleep(1000);
                time++;
                Program.Tick(time);
            }
        }
        public void Stop()
        {
            if(thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

    }
}
