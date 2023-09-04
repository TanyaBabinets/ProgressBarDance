using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace ProgressBarDance
{
    public partial class Form1 : Form
    {
        public SynchronizationContext uiContext;

        List<ProgressBar> pb = null;

        Random rand = new Random();
        

        public Form1()
        {
            InitializeComponent();
            uiContext = SynchronizationContext.Current;
            pb = new List<ProgressBar> { progressBar1, progressBar2, progressBar3, progressBar4, progressBar5 };
           

        }

        public void ThreadFunk(object obj)
        {

            ProgressBar pb2 = (ProgressBar)obj;


            uiContext.Send(d => pb2.Minimum = 0, null);
            uiContext.Send(d => pb2.Maximum = 230, null);
            uiContext.Send(d => pb2.Value = 0, null);          
            int count = 0;
            while (count != 200)
            {
                uiContext.Send(d => pb2.Value = rand.Next(1, 230), null);
                Thread.Sleep(100);
                count++;             
            }
        }
        private void button1_Click(object sender, EventArgs e) 
        {
            button1.Enabled = false;

            foreach (var t in pb)// Создание и запуск потоков для каждого ProgressBar
            {
                Thread thread = new Thread(new ParameterizedThreadStart(ThreadFunk));
                thread.IsBackground = true;
                thread.Start(t);

                Thread.Sleep(100);
            }            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

    }
}


