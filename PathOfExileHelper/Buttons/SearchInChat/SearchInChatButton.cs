using FontAwesome.WPF;
using PathOfExileHelper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using PathOfExileHelper.Buttons.SearchInChat.EventHandlers;

namespace PathOfExileHelper.Buttons.SearchInChat
{
    public class SearchInChatButton : Button
    {
        public SearchParameters SearchParameters { get; set; } = new SearchParameters();
        public GlobalChatParameters GlobalChatParameters { get; set; } = new GlobalChatParameters();

        private readonly Settings Settings;
        private readonly PathOfExileHelper.Settings GlobalSettings;
        private SearchInChatSettings SearchSettings;
        private ClientTxtReader ClientTxtReader;
        private AutojoinGlobalChannel AutojoinGlobalChannel;
        private SearchMessage SearchMessage;
        private readonly MessagesWindow MessagesWindow;

        public SearchInChatButton(PathOfExileHelper.Settings settings, MessagesWindow messagesWindow) : base()
        {
            SettingsRequired = true;
            ButtonControl.ControlButton.Click += ButtonClick;

            Settings = Settings.Load(settings);
            GlobalSettings = settings;

            MessagesWindow = messagesWindow;
        }

        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (SearchSettings == null)
            {
                SearchSettings = new SearchInChatSettings(Settings, SearchParameters, GlobalChatParameters)
                {
                    Topmost = true,
                };

                SearchSettings.StartButton.IsEnabled = !Activated;
                SearchSettings.ClearButton.IsEnabled = !Activated;
                SearchSettings.StopButton.IsEnabled = Activated;

                SearchSettings.ClearButton.Click += ClearSearch;
                SearchSettings.StartButton.Click += StartSearch;
                SearchSettings.StopButton.Click += StopSearch;
                SearchSettings.Closed += SettingsClosed;

                SearchSettings.Show();
            }
            else
            {
                SearchSettings.Close();
            }
        }

        public void StartSearch(object sender, RoutedEventArgs e)
        {
            Activated = true;
            SearchSettings.StopButton.IsEnabled = true;
            SearchSettings.StartButton.IsEnabled = false;
            SearchSettings.ClearButton.IsEnabled = false;
            SearchSettings.Close();
            SearchSettings = null;

            ClientTxtReader = new ClientTxtReader(GlobalSettings.Main.ClientTxtPath);

            SearchMessage = new SearchMessage(GlobalSettings.MessagesWindow, MessagesWindow, ClientTxtReader, SearchParameters);
            AutojoinGlobalChannel = new AutojoinGlobalChannel(ClientTxtReader, GlobalChatParameters);

            AutojoinGlobalChannel.Join();
            ClientTxtReader.StartReading();
        }

        public void StopSearch(object sender, RoutedEventArgs e)
        {
            Activated = false;
            SearchSettings.StopButton.IsEnabled = false;
            SearchSettings.StartButton.IsEnabled = true;
            SearchSettings.ClearButton.IsEnabled = true;

            ClientTxtReader.StopReading();
            MessagesWindow.MessagesPanel.Children.Clear();
            SearchMessage.Clear();
        }

        public void ClearSearch(object sender, RoutedEventArgs e)
        {
            SearchParameters.Clear();
        }

        public void SettingsClosed(object sender, EventArgs e)
        {
            SearchSettings = null;
        }

        protected override FontAwesomeIcon Icon()
        {
            return FontAwesomeIcon.Search;
        }
    }
}
