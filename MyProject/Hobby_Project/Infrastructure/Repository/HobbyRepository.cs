﻿using Application.Repositories;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class HobbyRepository : IHobbyArticleRepository
    {
        private readonly HobbyDbContext _context;

        public HobbyRepository(HobbyDbContext context)
        {
            _context = context;
        }

        public async Task<HobbyEntity> Add(HobbyEntity entity)
        {
             _context.HobbyEntities.Attach(entity).State = EntityState.Added;
             return entity;
        }

        public async Task DeleteAsync(int id)
        {
            HobbyEntity articleForDeleting = await FindById(id);

            _context.HobbyEntities.Remove(articleForDeleting);
        }

        public async Task<IEnumerable<HobbyEntity>> GetHobbyArticlesByUserId(int id)
        {
            return _context.HobbyEntities
                .Include(x => x.User)
                .Include(h => h.HobbyPhoto)
                .AsQueryable()
                .Where(h=>h.User.Id==id);
        }
        
        public async Task<IEnumerable<HobbyEntity>> GetAllEntitiesAsync()
        {
            return _context.HobbyEntities
                .Include(x=>x.User)
                .Include(x => x.HobbyPhoto)
                .AsQueryable();
        }
        public async Task<HobbyEntity> GetByIdAsync(int id)
        {
           await FindById(id);

            return await _context.HobbyEntities
                    .Include(h => h.HobbyPhoto)
                    .SingleAsync(x => x.Id == id);
        }
      
        public async Task<HobbyEntity> Update(HobbyEntity hobbyArticle)
        {
            _context.HobbyEntities.Update(hobbyArticle);
            return hobbyArticle;
        }
        
        public async Task<IEnumerable<HobbyEntity>> GetHobbyArticlesByUsername(string username)
        {
            return _context.HobbyEntities
                     .Include(h => h.HobbyPhoto)
                     .AsQueryable() 
                     .Where(h => h.User.Username.Equals(username));
        }

        public async Task<HobbyEntity> FindById(int id)
        {
            var hobby = await _context.HobbyEntities.FirstOrDefaultAsync(h => h.Id == id);

            if (hobby == null) throw new NullReferenceException($"HobbyArticle with Id: {id} does not exist");
            return hobby;
        }
    }
}
