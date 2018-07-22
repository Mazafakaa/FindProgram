using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
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

        public void AddFindItem(string fname, TreeNode node)
        {
            try
            {
                string str = "";
                string[] name = fname.Split(Path.DirectorySeparatorChar);
                if (name.Length > 1)
                {
                    str += name[1];
                    for (int i = 2; i < name.Length; i++)
                    {
                        str += Path.DirectorySeparatorChar + name[i];
                    }
                }
                if(node == null && name.Length > 0)
                {
                    node = FileItemsTree.Nodes[name[0]];
                    if(node == null)
                    {
                        node = FileItemsTree.Nodes.Add(name[0], name[0]);
                        if (str != "")
                        {
                            AddFindItem(str, node);
                        }
                    }
                    else
                    {
                        if (str != "")
                        {
                            AddFindItem(str, node);
                        }
                    }
                }
                else if(node != null && name.Length > 0)
                {
                    TreeNode tn = node.Nodes[name[0]];
                    if(tn == null)
                    {
                        tn = node.Nodes.Add(name[0], name[0]);
                        if (str != "")
                        {
                            AddFindItem(str, tn);
                        }
                    }
                    else
                    {
                        if (str != "")
                        {
                            AddFindItem(str, tn);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageService.ShowMessageException(ex);
            }
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

        public void AddItem(string name)
        {
            AddFindItem(name, null);
        }
        public void StartDefault()
        {
            Program.Start(start_directory.Text, template_name.Text, template_text.Text);
            FileItemsTree.Nodes.Clear();
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
    }
    
}
