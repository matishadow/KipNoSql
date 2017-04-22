using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace LibProject.Interfaces
{
    public interface IDocumentDbItemUpdater<in T>
    {
        Task<Document> UpdateItemAsync(Guid id, T item);
        Document UpdateItem(Guid id, T item);
    }
}