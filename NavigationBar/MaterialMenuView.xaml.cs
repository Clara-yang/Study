using MaterialDesignThemes.Wpf;
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
using WpfDemo.ViewModels;
using WpfDemo.Views;

namespace WpfDemo
{
    /// <summary>
    /// MaterialMenuView.xaml 的交互逻辑
    /// </summary>
    public partial class MaterialMenuView : Window
    {
        public MaterialMenuView()
        {
            InitializeComponent();

            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("1", new UCCostumers()));
            menuRegister.Add(new SubItem("2", new UCProviders()));
            menuRegister.Add(new SubItem("3"));
            menuRegister.Add(new SubItem("4"));
            var item1 = new ItemMenu("分组1", menuRegister, PackIconKind.Register);

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("1", new UCCostumers()));
            menuSchedule.Add(new SubItem("2", new UCProviders()));
            menuSchedule.Add(new SubItem("3"));
            menuSchedule.Add(new SubItem("4"));
            var item2 = new ItemMenu("分组2", menuSchedule, PackIconKind.Schedule);



            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("1", new UCCostumers()));
            menuReports.Add(new SubItem("2", new UCProviders()));
            menuReports.Add(new SubItem("3"));
            menuSchedule.Add(new SubItem("4"));
            var item3 = new ItemMenu("分组3", menuReports, PackIconKind.FileReport); 

            Menu.Children.Add(new UCMenu(item1,this));
            Menu.Children.Add(new UCMenu(item2,this));
            Menu.Children.Add(new UCMenu(item3,this));   
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender); 
            if (screen != null)
            {
                StackPanelMain.Children.Clear();    
                StackPanelMain.Children.Add(screen);
            }

        }

    }
}
