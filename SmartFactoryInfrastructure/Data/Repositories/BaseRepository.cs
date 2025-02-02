using SmartFactoryDomain.Interfaces.Repository;
using System.Collections.Concurrent;

namespace SmartFactoryInfrastructure.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ConcurrentDictionary<int, T> _storage = new();
        private int _currentId = 1;

        public Task<T> CreateAsync(T entity)
        {
            int id = _currentId++;
            var entityWithId = SetId(entity, id);
            _storage[id] = entityWithId;
            return Task.FromResult(entityWithId);
        }

        public Task<bool> DeleteAsync(T entity)
        {
            int? id = GetId(entity);
            if (id.HasValue && _storage.Remove(id.Value, out _))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_storage.Values.AsEnumerable());
        }

        public Task<T?> GetByIdAsync(int id)
        {
            _storage.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }

        public Task<T?> GetByIdAsync(Guid id) => Task.FromResult<T?>(null);

        public Task<T> UpdateAsync(T entity)
        {
            int? id = GetId(entity);
            if (id.HasValue && _storage.ContainsKey(id.Value))
            {
                _storage[id.Value] = entity;
                return Task.FromResult(entity);
            }
            throw new KeyNotFoundException("Entity not found.");
        }

        private T SetId(T entity, int id)
        {
            var prop = typeof(T).GetProperty("Id");
            prop?.SetValue(entity, id);
            return entity;
        }

        private int? GetId(T entity)
        {
            var prop = typeof(T).GetProperty("Id");
            return prop?.GetValue(entity) as int?;
        }
    }
}
