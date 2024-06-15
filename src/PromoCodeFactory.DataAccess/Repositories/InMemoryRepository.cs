using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        private const string _entityNotFoundMessage = "Entity not found."; // todo Move to resource or localization resource

        private IList<T> _data = [];
        protected IEnumerable<T> Data { get => _data; set => _data = value.ToList(); }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<bool> CreateAsync(T model)
        {
            while (model.Id == Guid.Empty || _data.FirstOrDefault(i => i.Id == model.Id) != null)
                model.Id = Guid.NewGuid(); // Позже на это на себя может взять бд

            _data.Add(model);

            return Task.FromResult(true);
        }

        public Task<bool> UpdateByIdAsync(T model)
        {
            int targetIdx = _data.IndexOf(model);
            if (targetIdx == -1) 
                throw new InvalidOperationException(_entityNotFoundMessage); // todo create custom exception type
            _data[targetIdx] = model;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            T entity = _data.FirstOrDefault(x => x.Id == id)
                ?? throw new InvalidOperationException(_entityNotFoundMessage); // todo create custom exception type

            var result = _data.Remove(entity);

            return Task.FromResult(result);
        }
    }
}