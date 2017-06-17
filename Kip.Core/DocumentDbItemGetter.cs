using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kip.Interfaces;
using Kip.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Kip.Core
{
    public class DocumentDbItemGetter<T> : DocumentDbBase, IDocumentDbItemGetter<T> where T : class 
    {
        public DocumentDbItemGetter(DocumentDbCredentials documentDbCredentials, IDocumentClient documentClient)
            : base(documentDbCredentials, documentClient)
        {
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            try
            {
                Uri documentUri = UriFactory.CreateDocumentUri(DocumentDbCredentials.DatabaseId,
                    DocumentDbCredentials.CollectionId, id.ToString());

                Document document = await DocumentClient.ReadDocumentAsync(documentUri);

                return (T) (dynamic) document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;

                throw;
            }
        }

        public T GetItem(Guid id)
        {
            T item = GetItemAsync(id).Result;

            return item;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            const int unlimitedItemCount = -1;

            IDocumentQuery<T> query = DocumentClient.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DocumentDbCredentials.DatabaseId, DocumentDbCredentials.CollectionId),
                new FeedOptions { MaxItemCount = unlimitedItemCount })
                .Where(predicate)
                .AsDocumentQuery();

            var items = new List<T>();
            while (query.HasMoreResults)
                items.AddRange(await query.ExecuteNextAsync<T>());

            return items;
        }

        public IEnumerable<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            Task<IEnumerable<T>> items = GetItemsAsync(predicate);

            return items.Result;
        }

        public Task<IEnumerable<T>> GetItemsAsync()
        {
            return GetItemsAsync(arg => true);
        }

        public IEnumerable<T> GetItems()
        {
            return GetItems(arg => true);
        }
    }
}