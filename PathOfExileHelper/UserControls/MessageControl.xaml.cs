using PathOfExileHelper.Services;
using System;
using System.Diagnostics;
using System.Media;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace PathOfExileHelper.UserControls
{
    public partial class MessageControl : UserControl
    {
        private readonly MessagesWindowSettings Settings;
        private readonly NewMessage Message;
        Stopwatch stopwatch = new Stopwatch();
        readonly Timer timer = new Timer(500);

        public MessageControl(MessagesWindowSettings settings, NewMessage message)
        {
            InitializeComponent();

            Settings = settings;
            Message = message;
            DataContext = Message;

            ApplySettings(settings);

            SoundPlayer player = new SoundPlayer(Properties.Resources.notification);
            player.Play();

            StartTime();
        }

        private void ApplySettings(MessagesWindowSettings settings)
        {
            btnCustom1.ToolTip = (new ToolTip().Content = settings.CustomWhisper1);
            btnCustom2.ToolTip = (new ToolTip().Content = settings.CustomWhisper2);
            btnCustom3.ToolTip = (new ToolTip().Content = settings.CustomWhisper3);

            if (settings.CustomWhisper1 == string.Empty)
            {
                btnCustom1.Visibility = Visibility.Hidden;
                btnCustom1.Visibility = Visibility.Collapsed;
            }

            if (settings.CustomWhisper2 == string.Empty)
            {
                btnCustom2.Visibility = Visibility.Hidden;
                btnCustom2.Visibility = Visibility.Collapsed;
            }

            if (settings.CustomWhisper3 == string.Empty)
            {
                btnCustom3.Visibility = Visibility.Hidden;
                btnCustom3.Visibility = Visibility.Collapsed;
            }
        }

        private void StartTime()
        {
            timer.Elapsed += Timer_Tick;
            timer.AutoReset = true;
            timer.Enabled = true;

            stopwatch.Start();

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string currentTime = String.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, ts.Seconds);

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                txtTime.Text = currentTime;
            });
        }

        private void ClickRemoveItem(object sender, RoutedEventArgs e)
        {
            timer.Stop();

            ((StackPanel)Parent).Children.Remove(this);
        }

        private void ClickAskForParty(object sender, RoutedEventArgs e)
        {
            POEWindow.SendInputToPoe("@" + Message.Username + " Party please!");
        }

        private void ClickWhisper(object sender, RoutedEventArgs e)
        {
            POEWindow.SendInputToPoeNoSubmit("@" + Message.Username + " ");
        }

        private void ClickCustomWhisper1(object sender, RoutedEventArgs e)
        {
            POEWindow.SendInputToPoe("@" + Message.Username + " " + Settings.CustomWhisper1);
        }

        private void ClickCustomWhisper2(object sender, RoutedEventArgs e)
        {
            POEWindow.SendInputToPoe("@" + Message.Username + " " + Settings.CustomWhisper2);
        }

        private void ClickCustomWhisper3(object sender, RoutedEventArgs e)
        {
            POEWindow.SendInputToPoe("@" + Message.Username + " " + Settings.CustomWhisper3);
        }
    }

    public class NewMessage
    {
        public string Username { get; set; }

        public string Message { get; set; }
    }
}
