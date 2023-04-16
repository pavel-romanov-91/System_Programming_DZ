using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multithreading_and_asynchrony_Part_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Task ProcessData(List<string> list,IProgress<ProgressBar> progress)
        {
            int index = 1;
            int totalProcess = list.Count; 
            var progressBar = new ProgressBar();
            return Task.Run(() =>
            {
                for (int i = 0; i < totalProcess; i++)
                {
                    progressBar.PercentComplete = index++ * 100 / totalProcess;
                    progress.Report(progressBar);
                    Thread.Sleep(10);
                }
            });
        }
    private async void btnStart_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i=0; i <1000; i++)
                list.Add(i.ToString());
            lbStatus.Text = "Working...";
            var progress = new Progress<ProgressBar>();
            progress.ProgressChanged += (o, report) =>
            {
                lbStatus.Text = string.Format("Processing...{0}%", report.PercentComplete);
                progressBar1.Value = report.PercentComplete;
                progressBar1.Update();
            };
            await ProcessData(list, progress);
            lbStatus.Text = "Done!";
                
                
        }
    }
}
