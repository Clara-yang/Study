using LiveCharts;
using Plot.UC;
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
using System.Windows.Shapes;

namespace Plot
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class ConstantChangesChartMain : Window
    {
        private int index = 0;
        public ConstantChangesChartMain()
        {
            InitializeComponent();
            Task.Factory.StartNew(RecordData);
        }

        private void RecordData()
        {
            // 持续生成随机数，模拟数据处理过程
            while (true)
            {
                Thread.Sleep(500);
                var r = new Random();
                float phase = r.Next(1, 7); // 角度 纵轴值
                float modulus = r.Next(1, 10); // 模值 纵轴值
                // 更新图表数据
                constantChangesChart.PhaseValue = phase;
                constantChangesChart.ModulusValue = modulus;
                constantChangesChart.Id = index++;
            }
        }
    }


}
