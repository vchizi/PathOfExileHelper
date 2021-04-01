using Newtonsoft.Json;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Autobomb
{
    public class Settings
    {
        [JsonIgnore]
        public PathOfExileHelper.Settings settings { private get;  set; }

        public int Interval { get; set; } = 100;

        public VirtualKeyCode KeyToPress { get; set; } = VirtualKeyCode.VK_D;

        public static Settings Load(PathOfExileHelper.Settings settings)
        {
            if (settings.AutobombSettings != null)
            {
                return settings.AutobombSettings;
            }

            return new Settings() { 
                settings = settings
            };
        }


        public void Save()
        {
            settings.AutobombSettings = this;
            settings.Save();
        }
    }
}
