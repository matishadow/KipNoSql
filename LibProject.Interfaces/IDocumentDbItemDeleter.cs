using System;
using System.Threading.Tasks;

namespace LibProject.Interfaces
{
    public interface IDocumentDbItemDeleter
    {
        Task DeleteItemAsync(Guid id);
        void DeleteItem(Guid id);
    }
}