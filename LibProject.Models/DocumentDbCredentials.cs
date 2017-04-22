namespace LibProject.Models
{
    public class DocumentDbCredentials
    {
        public string Endpoint { get; set; }
        public string AuthKey { get; set; }
        public string DatabaseId { get; set; }
        public string CollectionId { get; set; }
    }
}