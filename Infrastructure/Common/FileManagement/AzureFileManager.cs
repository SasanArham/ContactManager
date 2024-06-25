using Application.Common;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Common.FileManagement
{
    internal class AzureFileManager : IFileManager
    {
        private readonly AzureStorageConfigs _azureStorageConfigs;

        public AzureFileManager(IOptions<AzureStorageConfigs> azureStorageConfigs)
        {
            _azureStorageConfigs = azureStorageConfigs.Value;
        }

        public async Task Upload(IFormFile file, string destinationPath)
        {
            var blobServiceClient = new BlobServiceClient(new Uri(AzureStorageHelper.StoragePath(_azureStorageConfigs.AccountName)), new DefaultAzureCredential());

            var blobUri = new Uri($"{AzureStorageHelper.StoragePath(_azureStorageConfigs.AccountName)}/{_azureStorageConfigs.DefaultContainer}/{destinationPath}");
            var userDelegationKey = await blobServiceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddMinutes(2));
            Uri blobSASURI = CreateUserDelegationSASBlob(_azureStorageConfigs.DefaultContainer, destinationPath, blobUri, _azureStorageConfigs.AccountName
                , userDelegationKey, BlobSasPermissions.Create);

            BlobClient blobClientSAS = new BlobClient(blobSASURI);
            using (var uploadFileStream = file.OpenReadStream())
            {
                await blobClientSAS.UploadAsync(uploadFileStream);
                uploadFileStream.Close();
            }
        }

        private Uri CreateUserDelegationSASBlob(string containerName, string blobName, Uri blobUri, string accountName, UserDelegationKey userDelegationKey
            , BlobSasPermissions permissions)
        {
            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(1)
            };

            sasBuilder.SetPermissions(permissions);

            // Add the SAS token to the blob URI
            BlobUriBuilder uriBuilder = new BlobUriBuilder(blobUri)
            {
                // Specify the user delegation key
                Sas = sasBuilder.ToSasQueryParameters(
                    userDelegationKey,
                    accountName)
            };

            return uriBuilder.ToUri();
        }

        public async Task<string> GetDownloadUrl(string url)
        {
            var blobServiceClient = new BlobServiceClient(new Uri(AzureStorageHelper.StoragePath(_azureStorageConfigs.AccountName)), new DefaultAzureCredential());
            var blobUri = new Uri($"{AzureStorageHelper.StoragePath(_azureStorageConfigs.AccountName)}/{_azureStorageConfigs.DefaultContainer}/{url}");
            var userDelegationKey = await blobServiceClient.GetUserDelegationKeyAsync(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddMinutes(2));
            Uri blobSASURI = CreateUserDelegationSASBlob(_azureStorageConfigs.DefaultContainer, url, blobUri, _azureStorageConfigs.AccountName
                , userDelegationKey, BlobSasPermissions.Read);
            return blobSASURI.ToString();

        }
    }
}
