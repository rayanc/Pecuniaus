using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Pecuniaus.ServiceHost
{
    public class ServiceTimer
    {
        private Timer _timer;

        public Timer Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; }
        }
    }
}
