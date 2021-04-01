using PathOfExileHelper.Buttons.Autobomb;
using PathOfExileHelper.Buttons.SearchInChat;
using PathOfExileHelper.Buttons.Immortality;
using PathOfExileHelper.Buttons.SyndicateAndHeist;
using PathOfExileHelper.Utils;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using PathOfExileHelper.Services;

namespace PathOfExileHelper
{
    public partial class MainWindow : Window
    {
        private readonly Settings Settings;
        private readonly System.Windows.Forms.NotifyIcon NIcon = new System.Windows.Forms.NotifyIcon();
        private MainSettings MainSettings;

        public MainWindow()
        {
            InitializeComponent();
            TrayIcon();

            NoActiveWindow.SetNoActiveWindow(this);

            Settings = Settings.Load();
            if (Settings.Main == null)
            {
                Hide();
                ShowMainSettings();
            }

            ApplySettings(Settings);

            MessagesWindow MessagesWindow = new MessagesWindow(MessagesWindowSettings.Load(Settings));
            MessagesWindow.Show();

            ButtonsStack.Children.Add(new AutobombButton(Settings).GetButton());
            ButtonsStack.Children.Add(new ImmortalityButton(Settings).GetButton()); 
            ButtonsStack.Children.Add(new SyndicateAndHeistButton().GetButton());
            ButtonsStack.Children.Add(new SearchInChatButton(Settings, MessagesWindow).GetButton());
        }

        private void ShowMainSettings()
        {
            MainSettings = new MainSettings(Settings);
            MainSettings.Closed += delegate (object sender, EventArgs e) { MainSettings = null; };
            MainSettings.ApplyButton.Click += MainSettingsApplyButton_Click;

            MainSettings.Show();
        }

        private void MainSettingsApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainSettings.Main.ClientTxtPath == string.Empty || MainSettings.Main.ClientTxtPath == null)
            {
                MessageBox.Show("Please select path to Client.txt", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Settings.Main = MainSettings.Main;
            Settings.MessagesWindow = MainSettings.MessagesWindowSettings;
            Settings.Save();

            MainSettings.Close();
            Show();
        }

        private void ApplySettings(Settings settings)
        {
            if (settings.Position != null)
            {
                Top = settings.Position.Top;
                Left = settings.Position.Left;
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Settings.Position = new Position(Top, Left);
            Settings.Save();
        }

        private void TrayIcon()
        {
            System.Windows.Forms.ContextMenuStrip cMenu = new System.Windows.Forms.ContextMenuStrip();
            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Resources/icon.ico")).Stream;
            NIcon.Icon = new System.Drawing.Icon(iconStream);
            NIcon.Visible = true;

            var settings = cMenu.Items.Add("Settings");
            settings.Click += CMenu_OpenSettings;

            var exit = cMenu.Items.Add("Exit");
            exit.Click += CMenu_Close;

            NIcon.ContextMenuStrip = cMenu;
        }

        private void CMenu_Close(object sender, EventArgs e)
        {
            NIcon.Dispose();
            Application.Current.Shutdown();
        }

        private void CMenu_OpenSettings(object sender, EventArgs e)
        {
            ShowMainSettings();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
