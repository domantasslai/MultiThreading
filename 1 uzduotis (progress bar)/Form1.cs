using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace _1_uzduotis__progress_bar_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("*.exe");
            comboBox1.Items.Add("*.txt");
            comboBox1.Items.Add("*.pdf");
            comboBox1.Items.Add("*.docx");
            comboBox1.Items.Add("*.pptx");
            comboBox1.Items.Add("*.xlsx");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;
            folderBrowserDialog1.Description = "Select the searching directory";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var elapsedTime = watch.ElapsedMilliseconds;
			watch.Start();
			Thread tr = new Thread(ThreadCoordinator2);
            tr.Start();
			watch.Stop();
			textBox3.Text = elapsedTime.ToString() + " ms";
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        Thread fileSearchThread;

        private void ThreadCoordinator2()
        {
            search fileSearch = new search();
            fileSearchThread = new Thread(() => { fileSearch.ShowAllFoldersUnder(textBox2.Text, textBox1.Text); });

            fileSearchThread.Start();

            Thread progressThread = new Thread(() => { listBoxPildymas2(fileSearch.filesFound); });
            progressThread.Start();

            fileSearchThread.Join();
            progressThread.Join();
        }
        private void listBoxPildymas2(Object obj)
        {
            //int praeitosDir = directoryProgress.DirCount;
            List<string> paths = (List<string>)obj;
            int lastPathNumber = paths.Count;
            int lastShownPathNumber = 0;
            /*while (directoryProgress.DirCount <= 1)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    progressBar1.Maximum = directoryProgress.GetMaxDir(textBox1.Text, 2);
                });
                Thread.Sleep(10);
            }*/
            //MessageBox.Show(""+progressBar1.Maximum);
            while (fileSearchThread.IsAlive || (lastShownPathNumber < lastPathNumber))
            {
                if (lastShownPathNumber < lastPathNumber)
                {
                    lastShownPathNumber++;
                    this.Invoke((MethodInvoker)delegate
                    {
                        listBox1.Items.Add(lastShownPathNumber + " " + paths[lastShownPathNumber - 1]);
                    });
                }
                //progressDisplay(directoryProgress.DirCount);
                Thread.Sleep(1);
                lastPathNumber = paths.Count;
            }
            this.BeginInvoke((MethodInvoker)delegate
            {
                // MessageBox.Show("Fini");
                progressBar1.Value = progressBar1.Maximum;
            });
        }
    }
}


