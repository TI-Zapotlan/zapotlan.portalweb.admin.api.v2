﻿using Zapotlan.PortalWeb.Admin.Core.Entities;

namespace Zapotlan.PortalWeb.Admin.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Gets();

        Task<T?> GetAsync(Guid id) { throw new NotImplementedException("Implementación no realizada para el parámetro de tipo Guid"); }
        Task<T?> GetAsync(int id) { throw new NotImplementedException("Implementación no realizada para el parámetro de tipo Int"); }

        Task AddAsync(T item);

        void Update(T item) { throw new NotImplementedException("Implementación no realizada para Update"); }
        Task UpdateAsync(T item) { throw new NotImplementedException("Implementación no realizada para UpdateAsync"); }

        Task DeleteAsync(Guid id) { throw new NotImplementedException("Implementación no realizada para el parámetro de tipo Guid"); }
        Task DeleteAsync(int id) { throw new NotImplementedException("Implementación no realizada para el parámetro de tipo Int"); }
    }
}
