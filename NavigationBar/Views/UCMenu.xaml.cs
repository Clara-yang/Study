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
using WpfDemo.ViewModels;

namespace WpfDemo.Views
{
    /// <summary>
    /// UCMenu.xaml 的交互逻辑
    /// </summary>
    public partial class UCMenu : UserControl
    {
        MaterialMenuView _menuView;
        public UCMenu(ItemMenu itemMenu, MaterialMenuView menuView)
        {
            InitializeComponent();

            _menuView = menuView;

            ExpanderMenu.Visibility = itemMenu == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _menuView.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
        }
    }
}
