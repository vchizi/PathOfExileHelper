using PathOfExileHelper.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Immortality
{
    /// <summary>
    /// Interaction logic for FlaskUsageControl.xaml
    /// </summary>
    public partial class FlaskUsageControl : UserControl
    {
        private Window Window;

        public Settings Settings;

        public Anchor AnchorSettings;

        public UseFlask UseFlaskSettings;

        public FlaskUsageControl(Settings settings)
        {
            InitializeComponent();
            Settings = settings;

            foreach (int value in Enum.GetValues(typeof(VirtualKeyCode)))
            {
                Keys.Items.Add(Enum.GetName(typeof(VirtualKeyCode), value));
            }

            ApplySettings(Settings);
        }

        public void ApplySettings(Settings settings)
        {
            if (settings.Anchor != null)
            {
                AnchorXText.Text = settings.Anchor.X.ToString();
                AnchorYText.Text = settings.Anchor.Y.ToString();
                AnchorColorButton.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(PixelColor.GetHtmlColor(settings.Anchor.Pixel)));

                AnchorSettings = (Anchor)settings.Anchor.Clone();
            }
            if (settings.UseFlask != null)
            {
                UseFlaskXText.Text = settings.UseFlask.X.ToString();
                UseFlaskYText.Text = settings.UseFlask.Y.ToString();
                UseFlaskColorButton.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(PixelColor.GetHtmlColor(settings.UseFlask.Pixel)));

                UseFlaskSettings = (UseFlask)settings.UseFlask.Clone();
            }

            ActiveCheckBox.IsChecked = settings.Active;
            TimeoutText.Text = settings.Timeout.ToString();
            Keys.SelectedValue = settings.KeyToPress.ToString();
        }

        private void AnchorColorButton_Click(object sender, RoutedEventArgs e)
        {
            MouseHook.Start();
            MouseHook.MouseAction += AnchorEvent;
        }

        private void UseFlaskColorButton_Click(object sender, RoutedEventArgs e)
        {
            MouseHook.Start();
            MouseHook.MouseAction += UseFlaskEvent;
        }

        private void AnchorEvent(object sender, MouseClickEvent e)
        {
            AnchorXText.Text = e.X.ToString();
            AnchorYText.Text = e.Y.ToString();

            var Pixel = PixelColor.GetColor(e.X, e.Y);
            AnchorSettings = new Anchor()
            {
                X = e.X,
                Y = e.Y,
                Pixel = Pixel
            };

            AnchorColorButton.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(PixelColor.GetHtmlColor(Pixel)));

            MouseHook.MouseAction -= AnchorEvent;
            MouseHook.Stop();
        }

        public void updateAnchor(Anchor settings)
        {
            AnchorSettings = settings;

            AnchorXText.Text = AnchorSettings.X.ToString();
            AnchorYText.Text = AnchorSettings.Y.ToString();
            AnchorColorButton.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(PixelColor.GetHtmlColor(AnchorSettings.Pixel)));
        }

        private void UseFlaskEvent(object sender, MouseClickEvent e)
        {
            UseFlaskXText.Text = e.X.ToString();
            UseFlaskYText.Text = e.Y.ToString();

            var Pixel = PixelColor.GetColor(e.X, e.Y);
            UseFlaskSettings = new UseFlask()
            {
                X = e.X,
                Y = e.Y,
                Pixel = Pixel
            };

            UseFlaskColorButton.Foreground = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(PixelColor.GetHtmlColor(Pixel)));

            MouseHook.MouseAction -= UseFlaskEvent;
            MouseHook.Stop();
        }

        private void Highlight_Click(object sender, RoutedEventArgs e)
        {

            if (Window == null)
            {
                if (AnchorXText.Text == "" || AnchorYText.Text == "" || UseFlaskXText.Text == "" || UseFlaskYText.Text == "")
                {
                    return;
                }

                Window = new Window
                {
                    Background = System.Windows.Media.Brushes.Transparent,
                    WindowStyle = WindowStyle.None,
                    Topmost = true,
                    ResizeMode = ResizeMode.NoResize,
                    AllowsTransparency = true,
                    ShowInTaskbar = false,
                    WindowState = WindowState.Maximized,
                };

                int AnchorX = int.Parse(AnchorXText.Text);
                int AnchorY = int.Parse(AnchorYText.Text);
                System.Windows.Shapes.Rectangle AnchorRectangle = new System.Windows.Shapes.Rectangle
                {
                    Width = 21,
                    Height = 21,
                    Stroke = System.Windows.Media.Brushes.Green,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    StrokeThickness = 3
                };

                int UseFlaskX = int.Parse(UseFlaskXText.Text);
                int UseFlaskY = int.Parse(UseFlaskYText.Text);
                var UseFlaskLine = new Line
                {
                    Stroke = System.Windows.Media.Brushes.Green,
                    X1 = UseFlaskX - 50,
                    X2 = UseFlaskX + 50,
                    Y1 = UseFlaskY + 3,
                    Y2 = UseFlaskY + 3,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    StrokeThickness = 3
                };

                Canvas canvas = new Canvas();

                canvas.Children.Add(AnchorRectangle);
                Canvas.SetTop(AnchorRectangle, AnchorY - 8);
                Canvas.SetLeft(AnchorRectangle, AnchorX - 8);
                canvas.Children.Add(UseFlaskLine);

                Window.Content = canvas;

                Window.Show();
            } else
            {
                Window.Close();
                Window = null;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ((StackPanel)Parent).Children.Remove(this);
        }
    }
}
