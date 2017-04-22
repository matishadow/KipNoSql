using System;
using System.Threading.Tasks;
using LibProject.Interfaces;
using LibProject.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace LibProject.Core
{
    public class DocumentDbItemDeleter : DocumentDbBase, IDocumentDbItemDeleter
    {
        public DocumentDbItemDeleter(DocumentDbCredentials documentDbCredentials, IDocumentClient documentClient)
            : base(documentDbCredentials, documentClient)
        {
        }

        public async Task DeleteItemAsync(Guid id)
        {
            Uri documentUri = UriFactory.CreateDocumentUri(DocumentDbCredentials.DatabaseId,
                DocumentDbCredentials.CollectionId, id.ToString());

            await DocumentClient.DeleteDocumentAsync(documentUri);
        }

        public void DeleteItem(Guid id)
        {
            DeleteItemAsync(id).RunSynchronously();
        }
    }
}