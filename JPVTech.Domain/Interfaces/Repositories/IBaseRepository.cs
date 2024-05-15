using JPVTech.Domain.Entities;
using JPVTech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
        Task<IList<TEntity>> Select();
        Task<TEntity> Select(int id);
        Task<(List<TEntity>, int)> SelectPaginated(PaginationModel paginationInfo);
    }
}
