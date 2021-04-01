using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathOfExileHelper.Events
{
    public class NewLineEvent : EventArgs
    {
        public String Line { get; set; }

        public NewLineEvent(string line)
        {
            Line = line;
        }
    }
}
