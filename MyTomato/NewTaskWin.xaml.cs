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
    /// NewTaskWin.xaml 的交互逻辑
    /// </summary>
    public partial class NewTaskWin : Window
    {
        public bool IsOk { get; private set; } = false;
        public string TaskName { get { return TaskCombo.Text; } }

        public TimeSpan Span { get; private set; }

        public NewTaskWin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TaskCombo.Text))
                    throw new Exception("番茄名称不能为空!");
                Span = TimeSpan.Parse(TimeText.Text);

                IsOk = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IsOk = false;
            Close();
        }
    }
}
