using System;
using System.Threading.Tasks;
using Kip.Interfaces;
using Kip.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Kip.Core
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
