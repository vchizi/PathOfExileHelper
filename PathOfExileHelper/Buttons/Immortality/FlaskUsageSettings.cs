using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathOfExileHelper.Buttons.Immortality
{
    public class FlaskUsageSettings
    {
        [JsonIgnore]
        public PathOfExileHelper.Settings settings { private get; set; }

        public List<Settings> SettingsList = new List<Settings>();

        public static FlaskUsageSettings Load(PathOfExileHelper.Settings settings)
        {
            if (settings.FlaskUsageSettings != null)
            {
                return settings.FlaskUsageSettings;
            }

            return new FlaskUsageSettings()
            {
                settings = settings
            };
        }

        public void Save()
        {
            settings.FlaskUsageSettings = this;
            settings.Save();
        }
    }
}
