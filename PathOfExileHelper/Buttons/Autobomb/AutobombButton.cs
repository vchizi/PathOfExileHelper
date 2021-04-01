using FontAwesome.WPF;
using PathOfExileHelper.Services;
using System;
using System.Timers;
using System.Windows;
using WindowsInput;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Autobomb
{
    public class AutobombButton : Button
    {
        private Timer Timer;

        private AutobombSettings AutobombSettings;

        private readonly Settings Settings;

        public AutobombButton(PathOfExileHelper.Settings settings) : base()
        {
            Settings = Settings.Load(settings);

            ButtonControl.ControlButton.Click += ButtonClick;
            ButtonControl.ControlButton.MouseRightButtonUp += RightButtonClick;
        }

        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (Activated)
            {
                StartTimer();
            }
            else
            {
                StopTimer();
            }
        }

        public void RightButtonClick(object sender, RoutedEventArgs e)
        {
            if (AutobombSettings != null)
            {
                AutobombSettings.Close();
                AutobombSettings = null;
            }
            else
            {
                AutobombSettings = new AutobombSettings(Settings.Interval, Settings.KeyToPress)
                {
                    Topmost = true,
                };

                AutobombSettings.SaveButton.Click += AutobombSettingsSaved;
                AutobombSettings.Closed += delegate (Object o, EventArgs es)
                {
                    AutobombSettings = null;
                };

                AutobombSettings.Show();
            }
        }

        private void AutobombSettingsSaved(object sender, RoutedEventArgs e)
        {
            Settings.KeyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), AutobombSettings.Keys.SelectedValue.ToString());
            Settings.Interval = int.Parse(AutobombSettings.Interval.Text);
            Settings.Save();

            if (Activated)
            {
                StopTimer();
                StartTimer();
            }

            AutobombSettings.Close();
            AutobombSettings = null;
        }

        protected override FontAwesomeIcon Icon()
        {
            return FontAwesomeIcon.Bomb;
        }

        private void StartTimer()
        {
            Timer = new Timer(Settings.Interval)
            {
                AutoReset = true
            };

            Timer.Elapsed += delegate (Object o, ElapsedEventArgs es)
            {
                if (POEWindow.IsWindowActive())
                {
                    InputSimulator iSim = new InputSimulator();
                    iSim.Keyboard.KeyPress(Settings.KeyToPress);

                    iSim = null;
                }
            };
            Timer.Start();
        }

        private void StopTimer()
        {
            if (Timer != null)
            {
                Timer.Stop();
                Timer.Close();
            }
        }
    }
}
