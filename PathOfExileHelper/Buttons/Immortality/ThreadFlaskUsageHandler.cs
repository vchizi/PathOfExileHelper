using PathOfExileHelper.Events;
using PathOfExileHelper.Services;
using PathOfExileHelper.UserControls;
using PathOfExileHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using static System.Net.Mime.MediaTypeNames;

namespace PathOfExileHelper.Buttons.Immortality
{
    public class ThreadFlaskUsageHandler : IFlaskUsageHandler
    {
        private const int ACTIVE_WINDOW_CHECK_TIMEOUT = 50;

        private static string Pattern = @"\]\s[#&]?(<.*\>\s)?:\s(You have entered ){1}(?<zoneName>.*).$";
        private static Regex Rgx = new Regex(Pattern);

        private bool activeWindow = true;
        private bool zoneWithoutFlask = true;

        public List<Settings> SettingsList = new List<Settings>();

        private List<Thread> WorkerThreads = new List<Thread>();

        private Dictionary<string, bool> ZonesWithoutFlask = new Dictionary<string, bool>() {
            {"Lioneye's Watch", true},
            {"The Forest Encampment", true},
            {"The Sarn Encampment", true},
            {"Overseer's Tower", true},
            {"The Bridge Encampment", true},
            {"Highgate", true},
            {"Oriath Docks", true},
            {"Aspirants' Plaza", true},
            {"The Rogue Harbour", true},
            {"Karui Shores", true},
            //{"The Menagerie", false}, can't be without flask we have fights here
            {"Azurite Mine", true},
            {"Coastal Hideout", true}, //ignore all hideouts
        };

        Thread ActiveWindowThread;

        ClientTxtReader ClientTxtReader;

        public ThreadFlaskUsageHandler(List<Settings> settingsList)
        {
            SettingsList = settingsList;

            StartActiveWindowMonitoring();
        }

        public ThreadFlaskUsageHandler(List<Settings> settingsList, PathOfExileHelper.Settings settings)
        {
            SettingsList = settingsList;

            ClientTxtReader = new ClientTxtReader(settings.Main.ClientTxtPath);
            ClientTxtReader.StartReading();

            ClientTxtReader.NewLineAdded += HandleNewLine;

            StartActiveWindowMonitoring();
        }

        
        private void StartActiveWindowMonitoring()
        {
            ActiveWindowThread = new Thread(new ThreadStart(ActiveWindow));
            ActiveWindowThread.Name = "Thread #ActiveWindow";
            ActiveWindowThread.Start();
            Console.WriteLine(DateTime.Now.ToString() + " - Starting thread: " + ActiveWindowThread.Name);
        }

        public void Start()
        {
            Console.WriteLine(DateTime.Now.ToString() + " - Zone without flask: " + zoneWithoutFlask);
            if (activeWindow == false || zoneWithoutFlask == true) { return; }

            //Stop();

            WorkerThreads = new List<Thread>();

            uint number = 1;
            foreach (Settings settings in SettingsList)
            {
                if (settings.Active == false)
                {
                    continue;
                }

                Thread workerThread = new Thread(new ParameterizedThreadStart(FlaskUse));
                workerThread.Name = "Thread #" + number;
                workerThread.Start(settings);

                Console.WriteLine(DateTime.Now.ToString() + " - Starting thread: " + workerThread.Name);

                WorkerThreads.Add(workerThread);

                number++;
            }
        }

        public void FlaskUse(object obj)
        {
            try
            {
                Settings settings = (Settings)obj;
                while (true)
                {
                    uint color = PixelColor.GetColor(settings.UseFlask.X, settings.UseFlask.Y);
                    if (settings.Anchor.Pixel == PixelColor.GetColor(settings.Anchor.X, settings.Anchor.Y) && settings.UseFlask.Pixel != color)
                    {
                        InputSimulator iSim = new InputSimulator();
                        iSim.Keyboard.KeyPress(settings.KeyToPress);

                        Console.WriteLine(DateTime.Now.ToString() + " - Used Flask: " + settings.KeyToPress + ". Timout: " + settings.Timeout);
                        Console.WriteLine(DateTime.Now.ToString() + " - Config Color: " + PixelColor.GetHtmlColor(settings.UseFlask.Pixel));
                        Console.WriteLine(DateTime.Now.ToString() + " - Used At Color: " + PixelColor.GetHtmlColor(color));
                        iSim = null;

                        Thread.Sleep(settings.Timeout);
                    }
                }
            } catch (ThreadAbortException) {
                Console.WriteLine(DateTime.Now.ToString() + $" - { Thread.CurrentThread.Name} Has Finished its Execution");
            }
        }

        public void ActiveWindow() {
            while (true)
            {
                if (POEWindow.IsWindowActive() == true && activeWindow == false && zoneWithoutFlask == false)
                {
                    activeWindow = true;
                    Start();
                } else if (POEWindow.IsWindowActive() == false) {
                    activeWindow = false;
                    Stop();
                }

                Thread.Sleep(ACTIVE_WINDOW_CHECK_TIMEOUT);
            }
        }

        public void Stop() {
            foreach (var workerThread in WorkerThreads)
            {
                if (workerThread.IsAlive)
                {
                    Console.WriteLine(DateTime.Now.ToString() + " - Stoping thread: " + workerThread.Name);
                    try
                    {
                        workerThread.Abort();
                    }catch (ThreadAbortException) { }
                }
            }
        }

        public void HandleNewLine(object sender, NewLineEvent e)
        {
            MatchCollection matches = Rgx.Matches(e.Line);
            if (matches.Count > 0)
            {
                string zoneName = matches[0].Groups["zoneName"].Value;

                Console.WriteLine(DateTime.Now.ToString() + " - You have entered: " + zoneName + ". Zone Without Flasks: " + ZonesWithoutFlask.ContainsKey(zoneName));
                if (ZonesWithoutFlask.ContainsKey(zoneName) == false && zoneWithoutFlask == true)
                {
                    MouseHook.Start();
                    MouseHook.MouseAction += MouseEvent;
                } else if (ZonesWithoutFlask.ContainsKey(zoneName) == true) {
                    zoneWithoutFlask = true;
                    Stop();
                }
            }
        }

        private void MouseEvent(object sender, MouseClickEvent e)
        {
            Console.WriteLine("Left mouse click!");

            zoneWithoutFlask = false;
            Start();

            MouseHook.MouseAction -= MouseEvent;
        }
    }
}
