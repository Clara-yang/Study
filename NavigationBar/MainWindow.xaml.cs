using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace WpfDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Activated += w_Activated;
            this.Closing += w_Closing;
            this.ContentRendered += w_ContentRendered;
            this.Deactivated += w_Deactivated;
            this.Loaded += w_Loaded;
            this.Closed += w_Closed;
            this.Unloaded += w_Unloaded;
            this.SourceInitialized += w_SourceInitiated;

            InitializeComponent();
        }

        void w_SourceInitiated(object sender,EventArgs e) { Console.WriteLine("1---SourceInitiated资源发起"); }
        void w_Unloaded(object sender,EventArgs e) { Console.WriteLine("2---Unloaded卸载"); }
        void w_Closed(object sender,EventArgs e) { Console.WriteLine("3---Closed已关闭"); }
        void w_Loaded(object sender,EventArgs e) { Console.WriteLine("4---加载"); }
        void w_Deactivated(object sender,EventArgs e) { Console.WriteLine("5---停用"); }
        void w_ContentRendered(object sender,EventArgs e) { Console.WriteLine("6---内容呈现"); }
        void w_Closing(object sender,EventArgs e) { Console.WriteLine("7---关闭"); }
        void w_Activated(object sender,EventArgs e) { Console.WriteLine("8---激活"); }

        //public abstract class DispatcherObject : DispatcherObjectBase
        //{
        //    public DispatcherOperation BeginInvoke(Delegate method, DispatcherPriority priority, params object[] args);
        //}

        private void ModifyUI()
        {
            // 模拟正在运行的一些工作
            Thread.Sleep(TimeSpan.FromSeconds(2));
            // 子线程中不可以直接更改UI线程创建的对象  lbHello.Content = "欢迎光临Dispatcher"; 
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                //lbHello.Content = "欢迎Dispatcher同步方法！";
            });
        }

        private void btnThd_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(ModifyUI);
            thread.Start();
        }

        private void btnAppBeginInvoke_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    //this.lbHello.Content = "欢迎Dispatcher异步方法！" + DateTime.Now.ToString();
                }));
            }).Start();
        }

        private void btnByCode_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
