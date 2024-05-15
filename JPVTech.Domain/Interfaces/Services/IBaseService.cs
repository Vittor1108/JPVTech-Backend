using FluentValidation;
using JPVTech.Domain.Entities;
using JPVTech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity: BaseEntity
    {
        Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
        where TValidator : AbstractValidator<TEntity>
        where TInputModel : class
        where TOutputModel : class;

        Task Delete(int id);
        Task<IList<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class;

        Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class;
        Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel, int id)
           where TValidator : AbstractValidator<TEntity>
           where TInputModel : class
           where TOutputModel : class;

        Task<(List<TOutputModel>, int)> SelectPaginated<TOutputModel>(PaginationModel paginationInfo) where TOutputModel : class;
    }
}
