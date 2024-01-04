using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Plot.UC
{
    /// <summary>
    /// ConstantChangesChart.xaml 的交互逻辑
    /// </summary>
    public partial class ConstantChangesChart : UserControl
    {
        public ChartValues<MeasureModel> PhaseChartValues { get; set; }
        public ChartValues<MeasureModel> ModulusChartValues { get; set; }
        public float PhaseValue { get; set; }
        public float ModulusValue { get; set; }

        // 当数值被更改时，触发更新
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                Read();
            }
        }

        public ConstantChangesChart()
        {
            InitializeComponent();

            // 设置图表的XY轴数值对应
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.Index)  // Index做X轴
                .Y(model => model.Value); // Value做Y轴
            Charting.For<MeasureModel>(mapper);  // 声明绑定，固定写法
            PhaseChartValues = new ChartValues<MeasureModel>();
            ModulusChartValues = new ChartValues<MeasureModel>();

            DataContext = this;
        }

        // 更新图表
        private void Read()
        {
            PhaseChartValues.Add(new MeasureModel
            {
                Index = this.Id,
                Value = this.PhaseValue
            });
            ModulusChartValues.Add(new MeasureModel
            {
                Index = this.Id,
                Value = this.ModulusValue
            });

            // 限定图表最多只有十五个元素
            if (PhaseChartValues.Count > 15)
            {
                PhaseChartValues.RemoveAt(0);
                ModulusChartValues.RemoveAt(0);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
    // 数据点格式，给X Y轴赋值
    public class MeasureModel
    {
        public int Index { get; set; }
        public float Value { get; set; }
    }
}
