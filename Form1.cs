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

namespace ThreadingExperiment
{
    public partial class Form1 : Form
    {
        int start = 0;
        int middle = 5000;
        int end = 10000;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            btnWork.Text = "Working...";
            btnWork.Enabled = false;
            btnPrint.Enabled = false;

            //DoTimeConsumingWork();

            Thread workerThread = new Thread(DoTimeConsumingWork);
            workerThread.Start();

            while (start < middle || end > middle)
            {
                //Spin wheels
            }

            btnWork.Enabled = true;
            btnPrint.Enabled = true;
            btnWork.Text = "Job done!";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                lstbxNumbers.Items.Add(i);
            }
        }

        private void DoTimeConsumingWork()
        {
            Thread forward = new Thread(WorkForward);
            Thread backward = new Thread(WorkBackward);

            forward.Start();
            backward.Start();

        }

        private void WorkForward()
        {
            while (start < middle)
            {
                start++;
            }
        }
        private void WorkBackward()
        {
            while (end > middle)
            {
                end--;
            }
        }
    }
}
