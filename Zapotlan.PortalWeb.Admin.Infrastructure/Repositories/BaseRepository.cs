using Microsoft.EntityFrameworkCore;
using Zapotlan.PortalWeb.Admin.Core.Entities;
using Zapotlan.PortalWeb.Admin.Core.Interfaces;
using Zapotlan.PortalWeb.Admin.Infrastructure.Data;

namespace Zapotlan.PortalWeb.Admin.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        //private readonly PortalWebDbContext _context; // No se utiliza, validar si es mejor quitarlo
        protected DbSet<T> _entity;

        // CONSTRUCTOR

        public BaseRepository(PortalWebDbContext context)
        {
            //_context = context;
            _entity = context.Set<T>();
        }

        // METHODS

        public virtual IEnumerable<T> Gets()
        {
            return _entity.AsEnumerable();
        }

        public virtual async Task<T?> GetAsync(Guid id)
        {
            return await _entity.FindAsync(id);
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public virtual async Task AddAsync(T item)
        {
            await _entity.AddAsync(item);
        }

        public virtual void Update(T item) // la sincronia se va a hacer en el unitOfWork
        {
            _entity.Update(item);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            T? item = await GetAsync(id);
            if (item != null)
            {
                _entity.Remove(item);
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            T? item = await GetAsync(id);
            if (item != null)
            {
                _entity.Remove(item);
            }
        }
    }
}
