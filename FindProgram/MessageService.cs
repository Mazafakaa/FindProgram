using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindProgram
{
    public static class MessageService
    {
        public static void ShowMessageException(Exception ex)
        {
            MessageBox.Show("Во время выполнения программы произошла ошибка: \n" + ex.Message + "\n Трасировка стэка: \n" + ex.StackTrace);   
        }

    }
}
