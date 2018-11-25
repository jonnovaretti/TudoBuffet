using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace TudoBuffet.Website.Infrastructures
{
    public class BlobAccess
    {
        private readonly string storageConnection;

        public BlobAccess(string storageConnection)
        {
            this.storageConnection = storageConnection;
        }

        public async Task<string> UploadToBlob(string filename, string containerName, Stream stream)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            CloudBlobClient cloudBlobClient;
            CloudBlockBlob cloudBlockBlob;

            if (!CloudStorageAccount.TryParse(storageConnection, out storageAccount))
                throw new StorageException("Não foi possível conectar no blob");

            cloudBlobClient = storageAccount.CreateCloudBlobClient();

            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            await cloudBlobContainer.CreateIfNotExistsAsync();
            
            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);

            await cloudBlockBlob.UploadFromStreamAsync(stream);

            return cloudBlockBlob.Uri.AbsoluteUri;
        }
    }
}
