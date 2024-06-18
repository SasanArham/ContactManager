using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Base
{
    internal static class SQlDbConnectionStringHelper
    {
        /// <summary>
        /// Tries to get main db sql connection string from varous resourses in following order:
        /// <para> Azure key vault </para>
        /// <para> Enviroment variables</para>
        /// <para> App setting </para>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static string GetConnectionString(IConfiguration configuration)
        {
            // ToDo : Can we do this with "Chain of responsiblities" design pattern?
            string connectionString;

            connectionString = GetFromAzureKeyVault(configuration);
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }

            connectionString = GetFromEnviromentVariables();
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }

            connectionString = GetFromAppSettings(configuration);
            if (!string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }
            throw new Exception("Could not find main db connectionstring");
        }

        private static string GetFromEnviromentVariables()
        {
            try
            {
                var ServerName = Environment.GetEnvironmentVariable("DATABASE_SERVER");
                if (string.IsNullOrEmpty(ServerName))
                {
                    return string.Empty;
                }

                var Port = Environment.GetEnvironmentVariable("DATABASE_PORT");

                var DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
                if (string.IsNullOrEmpty(DatabaseName))
                {
                    return string.Empty;
                }

                var Username = Environment.GetEnvironmentVariable("DATABASE_USER");
                if (string.IsNullOrEmpty(Username))
                {
                    return string.Empty;
                }

                var Password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
                if (string.IsNullOrEmpty(Password))
                {
                    return string.Empty;
                }

                string connectionString = $"Server={ServerName},{Port};Database={DatabaseName};User Id={Username};Password={Password};TrustServerCertificate=Yes;MultipleActiveResultSets=true";
                return connectionString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string GetFromAzureKeyVault(IConfiguration configuration)
        {
            try
            {
                var keyVaultUrl = Environment.GetEnvironmentVariable("Key_Vault_Url") ?? configuration["AzureApp:Key_Vault_Url"];
                var client = new SecretClient(new Uri(keyVaultUrl!), new DefaultAzureCredential());
                var connectionstring = client.GetSecret("MainDataBaseConnectionString").Value.Value;
                return connectionstring;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string GetFromAppSettings(IConfiguration configuration)
        {
            try
            {
                var ServerName = configuration["MainDataBase:ServerName"];
                if (string.IsNullOrEmpty(ServerName))
                {
                    return string.Empty;
                }
                var Port = configuration["MainDataBase:Port"];

                var DatabaseName = configuration["MainDataBase:DbName"];
                if (string.IsNullOrEmpty(DatabaseName))
                {
                    return string.Empty;
                }

                var Username = configuration["MainDataBase:Username"];
                if (string.IsNullOrEmpty(Username))
                {
                    return string.Empty;
                }

                var Password = configuration["MainDataBase:Password"];
                if (string.IsNullOrEmpty(Password))
                {
                    return string.Empty;
                }

                string connectionString = $"Server={ServerName},{Port};Database={DatabaseName};User Id={Username};Password={Password};TrustServerCertificate=Yes;MultipleActiveResultSets=true";
                return connectionString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
