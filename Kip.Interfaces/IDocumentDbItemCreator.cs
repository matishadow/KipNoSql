using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace Kip.Interfaces
{
    public interface IDocumentDbItemCreator<in T>
    {
        Task<Document> CreateItemAsync(T item);
        Document CreateItem(T item);
    }
}