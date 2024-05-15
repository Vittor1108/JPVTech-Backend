using JPVTech.Data.Context;
using JPVTech.Domain.Entities;
using JPVTech.Domain.Interfaces.Repositories;
using JPVTech.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqlContext _dbContext;
        public BaseRepository(SqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int id)
        {
            TEntity entity = await Select(id);

            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Insert(TEntity entity)
        {            
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<TEntity>> Select()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Select(int id)
        {
            return await _dbContext.Set<TEntity>().FirstAsync(x => x.Id == id);
        }

        public async Task<(List<TEntity>, int)> SelectPaginated(PaginationModel paginationInfo)
        {
            int pageNumber = paginationInfo.PageNumber;
            int pageSize = paginationInfo.PageSize;

            IQueryable<TEntity> baseQuery = _dbContext.Set<TEntity>();

            List<TEntity> entities = await baseQuery.Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();

            int totalRecords = await baseQuery.CountAsync();

            (List<TEntity>, int) response = (entities, totalRecords);

            return response;
        }

        public async Task Update(TEntity entity)
        {            
            entity.UpdatedAt = DateTime.Now;
            _dbContext.Set<TEntity>().Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
