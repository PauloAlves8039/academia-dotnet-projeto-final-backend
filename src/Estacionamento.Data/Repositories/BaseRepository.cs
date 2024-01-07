using Estacionamento.Data.Context;
using Estacionamento.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly EstacionamentoContext _contexto;

        public BaseRepository(EstacionamentoContext contexto)
        {
            _contexto = contexto;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _contexto.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await _contexto.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            _contexto.Set<TEntity>().Add(entity);
            await _contexto.SaveChangesAsync();
            return entity;
        }
        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await _contexto.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                _contexto.Set<TEntity>().Remove(entity);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}
