using PathOfExileHelper.Events;
using PathOfExileHelper.Services;
using PathOfExileHelper.UserControls;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PathOfExileHelper.Buttons.SearchInChat.EventHandlers
{
    public class SearchMessage
    {
        private static string Pattern = @"\]\s[#&]?(<.*\>\s)?(?<username>\w*):\s(?<message>.*)$";
        private Regex Rgx = new Regex(Pattern);
        private readonly MessagesWindow MessagesWindow;
        private readonly MessagesWindowSettings Settings;

        public SearchParameters SearchParameters { get; }

        Dictionary<string, string> Messages = new Dictionary<string, string>();

        public SearchMessage(MessagesWindowSettings settings, MessagesWindow messagesWindow, ClientTxtReader clientTxtReader, SearchParameters searchParameters)
        {
            clientTxtReader.NewLineAdded += HandleNewLine;
            Settings = settings;
            MessagesWindow = messagesWindow;
            SearchParameters = searchParameters;
        }

        public void HandleNewLine(object sender, NewLineEvent e)
        {
            MatchCollection matches = Rgx.Matches(e.Line);
            if (matches.Count > 0)
            {
                string message = matches[0].Groups["message"].Value;
                string username = matches[0].Groups["username"].Value;

                if (MessageFound(message.ToUpper(), e))
                {
                    try
                    {
                        Messages.Add(message.ToUpper(), username);
                    }
                    catch (ArgumentException)
                    {
                        return;
                    }

                    MessageControl messageControl = new MessageControl(Settings, new NewMessage() { Username = username, Message = message });
                    MessagesWindow.MessagesPanel.Children.Add(messageControl);
                }
            }
        }
        private bool MessageFound(string message, NewLineEvent e)
        {
            string[] orText = SearchParameters.OrText.ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] andOrText = SearchParameters.AndOrText.ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] notText = SearchParameters.NotText.ToUpper().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (orText.Length > 0 && !orFound(message, orText))
            {
                return false;
            }

            if (andOrText.Length > 0 && !andOrFound(message, andOrText))
            {
                return false;
            }

            if (notText.Length > 0 && notFound(message, notText))
            {
                return false;
            }

            return true;
        }

        private bool orFound(string message, string[] or)
        {
            bool found = false;

            foreach (string element in or)
            {
                if (message.Contains(element.Trim()))
                {
                    found = true;
                }
            }

            return found;
        }

        private bool andOrFound(string message, string[] andOr)
        {
            bool found = false;
            foreach (string element in andOr)
            {
                if (message.Contains(element.Trim()))
                {
                    found = true;
                }
            }

            return found;
        }

        private bool notFound(string message, string[] not)
        {
            bool found = false;
            foreach (string element in not)
            {
                Regex regex = new Regex(@"\b" + element.Trim() + @"\b");
                Match match = regex.Match(message);
                if (match.Success)
                {
                    found = true;
                }
            }

            return found;
        }

        public void Clear()
        {
            Messages.Clear();
        }
    }
}
