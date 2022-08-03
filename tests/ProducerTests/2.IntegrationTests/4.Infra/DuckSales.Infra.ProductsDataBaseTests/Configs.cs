using Microsoft.Extensions.Configuration;

namespace DuckSales.Infra.ProductsDataBaseTests;

public static class Configs
{
    private static IConfiguration _config;

    public static IConfiguration Configuration
    {
        get
        {
            if (_config is null)
                SetConfig();

            return _config;
        }
    }

    private static void SetConfig()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddUserSecrets<ProductDbTestFixtures>()
            .AddEnvironmentVariables()
            .Build();
    }

    public static T GetConfig<T>(string sectionName)
    {
        return Configuration.GetSection(sectionName).Get<T>();
    }
}
