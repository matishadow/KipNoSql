using System;
using System.Threading.Tasks;
using Kip.Interfaces;
using Kip.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Kip.Core
{
    public class DocumentDbItemUpdater<T> : DocumentDbBase, IDocumentDbItemUpdater<T> where T : class
    {
        public DocumentDbItemUpdater(DocumentDbCredentials documentDbCredentials, IDocumentClient documentClient)
            : base(documentDbCredentials, documentClient)
        {
        }

        public async Task<Document> UpdateItemAsync(Guid id, T item)
        {
            Uri documentUri = UriFactory.CreateDocumentUri(DocumentDbCredentials.DatabaseId,
                DocumentDbCredentials.CollectionId, id.ToString());

            return await DocumentClient.ReplaceDocumentAsync(documentUri, item);
        }

        public Document UpdateItem(Guid id, T item)
        {
            return UpdateItemAsync(id, item).Result;
        }
    }
}
