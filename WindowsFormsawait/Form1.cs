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

namespace WindowsFormsawait
{
    public partial class Form1 : Form
    {
        Button b1;
        Button b2;
        Label l;
        public Form1()
        {
            InitializeComponent();
			b1 = new Button();
			b1.Text = "Perform long operation";
			b1.Dock = DockStyle.Top;
			b1.Click += new EventHandler(B1_Click);
			Controls.Add(b1);

			b2 = new Button();
			b2.Text = "Show Message";
			b2.Dock = DockStyle.Top;
			b2.Click += new EventHandler(B2_Click);
			Controls.Add(b2);

			l = new Label();
			l.Text = "Wait";
			l.Dock = DockStyle.Bottom;
			Controls.Add(l);
		}
		private async void B1_Click(object sender, EventArgs e)
		{
			//DoLongOperation();
			b1.Enabled = false;
			l.Text = "Doing...";
			//Thread.Sleep(10000);
			var result = await Task<string>.Factory.StartNew(() =>
			{
				//Task.Delay(10000);
				Thread.Sleep(10000);
				return "Done";
			});
			
			l.Text = result;
			b1.Enabled = true;
		}

		private void B2_Click(object sender, EventArgs e)
		{
			MessageBox.Show("message");
		}

		private async void DoLongOperation()
		{
			b1.Enabled = false;
			l.Text = "Doing...";
			//Thread.Sleep(10000);
			var result = await Task<string>.Factory.StartNew(() =>
			{
				//Task.Delay(10000);
				Thread.Sleep(10000);
				return "Done";
			});

			l.Text = result;
			b1.Enabled = true;
		}
		private CancellationTokenSource cts;
		private async void button1_Click(object sender, EventArgs e)
		{
			cts = new CancellationTokenSource();
			try
			{
				await Task.Run(() =>
				{
					try
					{
						//註冊執行續中斷事件
						using (cts.Token.Register(Thread.CurrentThread.Abort))
						{
							//執行大量運算
							Thread.Sleep(1000000);
						}
					}
					catch (ThreadAbortException)
					{
						MessageBox.Show("已暫停");
					}

				}, cts.Token);
			}
			catch
			{
				
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {
			cts.Cancel();

		}
    }
}
