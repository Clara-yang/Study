using InteractiveDataDisplay.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Plot
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            LoadPoint();
            LoadPoint1();
            LoadPoint2();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int N = 25; // 点数
            Random r = new Random();
            double k;
            double[] x = new double[N];
            double[] y = new double[N];
            for (int i = 0; i < N; i++)
            {
                k = r.NextDouble();
                //x[i] = r.NextDouble() * 200;
                x[i] = k < 0.5 ? r.NextDouble() * 50 : -(r.NextDouble() * 50);
                y[i] = k < 0.5 ? r.NextDouble() * 50 : -(r.NextDouble() * 50);
            }
            barChart.PlotXY(x, y);
        }
        private void LoadPoint()
        {
            int N = 25; // 点数
            Random r = new Random();
            double k;
            double[] x = new double[N];
            double[] y = new double[N];
            for (int i = 0; i < N; i++)
            {
                k = r.NextDouble();
                x[i] = r.NextDouble() * 200;
                //x[i] = k < 0.5 ? r.NextDouble() * 12 - 1 : -(r.NextDouble() * 12 - 1); 
                y[i] = k < 0.5 ? r.NextDouble() * 10 : -(r.NextDouble() * 10);
            }
            circles.PlotXY(x, y);
        }
        private void LoadPoint1()
        {
            int N = 25; // 点数
            Random r = new Random();
            double k;
            double[] x = new double[N];
            double[] y = new double[N];
            for (int i = 0; i < N; i++)
            {
                k = r.NextDouble();
                //x[i] = r.NextDouble() * 200;
                x[i] = k < 0.5 ? r.NextDouble() * 20 : -(r.NextDouble() * 20);
                y[i] = r.NextDouble() * 200;
                //y[i] = k < 0.5 ? r.NextDouble() * 20 : -(r.NextDouble() * 20);
            }
            circles1.PlotXY(x, y);
        }
        private void LoadPoint2()
        {
            int N = 25; // 点数
            Random r = new Random();
            double k;
            double[] x = new double[N];
            double[] y = new double[N];
            for (int i = 0; i < N; i++)
            {
                k = r.NextDouble();
                x[i] = r.NextDouble() * 200;
                //x[i] = k < 0.5 ? r.NextDouble() * 20 : -(r.NextDouble() * 20);
                y[i] = r.NextDouble() * 200;
                //y[i] = k < 0.5 ? r.NextDouble() * 20 : -(r.NextDouble() * 20);
            }
            circles2.PlotXY(x, y);
        }
    }
}
