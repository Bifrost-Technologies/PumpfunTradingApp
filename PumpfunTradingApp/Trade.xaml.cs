using SolanaMAUI_sample.State;
using Solnet.Programs;
using Solnet.Pumpfun;
using Solnet.Rpc;
using Solnet.Wallet;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace SolanaMAUI_sample;

public partial class Trade : ContentPage
{
    public TradeViewModel ViewModel { get; set; }
    public Trade()
    {
        InitializeComponent();
        ViewModel = new TradeViewModel()
        {
            CurrentTokenAddress = RuntimeState.CurrentTokenAddress,
        };
        BindingContext = ViewModel;
    }
    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string tokenAddress = TokenAddressEntry.Text;
        string amount = AmountEntry.Text;
        string? transactionType = TransactionTypePicker.SelectedItem.ToString();

        Account _trader = Account.FromSecretKey(RuntimeState.TRADER_KEY);
        IRpcClient connection = ClientFactory.GetClient(RuntimeState.RPC_Provider);
        PumpfunClient pumpFun = new PumpfunClient(connection, _trader);
        string response = string.Empty;
        try
        {
            if (transactionType == "Buy")
            {
                response = await pumpFun.Buy(tokenAddress, Convert.ToDecimal(amount), Convert.ToInt32(15), Convert.ToUInt64(100000), Convert.ToUInt64(1500000));
                await DisplayAlert("Response:", response, "OK");
            }
            if (transactionType == "Sell")
            {
                PublicKey associatedUser = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(_trader, new PublicKey(RuntimeState.CurrentTokenAddress));
                var tokenbalance = await connection.GetTokenAccountBalanceAsync(associatedUser);

                if (tokenbalance.Result != null && tokenbalance.WasSuccessful == true && tokenbalance.Result.Value != null)
                {
                    decimal _amount = tokenbalance.Result.Value.AmountDecimal;
                    response = await pumpFun.Sell(tokenAddress, Convert.ToDecimal(_amount), Convert.ToDecimal(0.001), Convert.ToUInt64(100000), Convert.ToUInt64(2500000));
                    await DisplayAlert("Response:", response, "OK");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await DisplayAlert("Error:", ex.Message, "ERROR");
        }
    }
}
public class TradeViewModel : INotifyPropertyChanged
{
    private string? _currentTokenAddress = string.Empty;
    public string? CurrentTokenAddress
    {
        get => _currentTokenAddress; set
        {
            if (_currentTokenAddress != value)
            { _currentTokenAddress = value; OnPropertyChanged(); }
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}