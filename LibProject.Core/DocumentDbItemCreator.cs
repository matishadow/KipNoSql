using System;
using System.Threading.Tasks;
using LibProject.Interfaces;
using LibProject.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace LibProject.Core
{
    public class DocumentDbItemCreator<T> : DocumentDbBase, IDocumentDbItemCreator<T> where T : class
    {
        public DocumentDbItemCreator(DocumentDbCredentials documentDbCredentials, IDocumentClient documentClient)
            : base(documentDbCredentials, documentClient)
        {
        }

        public async Task<Document> CreateItemAsync(T item)
        {
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(DocumentDbCredentials.DatabaseId,
                DocumentDbCredentials.CollectionId);
            
            return await DocumentClient.CreateDocumentAsync(collectionUri, item);
        }

        public Document CreateItem(T item)
        {
            return CreateItemAsync(item).Result;
        }
    }
}
