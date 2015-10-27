using System.Collections.Generic;
using System.Linq;
using Constraints.Abstract;

namespace Constraints.Repository
{
    internal class RepositoryWithKey<TKey, T> : IRepositoryWithKey<TKey, T> where T : IEntity
    {
        public IDictionary<TKey, T> list = new Dictionary<TKey, T>();
 
        public IEnumerable<T> GetItems()
        {
            return list.Select(x => x.Value).AsEnumerable();
        }

        public T GetItem(TKey key)
        {
            T value = default(T);
            if (this.list.ContainsKey(key)) // should get index here
                value = this.list[key];
            
            var test = value.IsValid(); // IEntity stuff

            return value;
        }

        public void AddItem(TKey key, T item)
        {
            this.list.Add(new KeyValuePair<TKey, T>(key, item));
        }
    }
}