﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Immortality
{
    public class Settings
    {
        [JsonIgnore]
        public PathOfExileHelper.Settings settings { private get; set; }
        public Anchor Anchor { get; set; }
        public UseFlask UseFlask { get; set; }
        public int Timeout { get; set; } = 200;
        public bool Active { get; set; }
        public VirtualKeyCode KeyToPress { get; set; } = VirtualKeyCode.VK_1;

        public static Settings Load(PathOfExileHelper.Settings settings)
        {
            if (settings.ImmortalitySettings != null)
            {
                return settings.ImmortalitySettings;
            }

            return new Settings()
            {
                settings = settings
            };
        }

        public void Save()
        {
            settings.ImmortalitySettings = this;
            settings.Save();
        }
    }

    public class Anchor : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public uint Pixel { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class UseFlask : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public uint Pixel { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
