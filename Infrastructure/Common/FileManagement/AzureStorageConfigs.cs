namespace Infrastructure.Common.FileManagement
{
    public record AzureStorageConfigs
    {
        public string AccountName { get; set; } = string.Empty;
        public string DefaultContainer { get; set; } = string.Empty;
    }
}
