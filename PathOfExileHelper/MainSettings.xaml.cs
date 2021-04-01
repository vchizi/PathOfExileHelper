using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
namespace PathOfExileHelper
{
    /// <summary>
    /// Interaction logic for MainSettings.xaml
    /// </summary>
    public partial class MainSettings : Window
    {
        private string LogFileDirectory;

        public Main Main { get; private set; }

        public MessagesWindowSettings MessagesWindowSettings { get; private set; }

        public MainSettings(Settings settings)
        {
            InitializeComponent();

            Main = settings.Main != null ? (Main)settings.Main.Clone() : new Main();
            MessagesWindowSettings = settings.MessagesWindow != null ? (MessagesWindowSettings)settings.MessagesWindow.Clone() : new MessagesWindowSettings();

            CustomWhisper1Text.DataContext = MessagesWindowSettings;
            CustomWhisper2Text.DataContext = MessagesWindowSettings;
            CustomWhisper3Text.DataContext = MessagesWindowSettings;
            ClientTxtPathText.DataContext = Main;
        }

        private void OpenPathToClient_Click(object sender, RoutedEventArgs e)
        {
            if (Main.ClientTxtPath == null)
            {
                Process[] processes = Process.GetProcessesByName("PathOfExile_x64Steam");
                if (processes.Length > 0)
                {
                    LogFileDirectory = processes[0].Modules[0].FileName.Replace("PathOfExile_x64Steam.exe", @"logs\");
                }
            }
            else
            {
                LogFileDirectory = Main.ClientTxtPath.Replace(@"logs\Client.txt", "");
            }

            OpenFileDialog ofd = new OpenFileDialog();
            if (Directory.Exists(LogFileDirectory))
            {
                ofd.InitialDirectory = LogFileDirectory;
            }
            ofd.Filter = "Text files (*.txt)|*.txt";

            if (ofd.ShowDialog() == true)
            {
                Main.ClientTxtPath = ofd.FileName;
            }
        }
    }
}
