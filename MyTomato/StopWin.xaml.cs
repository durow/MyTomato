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

namespace MyTomato
{
    /// <summary>
    /// StopWin.xaml 的交互逻辑
    /// </summary>
    public partial class StopWin : Window
    {
        public bool IsOk { get; private set; } = false;
        public string Reason { get { return ReasonText.Text; } }

        public StopWin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(ReasonText.Text))
            {
                MessageBox.Show("放弃原因不能为空!");
                return;
            }

            IsOk = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            Close();
        }
    }
}
