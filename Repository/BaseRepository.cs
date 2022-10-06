using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.ContextFolder;

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
        public async Task UpdateEntity(IT entityForUpdate)
        {
            var entity = _mapper.Map<T>(entityForUpdate);
            ////_dbSet.Attach(entity);
            ////_context.Entry(entity).State = EntityState.Modified;
            ////await _context.SaveChangesAsync();
            ////var entityDb = await GetEntityByKey(keys);
            ////if (entityDb == null)
            ////{
            ////    throw new DbUpdateException("No data to be updated.");
            ////}
            //_dbSet.Update(entity);

            //_context.Entry(entityDb).CurrentValues.SetValues(entityForUpdate);
            //await _context.SaveChangesAsync();
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
