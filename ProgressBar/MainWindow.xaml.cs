using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgressBar
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker bgworker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            InitWork();
            bgworker.RunWorkerAsync();
            //ProgressBegin();
        }

        /// <summary>
        /// 初始化bgwork
        /// </summary>
        private void InitWork()
        {
            bgworker.WorkerReportsProgress = true;
            bgworker.DoWork += new DoWorkEventHandler(DoWork);
            bgworker.ProgressChanged += new ProgressChangedEventHandler(BgworkChange);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                bgworker.ReportProgress(i);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        ///改变进度条的值
        /// </summary>
        private void BgworkChange(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }



        private void ProgressBegin()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    this.progressBar1.Dispatcher.BeginInvoke((ThreadStart)delegate { this.progressBar1.Value = i; });
                    Thread.Sleep(100);
                }
            }));
            thread.Start();
        }
         
    }
}
