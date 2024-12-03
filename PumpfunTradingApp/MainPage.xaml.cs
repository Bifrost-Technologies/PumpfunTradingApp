using SolanaMAUI_sample.State;

namespace SolanaMAUI_sample
{
    public partial class MainPage : ContentPage
    {
        string baseurl = "https://ape.pro/solana/";
        string parameters = "?embed=1&tabs=0&info=0&chartLeftToolbar=0&chartTheme=dark&theme=dark&chartStyle=0&chartType=usd&interval=15";
        public MainPage()
        {
            InitializeComponent();
            dex.Source = baseurl + RuntimeState.CurrentTokenAddress + parameters;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            string tokenAddress = TokenAddressEntry.Text;
            RuntimeState.CurrentTokenAddress = tokenAddress;
            dex.Source = baseurl + tokenAddress + parameters;
            DisplayAlert("Submission", tokenAddress, "OK");

        }
    }

}
