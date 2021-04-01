using PathOfExileHelper.Events;
using System;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows;

namespace PathOfExileHelper.Services
{
    public class ClientTxtReader
    {
        private FileStream FileStream;
        private StreamReader StreamReader;
        private Timer Timer;

        public event EventHandler<NewLineEvent> NewLineAdded;

        public ClientTxtReader(string ClientTxtPath)
        {
            if (FileStream == null)
            {
                FileStream = new FileStream(ClientTxtPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }

            if (StreamReader == null)
            {
                StreamReader = new StreamReader(FileStream, Encoding.UTF8);
            }

            while (StreamReader.ReadLine() != null) { }
        }

        public void StartReading()
        {
            Timer = new Timer(500);

            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (FileStream.Position < FileStream.Length)
            {
                string line;
                while ((line = StreamReader.ReadLine()) != null)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        NewLineAdded?.Invoke(this, new NewLineEvent(line));
                    });
                }
            }
        }

        public void StopReading()
        {
            if (Timer != null)
            {
                Timer.Stop();
            }
        }
    }
}
