using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FindProgram.Models;

namespace FindProgram
{
    public partial class Form1 : Form
    {
        string savesearch = null;
        static long count = 0;
        public Form1()
        {
            InitializeComponent();
        }


        public void AddFindItem(string name)
        {
            FileListBox.Items.Add(name);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            savesearch = FileName.Text;
            FileName.Text = "";
            TimerLabel.Text = "";
            CountTestElement.Text = 0.ToString();
            Program.Pause();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }


        public void FileNameOutlut(string filename)
        {
            FileName.Text = filename;
            CountTestElement.Text = count++.ToString();
        }


        public void StartDefault()
        {
            Program.Start(start_directory.Text, template_name.Text, template_text.Text);
            FileListBox.Items.Clear();
            count = 0;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if ((template_name.Text != "" && template_text.Text != "") || (template_name.Text == "" && template_text.Text != "") || (template_name.Text != "" && template_text.Text == ""))
            {
                if (savesearch != null)
                {
                    Confirm confirm = new Confirm();
                    confirm.ShowDialog();
                }
                else
                {
                    Program.Start(start_directory.Text, template_name.Text, template_text.Text);
                }
                btnStop.Enabled = true;
                btnStart.Enabled = false;
            }
            else
            {
                MessageService.ShowMessageException(new Exception("Некоректно заданы критерии поиска"));
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Stop();
            XMLWorker.Write(start_directory.Text, template_name.Text, template_text.Text);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            FindInfo findInfo =  XMLWorker.Read();
            if(findInfo != null)
            {
                start_directory.Text = findInfo.FilePath;
                template_text.Text = findInfo.Template_text;
                template_name.Text = findInfo.Template_name;
            }
            btnStop.Enabled = false;
        }

        private void FileListBox_DoubleClick(object sender, EventArgs e)
        {
            if (FileListBox.Items.Count > 0)
            {
                int index = FileListBox.SelectedIndex;
                string param = FileListBox.Items[index].ToString();
                Process.Start("explorer", param);
                FileListBox.ClearSelected();
            }
        }
        public void Tick(int time)
        {
            TimerLabel.Text = time.ToString();
        }
    }
    
}
