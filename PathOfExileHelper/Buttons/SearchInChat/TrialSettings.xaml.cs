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

namespace PathOfExileHelper.Buttons.SearchInChat
{
    /// <summary>
    /// Interaction logic for TrialSettings.xaml
    /// </summary>
    public partial class TrialSettings : Window
    {
        public Trial Trial { get; private set; }

        public TrialSettings(Trial trial)
        {
            InitializeComponent();
            NoActiveWindow.SetNoActiveWindow(this);
            Trial = (Trial)trial.Clone();

            DataContext = Trial;
        }
    }
}
