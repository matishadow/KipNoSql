namespace LibProject.Models
{
    public class DocumentDbCredentials
    {
        public DocumentDbCredentials(string endpoint, string authKey, string databaseId, string collectionId)
        {
            Endpoint = endpoint;
            AuthKey = authKey;
            DatabaseId = databaseId;
            CollectionId = collectionId;
        }

        public string Endpoint { get; set; }
        public string AuthKey { get; set; }
        public string DatabaseId { get; set; }
        public string CollectionId { get; set; }
    }
}