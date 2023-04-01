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
    public class ImmortalityButton : Button
    {
        //private readonly Settings Settings;
        private readonly FlaskUsageSettings FlaskUsageSettings;

        private readonly IFlaskUsageHandler FlaskUsageHandler;

        private ImmortalitySettings ImmortalitySettings;

        public ImmortalityButton(PathOfExileHelper.Settings settings) : base()
        {
            Activated = true;

            FlaskUsageSettings = FlaskUsageSettings.Load(settings);
            if (FlaskUsageSettings.SettingsList.Count == 0)
            {
                SettingsRequired = true;
                Activated = false;
            }

            ButtonControl.ControlButton.Click += ButtonClick;
            ButtonControl.ControlButton.MouseRightButtonUp += RightButtonClick;

            FlaskUsageHandler = new ThreadFlaskUsageHandler(FlaskUsageSettings.SettingsList, settings);

            if (Activated)
            {
                FlaskUsageHandler.Start();
            }
        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (FlaskUsageSettings.SettingsList.Count == 0)
            {
                OpenSettingsWindow();

                return;
            }

            if (Activated)
            {
                FlaskUsageHandler.Start();
            }
            else
            {
                FlaskUsageHandler.Stop();
            }
        }

        public void RightButtonClick(object sender, RoutedEventArgs e) 
        {
            OpenSettingsWindow();
        }

        public void OpenSettingsWindow()
        {
            if (ImmortalitySettings == null)
            {
                ImmortalitySettings = new ImmortalitySettings(FlaskUsageSettings);
                ImmortalitySettings.Closed += SettingWindowClosed;

                ImmortalitySettings.Show();
            } 
        }

        protected override FontAwesomeIcon Icon()
        {
            return FontAwesomeIcon.Flask;
        }

        private void SettingWindowClosed(object sender, EventArgs e)
        {
            if (FlaskUsageSettings.SettingsList.Count > 0)
            {
                SettingsRequired = false;

                if (Activated)
                {
                    FlaskUsageHandler.Stop();
                    FlaskUsageHandler.Start();
                }
            }
            ImmortalitySettings = null;
        }
    }
}
