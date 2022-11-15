using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.ContextFolder;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Repository
{
    public class BaseRepository<T, IT> where T : BaseEntity, IT
    {
        private Context _context;
        private DbSet<T> _dbSet;
        private IMapper _mapper;

        public BaseRepository(Context context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }
        public async Task<List<T>> GetEntities()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> InsertEntity(IT entityForInsert)
        {
            var entity = _mapper.Map<T>(entityForInsert);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> GetEntityByKey(params object[] keys)
        {
            var entityByKeys = await _dbSet.FindAsync(keys);
            return entityByKeys;
        }
        public async Task UpdateEntity(IT entity,params object[] keys)
        {
            try
            {
                var dbEntity =await GetEntityByKey(keys);
                _context.Entry(dbEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new DbUpdateException("No data to update.");
            }
          
        }
        public async Task DeleteEntity(T entityForDelete)
        {
            if (entityForDelete == null)
            {
                throw new DbUpdateException("No data to delete.");
            }
            _dbSet.Remove(entityForDelete);

            await _context.SaveChangesAsync();
        }
    }
}
