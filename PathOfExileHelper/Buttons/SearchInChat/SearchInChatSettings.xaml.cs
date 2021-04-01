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
    /// Interaction logic for SearchInChatSettings.xaml
    /// </summary>
    public partial class SearchInChatSettings : Window
    {
        private TrialSettings TrialSettings;
        private NikoSettings NikoSettings;
        public Settings Settings { get; set; }
        private SearchParameters SearchParameters;

        public SearchInChatSettings(Settings settings, SearchParameters searchParameters, GlobalChatParameters globalChatParameters)
        {
            InitializeComponent();
            Settings = settings;

            SearchParameters = searchParameters;

            GlobalChannelInput.DataContext = globalChatParameters;
            AutoJoingGlobal.DataContext = globalChatParameters;

            OrInput.DataContext = searchParameters;
            AndOrInput.DataContext = searchParameters;
            NotInput.DataContext = searchParameters;
        }

        private void TrialButton_Click(object sender, RoutedEventArgs e)
        {
            if (TrialSettings == null)
            {
                TrialSettings = new TrialSettings(Settings.Trial);
                TrialSettings.Closed += TrialSettings_Closed;
                TrialSettings.ApplyButton.Click += TrialSettingsApply_Clicked;
                TrialSettings.Show();
                TrialSettings.Activate();
            }
        }

        private void TrialSettings_Closed(object sender, EventArgs e)
        {
            TrialSettings = null;
        }

        private void TrialSettingsApply_Clicked(object sender, EventArgs e)
        {
            Settings.Trial = TrialSettings.Trial;
            Settings.Save();

            PopulateTrialSearchParameters(TrialSettings.Trial);

            TrialSettings.Close();
        }

        private void PopulateTrialSearchParameters(Trial trial)
        {
            List<string> list = new List<string>();
            if (trial.PiercingTruth)
                list.Add("piercing");
           
            if (trial.SwirlingFear)
                list.Add("swirling");
            
            if (trial.CripplingGrief)
                list.Add("crippling");
            
            if (trial.BurningRage)
                list.Add("burning");
            
            if (trial.LingeringPain)
                list.Add("lingering");
            
            if (trial.StingingDoubt)
                list.Add("stinging");

            SearchParameters.OrText = String.Join(", ", list.ToArray());
            SearchParameters.AndOrText = "";
            SearchParameters.NotText = "lf";
        }

        private void NikoButton_Click(object sender, RoutedEventArgs e)
        {
            if (NikoSettings == null)
            {
                NikoSettings = new NikoSettings(Settings.Niko);
                NikoSettings.Closed += NikoSettings_Closed;
                NikoSettings.ApplyButton.Click += NikoSettingsApply_Clicked;

                NikoSettings.Show();
                NikoSettings.Activate();
            }
        }

        private void NikoSettings_Closed(object sender, EventArgs e)
        {
            NikoSettings = null;
        }

        private void NikoSettingsApply_Clicked(object sender, EventArgs e)
        {
            Settings.Niko = NikoSettings.Niko;
            Settings.Save();

            PopulateNikoSearchParameters(NikoSettings.Niko);

            NikoSettings.Close();
            NikoSettings = null;
        }

        private void PopulateNikoSearchParameters(Niko niko)
        {
            List<string> list = new List<string>();
            if (niko.Single)
                list.Add("single");
            if (niko.Double)
                list.Add("double");
            if (niko.Triple)
                list.Add("triple");

            SearchParameters.OrText = "sulph, niko";
            SearchParameters.AndOrText = String.Join(", ", list.ToArray());
            SearchParameters.NotText = "scarab, lf";
        }
    }
}
