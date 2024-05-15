using AutoMapper;
using FluentValidation;
using JPVTech.Domain.Entities;
using JPVTech.Domain.Interfaces.Repositories;
using JPVTech.Domain.Interfaces.Services;
using JPVTech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPVTech.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;   
        }

        public async Task<TOutputModel> Add<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            
            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Insert(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task Delete(int id)
        {
            await _baseRepository.Delete(id);
        }

        public async Task<IList<TOutputModel>> GetAll<TOutputModel>() where TOutputModel : class
        {
            var entities = await _baseRepository.Select();

            var outputModels = entities.Select(s => _mapper.Map<TOutputModel>(s)).ToList();

            return outputModels;
        }

        public async Task<TOutputModel> GetById<TOutputModel>(int id) where TOutputModel : class
        {
            var entity = await _baseRepository.Select(id);

            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        public async Task<(List<TOutputModel>, int)> SelectPaginated<TOutputModel>(PaginationModel paginationInfo) where TOutputModel : class
        {
            (List<TEntity>, int) tupleGetAllResult = await _baseRepository.SelectPaginated(paginationInfo);

            List<TOutputModel> responseEntity = _mapper.Map<List<TOutputModel>>(tupleGetAllResult.Item1);
            (List<TOutputModel>, int) response = (responseEntity, tupleGetAllResult.Item2);

            return response;
        }

        public async Task<TOutputModel> Update<TInputModel, TOutputModel, TValidator>(TInputModel inputModel, int id)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            entity.Id = id;

            Validate(entity, Activator.CreateInstance<TValidator>());
            await _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            validator.ValidateAndThrow(obj);
        }
    }
}
