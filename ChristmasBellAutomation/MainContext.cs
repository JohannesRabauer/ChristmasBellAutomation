using ChristmasBellAutomation.Helper;
using System;
using System.Threading;
using System.Windows.Media;

namespace ChristmasBellAutomation
{
    public enum TimeType
    {
        Countdown,
        SpecificTime
    }
    class MainContext : BaseModel
    {

        private TimeType _selectedTimeType;

        public TimeType SelectedTimeType
        {
            get { return _selectedTimeType; }
            set { _selectedTimeType = value;
                OnPropertyChanged("SelectedTimeType");
            }
        }

        private bool _doRepeat;

        public bool DoRepeat
        {
            get { return _doRepeat; }
            set { _doRepeat = value;
                OnPropertyChanged("DoRepeat");
            }
        }

        private DateTime _specificTime;

        public DateTime SpecificTime
        {
            get { return _specificTime; }
            set { _specificTime = value;
                OnPropertyChanged("SpecificTime");
            }
        }


        private int _repeatCount;

        public int RepeatCount
        {
            get { return _repeatCount; }
            set { _repeatCount = value;
                OnPropertyChanged("RepeatCount");
            }
        }

        private int _repeatInterval;

        public int RepeatInterval
        {
            get { return _repeatInterval; }
            set { _repeatInterval = value;
                OnPropertyChanged("RepeatInterval");
            }
        }


        private TimeSpan _timeLeft;
        public TimeSpan TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value;
                OnPropertyChanged("TimeLeft");
            }
        }

        private int _countdownTimeInSeconds;

        public int CountdownTimeInSeconds
        {
            get { return _countdownTimeInSeconds; }
            set { _countdownTimeInSeconds = value;
                OnPropertyChanged("CountdownTimeInSeconds");
            }
        }


        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged("IsRunning");
            }
        }

        private Timer _bellTimer;
        private int _repeatedCount;


        public RelayCommand StartStopCommand { get; set; }

        public MainContext(int countdownTimeInSeconds)
        {
            this.StartStopCommand = new RelayCommand(param => startStop());
            this.IsRunning = false;
            this.CountdownTimeInSeconds = countdownTimeInSeconds;
            this.SpecificTime = DateTime.Now.AddHours(1);
        }

        private void startStop()
        {
            if (this._isRunning)
                stop();
            else
                start();
        }

        private void start()
        {
            this.IsRunning = true;
            TimeSpan newTimeLeft;
            if (this._selectedTimeType.Equals(TimeType.Countdown))
                newTimeLeft = new TimeSpan(0, 0, this._countdownTimeInSeconds);
            else
                newTimeLeft = this._specificTime.Subtract(DateTime.Now);
            newTimeLeft=new TimeSpan(newTimeLeft.Days,newTimeLeft.Hours,newTimeLeft.Minutes,newTimeLeft.Seconds);
            this.TimeLeft = newTimeLeft;
            this._bellTimer = new Timer(new TimerCallback(timerTick), null, 1000, 1000);
            this._repeatedCount = 0;
        }

        private void stop()
        {
            this.IsRunning = false;
            this._bellTimer.Dispose();
        }

        private void timerTick(object obj)
        {
            if (this.TimeLeft.TotalSeconds > 0)
            {
                this.TimeLeft = this.TimeLeft.Add(new TimeSpan(0, 0, -1));
            }else
            {
                playSound();
                if (this._doRepeat && this._repeatCount > this._repeatedCount)
                {
                    this._repeatedCount++;
                   this.TimeLeft= this.TimeLeft.Add(new TimeSpan(0, 0, this._repeatInterval));
                }else {
                    stop();
                }
            }
        }

        private void playSound()
        {
            MediaPlayer bellSound = new MediaPlayer();
           // bellSound.Open(new Uri(new Uri(System.Reflection.Assembly.GetExecutingAssembly().Location), @"174635__altfuture__wood-knocking.wav"));
            bellSound.Open(new Uri(new Uri(System.Reflection.Assembly.GetExecutingAssembly().Location), @"30157__herbertboland__belllittleshake.wav"));
            //Sound from https://freesound.org/people/HerbertBoland/sounds/30157/
            bellSound.Play();
        }
    }
}
