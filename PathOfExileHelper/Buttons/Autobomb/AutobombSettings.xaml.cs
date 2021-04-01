using PathOfExileHelper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WindowsInput.Native;

namespace PathOfExileHelper.Buttons.Autobomb
{
    /// <summary>
    /// Interaction logic for AutobombSettings.xaml
    /// </summary>
    public partial class AutobombSettings : Window
    {
        public AutobombSettings(int DefaultInterval, VirtualKeyCode DefaultKey)
        {
            InitializeComponent();

            foreach (int value in Enum.GetValues(typeof(VirtualKeyCode))) {
                Keys.Items.Add(Enum.GetName(typeof(VirtualKeyCode), value));
            }

            Keys.SelectedValue = DefaultKey.ToString();
            Interval.Text = DefaultInterval.ToString();
        }
    }
}
