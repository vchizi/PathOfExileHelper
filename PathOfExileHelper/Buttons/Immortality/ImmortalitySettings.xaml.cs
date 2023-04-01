using PathOfExileHelper.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Immortality
{
    public partial class ImmortalitySettings : Window
    {
        private Window Window;

        readonly FlaskUsageSettings FlaskUsageSettings;

        public ImmortalitySettings(FlaskUsageSettings settings)
        {
            InitializeComponent();

            FlaskUsageSettings = settings;

            if (settings.SettingsList.Count > 0)
            {
                foreach (var item in settings.SettingsList)
                {
                    FlaskUsageControl flaskUsageControl = new FlaskUsageControl(item);

                    flaskUsageControl.CopyAnchor.Click += CopyAnchor_Click;

                    FlaskUsagePanel.Children.Add(flaskUsageControl);
                }
            } else
            {
                FlaskUsagePanel.Children.Add(CreateFlaskUsageControl());
            }

            Closed += SettingWindowClosed;
        }

        private void CopyAnchor_Click(object sender, RoutedEventArgs e)
        {
            FlaskUsageControl control = (FlaskUsageControl)((Grid)((Control)sender).Parent).Parent;

            foreach (FlaskUsageControl item in FlaskUsagePanel.Children)
            {
                item.updateAnchor((Anchor)control.AnchorSettings.Clone());
            }
        }

        private void SettingWindowClosed(object sender, EventArgs e)
        {
            if (Window != null)
            {
                Window.Close();
                Window = null;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FlaskUsageSettings.SettingsList.Clear();
            foreach (FlaskUsageControl control in FlaskUsagePanel.Children)
            {
                control.Settings.Timeout = int.Parse(control.TimeoutText.Text);
                control.Settings.Active = control.ActiveCheckBox.IsChecked == true;
                control.Settings.Anchor = control.AnchorSettings;
                control.Settings.UseFlask = control.UseFlaskSettings;
                control.Settings.KeyToPress = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), control.Keys.SelectedValue.ToString());
                FlaskUsageSettings.SettingsList.Add(control.Settings);
            }

            FlaskUsageSettings.Save();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            FlaskUsagePanel.Children.Add(CreateFlaskUsageControl());
        }

        private FlaskUsageControl CreateFlaskUsageControl()
        {
            var setting = new Settings();
            FlaskUsageControl FlaskUsageControl = new FlaskUsageControl(setting);

            FlaskUsageSettings.SettingsList.Add(setting);

            return FlaskUsageControl;
        }
    }

}
