using PathOfExileHelper.Utils;
using System;
using System.Windows;
using System.Windows.Input;

namespace PathOfExileHelper
{
    /// <summary>
    /// Interaction logic for MessagesWindow.xaml
    /// </summary>
    public partial class MessagesWindow : Window
    {
        MessagesWindowSettings Settings;

        public MessagesWindow(MessagesWindowSettings settings)
        {
            InitializeComponent();

            NoActiveWindow.SetNoActiveWindow(this);

            Settings = settings;

            if (Settings.Position != null)
            {
                Top = Settings.Position.Top;
                Left = Settings.Position.Left;
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Settings.Position = new Position(this.Top, this.Left);
            Settings.Save();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
