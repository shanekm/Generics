using System.Collections.Generic;

namespace Constraints.Abstract
{
    public interface IRepositoryWithKey<TKey, T>
    {
        IEnumerable<T> GetItems();
        T GetItem(TKey key);
        void AddItem(TKey key, T item);
    }
}