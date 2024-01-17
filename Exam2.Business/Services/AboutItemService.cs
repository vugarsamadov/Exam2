using AutoMapper;
using Exam2.Business.Models;
using Exam2.Business.Pagination;
using Exam2.Business.Services.Abstract;
using Exam2.Core.Entities;
using Exam2.Infrastructure.Repositories;
using Exam2.Infrastructure.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam2.Business.Services
{
    public class AboutItemService : IAboutItemService
    {

        private IGenericRepository<AboutItem> _aboutItemRepository { get; }
        private IMapper _mapper { get; }

        public AboutItemService(IGenericRepository<AboutItem> aboutItemRepository
            ,IMapper mapper)
        {
            _aboutItemRepository = aboutItemRepository;
            _mapper = mapper;
        }


        public async Task CreateAsync(AboutItemCreateVM model)
        {
            var entity = _mapper.Map<AboutItem>(model);
            await _aboutItemRepository.CreateAsync(entity);
            await _aboutItemRepository.SaveChangesAsync();
        }

        public async Task<GenericPaginatedEntity<AboutItemVM>> GetAllPaginatedAsync(int currentpage, int perPage)
        {
            var entites = await _aboutItemRepository.GetAllPaginatedAsync(currentpage,perPage,true);
            var count = await _aboutItemRepository.GetCountAsync();
            var models = _mapper.Map<IEnumerable<AboutItemVM>>(entites);
            var paginatedModel = new GenericPaginatedEntity<AboutItemVM>(models,currentpage,perPage,count);

            return paginatedModel;
        }

        public async Task<AboutItemVM> GetByIdAsync(int id)
        {
            var entity = await _aboutItemRepository.GetByIdAsync(id,false);
            var model = _mapper.Map<AboutItemVM>(entity);
            return model;
        }

        public async Task<IEnumerable<AboutItemVM>> GetLastNAsync(int n)
        {
            var entities= await _aboutItemRepository
                .GetLastNAsync(n,false);
            var models = _mapper.Map<IEnumerable<AboutItemVM>>(entities);
            return models;
        }

        public async Task RevokeDeleteAsync(int id)
        {
            var entity = await _aboutItemRepository.GetByIdAsync(id,true);
            if(entity != null)
                entity.RevokeDelete();
            await _aboutItemRepository.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var entity = await _aboutItemRepository.GetByIdAsync(id, true);
            if (entity != null)
                entity.Delete();
            await _aboutItemRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, AboutItemUpdateVM model)
        {
            var entity = await _aboutItemRepository.GetByIdAsync(id, true);
            if (entity != null)
                _mapper.Map(model, entity);
            _aboutItemRepository.Update(entity);
            await _aboutItemRepository.SaveChangesAsync();
        }
    }
}
