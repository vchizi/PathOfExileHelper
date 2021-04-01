using Newtonsoft.Json;
using System;

namespace PathOfExileHelper.Buttons.SearchInChat
{
    public class Settings
    {
        [JsonIgnore]
        public PathOfExileHelper.Settings settings { private get; set; }

        public Trial Trial { get; set; }

        public Niko Niko { get; set; }

        public static Settings Load(PathOfExileHelper.Settings settings)
        {
            if (settings.SearchInChatSettings != null)
            {
                if (settings.SearchInChatSettings.Trial == null)
                    settings.SearchInChatSettings.Trial = new Trial();

                if (settings.SearchInChatSettings.Niko == null)
                    settings.SearchInChatSettings.Niko = new Niko();

                return settings.SearchInChatSettings;
            }

            return new Settings()
            {
                settings = settings,
                Trial = new Trial(),
                Niko = new Niko(),
            };
        }

        public void Save()
        {
            settings.SearchInChatSettings = this;
            settings.Save();
        }
    }

    public class Trial : ICloneable
    {
        public bool PiercingTruth { get; set; } = true;
        public bool SwirlingFear { get; set; } = true;
        public bool CripplingGrief { get; set; } = true;
        public bool BurningRage { get; set; } = true;
        public bool LingeringPain { get; set; } = true;
        public bool StingingDoubt { get; set; } = true;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class Niko : ICloneable
    {
        public bool Single { get; set; } = false;
        public bool Double { get; set; } = false;
        public bool Triple { get; set; } = false;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
