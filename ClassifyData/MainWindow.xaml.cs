using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ClassifyData
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] arr = null;// 待分类的数据
        int groupNum = 1; //每组的元素数

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.tbResult.Text = null;
            var result = GetGroupLists(arr, groupNum);
            foreach (var item in result)
            {
                var rowData = string.Empty;
                foreach (var i in item)
                {
                    rowData += i + ",";
                }
                this.tbResult.Text += rowData + "\r\n";
            }
        }

        /// <summary>
        /// 将集合进行分组
        /// </summary>
        /// <param name="myList">原集合</param>
        /// <param name="GroupNum">每组的数量 ps:最后一组数量不足时按照剩余数量统计</param>
        public static List<List<string>> GetGroupLists(string[] myList, int GroupNum)
        {
            List<List<string>> listGroup = new List<List<string>>();
            int j = GroupNum;
            for (int i = 0; i < myList.Length; i += GroupNum)
            {
                List<string> cList = new List<string>();
                cList = myList.Take(j).Skip(i).ToList();
                j += GroupNum;
                listGroup.Add(cList);
            }
            return listGroup;
        }

        private void tbGroup_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void tbGroup_TextChanged(object sender, TextChangedEventArgs e)
        {
            groupNum = Convert.ToInt32(tbGroup.Text);
        }

        private void tbData_TextChanged(object sender, TextChangedEventArgs e)
        {
            arr = tbData.Text.Split(',');
        }

    }
}
