using SolanaMAUI_sample.State;

namespace SolanaMAUI_sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            if(Current != null)
                Current.UserAppTheme = AppTheme.Dark;

            RuntimeState.CurrentTokenAddress = "EMPTY";
            RuntimeState.RPC_Provider = "RPC_PROVIDER_HERE";
            RuntimeState.TRADER_KEY = "TRADER_KEY_HERE";
        }
    }

}
