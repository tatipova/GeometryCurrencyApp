namespace GeometryCurrencyApp;

public class SettingsHelper
{
    public static IConfigurationRoot InitConfiguration(string filePath)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile(filePath);
        return builder.Build();
    }
}
