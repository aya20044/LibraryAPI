using AutoMapper;
using LibraryAPI.Data;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Services
{
    public class GenericService<TEntity, TDto>
        : IGenericService<TEntity, TDto>
        where TEntity : class,IEntity
    {
        protected readonly AppDbContext Context;
        protected readonly IMapper Mapper;
        protected readonly DbSet<TEntity> Entities;

        public GenericService(
            AppDbContext context,
            IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
            Entities = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TDto>> GetAll()
        {
            var entities = await Entities
                .AsNoTracking()
                .ToListAsync();

            return Mapper.Map<List<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetById(int id)
        {
            var entity = await Entities
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id == id);

            if (entity == null)
                return default;

            return Mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> Add(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();

            return Mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto?> Edit(int id, TDto dto)
        {
            var entity = await Entities.FindAsync(id);

            if (entity == null)
                return default;

            Mapper.Map(dto, entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<TDto>(entity);
        }

        public virtual async Task<bool> Delete(int id)
        {
            var entity = await Entities.FindAsync(id);

            if (entity == null)
                return false;

            Entities.Remove(entity);
            await Context.SaveChangesAsync();

            return true;
        }
    }
}