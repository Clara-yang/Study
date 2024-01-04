using MVVMBinding.ViewModel;
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

namespace MVVMBinding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel vm=new MainViewModel();    
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        } 

        private void turbulence_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (turbulence.Angle >= -60 && turbulence.Angle < 120)
            {
                turbulence.Angle += 45;
            }
            else
            {
                turbulence.Angle = -60;
            }
            vm.TurbulenceChanged(turbulence.Angle.ToString());
        }

        private void sd_wind_height_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            vm.HollowHeightChanged(sd_wind_height.Value.ToString());
        }

        private void clearWind_Click(object sender, RoutedEventArgs e)
        {
            turbulence.Angle = -60;
            vm.WindClear();
        }
    }
}
