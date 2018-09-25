using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Timers = System.Timers;

namespace BatteryHealth
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _batteryLevel;
        private readonly Timers.Timer _timer;
        private readonly PowerStatus _powerStatus = SystemInformation.PowerStatus;

        public MainWindow()
        {
            _timer = new Timers.Timer(1000 * 60); // 1 Minute.
            _timer.AutoReset = true;

            _timer.Elapsed += (e, t) =>
            {
                ReadBatterChargeLevel();
            };

            InitializeComponent();

            SetStartingPosition();
            ReadBatterChargeLevel();

            _timer.Start();

        }

        private void SetStartingPosition()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;

            Left = desktopWorkingArea.Right - Width - 5;
            Top = desktopWorkingArea.Bottom - Height;
            Topmost = true;
        }

        protected override void OnMouseDown(MouseButtonEventArgs arg)
        {
            if(arg.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public int BatteryLevel
        {
            get
            {
                return _batteryLevel;
            }
            set
            {
                _batteryLevel = value;
                OnPropertyChanged(nameof(BatteryLevel));
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ReadBatterChargeLevel()
        {
            BatteryLevel = (int)Math.Floor((_powerStatus.BatteryLifePercent * 100));
        }

        private void ContextMenu_Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
