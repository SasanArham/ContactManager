namespace Infrastructure.Common.FileManagement
{
    internal static class AzureStorageHelper
    {
        public static string StoragePath(string accountName) => $"https://{accountName}.blob.core.windows.net";
    }
}
