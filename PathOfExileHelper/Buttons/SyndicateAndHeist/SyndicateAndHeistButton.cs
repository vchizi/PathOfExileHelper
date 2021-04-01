using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PathOfExileHelper.Buttons.SyndicateAndHeist
{
    class SyndicateAndHeistButton : Button
    {
        private Window Window;

        public SyndicateAndHeistButton()
        {
            ButtonControl.ControlButton.Click += ShowSyndicateImage;
            ButtonControl.ControlButton.MouseRightButtonUp += ShowHeisImage;
            ButtonControl.ControlButton.MouseRightButtonUp += Activate;
        }

        public void ShowSyndicateImage(object sender, RoutedEventArgs e)
        {
            DisplayImage("pack://application:,,,/Resources/Images/syndicate.png");
        }

        public void ShowHeisImage(object sender, RoutedEventArgs e)
        {
            DisplayImage("pack://application:,,,/Resources/Images/heist.png");
        }

        private void DisplayImage(string ImagePath)
        {
            if (Window == null)
            {
                Window parentWindow = Window.GetWindow(ButtonControl);

                var BitmapImage = new BitmapImage(new Uri(ImagePath));
                Image Image = new Image()
                {
                    Source = BitmapImage,
                    Width = BitmapImage.Width * 1.2,
                    Height = BitmapImage.Height * 1.2,
                };

                double WindowLeft = 0;
                if (0 < (parentWindow.Left + parentWindow.Width) - Image.Width)
                {
                    WindowLeft = (parentWindow.Left + parentWindow.Width) - Image.Width;
                }

                Window = new Window
                {
                    WindowStyle = WindowStyle.None,
                    Topmost = true,
                    ResizeMode = ResizeMode.NoResize,
                    ShowInTaskbar = false,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    Top = parentWindow.Top + parentWindow.Height,
                    Left = WindowLeft,
                    Content = Image,
                };

                Window.Show();
            }
            else
            {
                Window.Close();
                Window = null;
            }
        }

        protected override FontAwesomeIcon Icon()
        {
            return FontAwesomeIcon.Image;
        }
    }
}
