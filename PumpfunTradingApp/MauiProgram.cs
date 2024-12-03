using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;

namespace SolanaMAUI_sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}