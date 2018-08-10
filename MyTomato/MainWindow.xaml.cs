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
using System.Windows.Threading;

namespace MyTomato
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimerService timer = new TimerService();

        public MainWindow()
        {
            InitializeComponent();
            timer.TimeChanged += Timer_TimeChanged;
            timer.Success += Timer_Success;
            timer.Stopped += Timer_Stopped;
        }

        private void Timer_Stopped(object sender, EventArgs e)
        {
            NewButton.IsEnabled = true;
            TaskName.Text = "No Task!";
        }

        private void Timer_Success(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                MessageBox.Show("Success!");
            }));
        }

        private void Timer_TimeChanged(object sender, TimerEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                TimeText.Text = e.RemainTimeString;
            }));
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsWorking) return;

            var startWin = new NewTaskWin();
            startWin.Owner = this;
            startWin.ShowDialog();
            if (!startWin.IsOk) return;

            TaskName.Text = startWin.TaskName;
            timer.SetTimeSpan(startWin.Span);
            timer.Start();
            NewButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (!timer.IsWorking) return;

            var stopWin = new StopWin();
            stopWin.Owner = this;
            stopWin.ShowDialog();
            if (!stopWin.IsOk) return;

            timer.Stop();
            StopButton.IsEnabled = false;
        }
    }
}
