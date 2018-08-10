using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTomato
{
    public class TimerService
    {
        private DateTime endTime;
        public bool IsWorking { get; private set; } = false;
        private TimeSpan timeSpan;
        public event EventHandler<TimerEventArgs> TimeChanged;
        public event EventHandler<EventArgs> Success;
        public event EventHandler<EventArgs> Stopped;

        public TimerService()
        {
            timeSpan = TimeSpan.FromMinutes(25);
        }

        public void SetTimeSpan(TimeSpan ts)
        {
            timeSpan = ts;
        }

        public void Start()
        {
            if (IsWorking) return;
            IsWorking = true;
            endTime = DateTime.Now + timeSpan;
            Task.Factory.StartNew(MainThread);
        }

        private void MainThread()
        {
            TimeSpan ts;
            while (IsWorking)
            {
                ts = endTime - DateTime.Now;
                if (ts.TotalSeconds <= 0)
                {
                    IsWorking = false;
                    Success?.Invoke(this, new EventArgs());
                    break;
                }

                var remain = $"{ts.Hours}:{ts.Minutes.ToString("D2")}:{ts.Seconds}.{ts.Milliseconds.ToString().Substring(0,1)}";
                TimeChanged?.Invoke(this, new TimerEventArgs(remain));
                Thread.Sleep(30);
            }

            TimeChanged?.Invoke(this, new TimerEventArgs("0:00:00.0"));
        }

        public void Stop()
        {
            IsWorking = false;
            Stopped?.Invoke(this, new EventArgs());
        }
    }

    public class TimerEventArgs : EventArgs
    {
        public string RemainTimeString { get; private set; }

        public TimerEventArgs(string remain)
        {
            RemainTimeString = remain;
        }
    }
}
