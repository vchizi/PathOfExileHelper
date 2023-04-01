using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PathOfExileHelper
{
    public class Settings
    {
        [JsonIgnore]
        private static string SettingsFile = @".\settings.json";

        public Main Main;

        public Position Position;

        public MessagesWindowSettings MessagesWindow;

        public Buttons.Autobomb.Settings AutobombSettings { get; set; }
        public Buttons.Immortality.Settings ImmortalitySettings { get; set; }
        public Buttons.Immortality.FlaskUsageSettings FlaskUsageSettings { get; set; }
        public Buttons.SearchInChat.Settings SearchInChatSettings { get; set; }

        public static Settings Load()
        {
            if (File.Exists(SettingsFile))
            {
                string settingsJson = File.ReadAllText(SettingsFile);

                Settings settings = JsonConvert.DeserializeObject<Settings>(settingsJson);

                return settings;
            }

            return new Settings();
        }

        public void Save()
        {
            File.WriteAllText(SettingsFile, JsonConvert.SerializeObject(this));
        }


        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (AutobombSettings != null) 
                AutobombSettings.settings = this;

            if (ImmortalitySettings != null)
                ImmortalitySettings.settings = this;

            if (FlaskUsageSettings != null)
                FlaskUsageSettings.settings = this;

            if (SearchInChatSettings != null)
                SearchInChatSettings.settings = this;

            if (MessagesWindow != null)
                MessagesWindow.Settings = this;
        }
    }

    public class MessagesWindowSettings : ICloneable, INotifyPropertyChanged
    {
        [JsonIgnore]
        public Settings Settings;
        public Position Position { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _customWhisper1 = string.Empty;
        public string CustomWhisper1
        {
            get
            {
                return _customWhisper1;
            }
            set
            {
                _customWhisper1 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomWhisper1"));
                }
            }

        }
        private string _customWhisper2 = string.Empty;
        public string CustomWhisper2
        {
            get
            {
                return _customWhisper2;
            }
            set
            {
                _customWhisper2 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomWhisper2"));
                }
            }

        }
        private string _customWhisper3 = string.Empty;
        public string CustomWhisper3
        {
            get
            {
                return _customWhisper3;
            }
            set
            {
                _customWhisper3 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomWhisper3"));
                }
            }

        }

        public static MessagesWindowSettings Load(Settings settings)
        {
            if (settings.MessagesWindow != null)
            {
                return settings.MessagesWindow;
            }

            return new MessagesWindowSettings()
            {
                Settings = settings,
            };
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void Save()
        {
            Settings.MessagesWindow = this;
            Settings.Save();
        }
    }

    public class Position
    {
        public double Top { get; }
        public double Left { get; }

        public Position(double Top, double Left)
        {
            this.Top = Top;
            this.Left = Left;
        }
    }

    public class Main : ICloneable, INotifyPropertyChanged
    {
        private string _clientTxtPath = string.Empty;
        public string ClientTxtPath {
            get
            {
                return _clientTxtPath;
            }
            set
            {
                _clientTxtPath = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ClientTxtPath"));
                }
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
