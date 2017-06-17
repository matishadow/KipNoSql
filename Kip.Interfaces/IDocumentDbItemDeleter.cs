using System;
using System.Threading.Tasks;

namespace Kip.Interfaces
{
    public interface IDocumentDbItemDeleter
    {
        Task DeleteItemAsync(Guid id);
        void DeleteItem(Guid id);
    }
}