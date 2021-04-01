using FontAwesome.WPF;
using PathOfExileHelper.UserControls;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace PathOfExileHelper.Buttons
{
    public abstract class Button : IButton
    {
        protected readonly MainControlButton ButtonControl;
        private bool _activated = false;
        public bool Activated {
            get
            {
                return _activated;
            }
            protected set
            {
                if (value != _activated)
                {
                    _activated = value;
                    ActivatedChanged();
                }
            }
        }

        public bool SettingsRequired { get; protected set; } = false;
            
        public Button()
        {
            ButtonControl = new MainControlButton();
            ButtonControl.ControlButton.Focusable = false;
            Awesome.SetContent(ButtonControl.ControlButton, Icon());
            ButtonControl.ControlButton.Click += Activate;
        }

        private void ActivatedChanged()
        {
            if (Activated)
            {
                ButtonControl.ControlButton.Foreground = Brushes.Green;
            }
            else
            {
                ButtonControl.ControlButton.Foreground = Brushes.White;
            }
        }

        protected void Activate(object sender, RoutedEventArgs e)
        {
            if (SettingsRequired)
            {
                return;
            }

            if (Activated)
            {
                Activated = false;
            }
            else
            {
                Activated = true;
            }
        }

        public MainControlButton GetButton()
        {
            return ButtonControl;
        }

        protected abstract FontAwesomeIcon Icon();
    }
}
