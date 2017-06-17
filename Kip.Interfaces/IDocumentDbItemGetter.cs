﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kip.Interfaces
{
    public interface IDocumentDbItemGetter<T>
    {
        Task<T> GetItemAsync(Guid id);
        T GetItem(Guid id);

        Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetItems(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetItemsAsync();
        IEnumerable<T> GetItems();
    }
}