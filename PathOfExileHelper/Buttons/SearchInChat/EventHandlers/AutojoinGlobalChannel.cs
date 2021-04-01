using PathOfExileHelper.Events;
using PathOfExileHelper.Services;
using System;
using System.Text.RegularExpressions;

namespace PathOfExileHelper.Buttons.SearchInChat.EventHandlers
{
    class AutojoinGlobalChannel
    {
        private static readonly string PatternZoneEntered = @"\]\s:\s(You have entered)\s";
        private static readonly string PatternFailedJoinChannel = @"\]\s:\s(Failed to join requested chat channel because it is full.)$";

        private readonly Regex RgxZoneEntered = new Regex(PatternZoneEntered);
        private readonly Regex RgxFailedJoinChannel = new Regex(PatternFailedJoinChannel);

        public GlobalChatParameters GlobalChatParameters { get; }

        public AutojoinGlobalChannel(ClientTxtReader clientTxtReader, GlobalChatParameters globalChatParameters)
        {
            clientTxtReader.NewLineAdded += HandleNewLine;

            GlobalChatParameters = globalChatParameters;
        }

        public void HandleNewLine(object sender, NewLineEvent e)
        {
            if (GlobalChatParameters.AutoJoin)
            {
                Match ZoneEnteredMatch = RgxZoneEntered.Match(e.Line);
                Match FailedJoinChannelMatch = RgxFailedJoinChannel.Match(e.Line);

                if (ZoneEnteredMatch.Success || FailedJoinChannelMatch.Success)
                {
                    System.Timers.Timer timer = new System.Timers.Timer(ZoneEnteredMatch.Success == true ? 2000 : 5000)
                    {
                        AutoReset = false
                    };

                    timer.Elapsed += delegate (Object o, System.Timers.ElapsedEventArgs es) { 
                        POEWindow.SendInputToPoe("/global " + GlobalChatParameters.GlobalChannel); 
                    };
                    timer.Start();
                }
            }
        }

        public void Join()
        {
            Console.WriteLine(GlobalChatParameters.AutoJoin);
            if (GlobalChatParameters.AutoJoin)
            {
                POEWindow.SendInputToPoe("/global " + GlobalChatParameters.GlobalChannel);
            }
        }
    }
}
