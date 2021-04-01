using FontAwesome.WPF;
using PathOfExileHelper.Services;
using PathOfExileHelper.Utils;
using System;
using System.Timers;
using System.Windows;
using WindowsInput;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Immortality
{
    public class ImmortalityButton : Button
    {
        private readonly Settings Settings;

        private Timer Timer;

        private ImmortalitySettings ImmortalitySettings;

        public ImmortalityButton(PathOfExileHelper.Settings settings) : base()
        {
            Settings = Settings.Load(settings);
            if (Settings.Anchor == null || Settings.UseFlask == null)
                SettingsRequired = true;

            ButtonControl.ControlButton.Click += ButtonClick;
            ButtonControl.ControlButton.MouseRightButtonUp += RightButtonClick;
        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (Settings.Anchor == null || Settings.UseFlask == null)
            {
                OpenSettingsWindow();

                return;
            }

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
            OpenSettingsWindow();
        }

        public void OpenSettingsWindow()
        {
            if (ImmortalitySettings == null)
            {
                ImmortalitySettings = new ImmortalitySettings(Settings);
                ImmortalitySettings.Closed += SettingWindowClosed;

                ImmortalitySettings.Show();
            } 
            else
            {
                ImmortalitySettings.Close();
                ImmortalitySettings = null;
            }
        }

        protected override FontAwesomeIcon Icon()
        {
            return FontAwesomeIcon.Flask;
        }

        private void SettingWindowClosed(object sender, EventArgs e)
        {
            if (Settings.Anchor != null && Settings.UseFlask != null)
                SettingsRequired = false;
        }

        private void StartTimer()
        {
            Timer = new Timer(1)
            {
                AutoReset = true
            };

            Timer.Elapsed += delegate (Object o, ElapsedEventArgs es)
            {
                if (POEWindow.IsWindowActive() && Settings.Anchor.Pixel == PixelColor.GetColor(Settings.Anchor.X, Settings.Anchor.Y) && Settings.UseFlask.Pixel != PixelColor.GetColor(Settings.UseFlask.X, Settings.UseFlask.Y))
                {
                    InputSimulator iSim = new InputSimulator();
                    iSim.Keyboard.KeyPress(VirtualKeyCode.VK_1);

                    iSim = null;

                    Timer.Stop();
                    System.Threading.Thread.Sleep(50);
                    Timer.Start();
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
