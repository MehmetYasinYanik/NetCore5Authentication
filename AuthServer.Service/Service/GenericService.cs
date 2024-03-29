﻿using AuthServer.Core.Repository;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Service.Service
{
    public class GenericService<TEntity, TDto> : IService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var products = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(products, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var product = await _genericRepository.GetByIdAsync(id);
            if (product == null)
            {
                return Response<TDto>.Fail("Id not found", 404, true);
            }
            else
            {
                return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(product), 200);
            }
        }

        public async Task<Response<EmptyDataDto>> Remove(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
                return Response<EmptyDataDto>.Fail("Data not found", 404, true);
            else 
            {
                _genericRepository.Remove(entity);
                await _unitOfWork.CommitAsync();
                return Response<EmptyDataDto>.Success(200);
            }    
        }

        public async Task<Response<EmptyDataDto>> Update(TDto entity, int id)
        {
            if (await _genericRepository.GetByIdAsync(id) == null)
                return Response<EmptyDataDto>.Fail("Data not found", 404, true);
            else
            {
                var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);

                _genericRepository.Update(updateEntity);
                await _unitOfWork.CommitAsync();
                //Response Body içerisinde data olmayacaktır
                return Response<EmptyDataDto>.Success(204);
            }
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}
