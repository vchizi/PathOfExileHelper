using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.WPF;
using PathOfExileHelper.Services;
using PathOfExileHelper.Utils;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using WindowsInput;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Immortality
{
    public class TimerFlaskUsageHandler: IFlaskUsageHandler
    {

        //private readonly Settings Settings;
        public List<Settings> SettingsList = new List<Settings>();

        //private Timer Timer;

        private List<Timer> Timers = new List<Timer>();

        public TimerFlaskUsageHandler(List<Settings> settingsList)
        {
            SettingsList = settingsList;
        }

        public TimerFlaskUsageHandler(List<Settings> settingsList, PathOfExileHelper.Settings settings)
        {
            SettingsList = settingsList;
        }

        public void Start() {
            Timers.Clear();
            foreach (var settings in SettingsList)
            {
                if (settings.Active == false)
                {
                    continue;
                }

                Timer timer = new Timer(1)
                {
                    AutoReset = false
                };
                timer.Elapsed += delegate (Object o, ElapsedEventArgs es)
                {
                    timer.Stop();
                    // PixelColor.GetColorFast(settings.Anchor.X, settings.Anchor.Y);
                    // Console.WriteLine(PixelColor.GetColor(settings.Anchor.X, settings.Anchor.Y));
                    uint test;
                    if (settings.Active && settings.Anchor.Pixel == PixelColor.GetColor(settings.Anchor.X, settings.Anchor.Y) && settings.UseFlask.Pixel != (test = PixelColor.GetColor(settings.UseFlask.X, settings.UseFlask.Y)) && POEWindow.IsWindowActive())
                    {
                        InputSimulator iSim = new InputSimulator();
                        iSim.Keyboard.KeyPress(settings.KeyToPress);

                        Console.WriteLine(DateTime.Now.ToString() + " - Used Flask: " + settings.KeyToPress + ". Timout: " + settings.Timeout);
                        Console.WriteLine("Config Color: " + PixelColor.GetHtmlColor(settings.UseFlask.Pixel));
                        Console.WriteLine("Used At Color: " + PixelColor.GetHtmlColor(test));
                        iSim = null;

                        System.Threading.Thread.Sleep(settings.Timeout);
                    }
                    try
                    {
                        timer.Start();
                    }
                    catch (ObjectDisposedException) { }
                };

                Console.WriteLine("Main timer started");
                timer.Start();
                Timers.Add(timer);
            }
        }

        public void Stop() {
            if (Timers.Count > 0)
            {
                foreach (Timer timer in Timers)
                {
                    timer.Stop();
                    timer.Dispose();
                }

                Timers.Clear();
            }
        }
    }
}
