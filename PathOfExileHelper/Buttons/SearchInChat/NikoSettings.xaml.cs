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
    /// Interaction logic for NikoSettings.xaml
    /// </summary>
    public partial class NikoSettings : Window
    {
        public Niko Niko { get; private set; }

        public NikoSettings(Niko niko)
        {
            InitializeComponent();

            Niko = (Niko)niko.Clone();

            DataContext = Niko;
        }
    }
}
