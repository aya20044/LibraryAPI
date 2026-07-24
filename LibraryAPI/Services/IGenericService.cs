using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public interface IGenericService <TEntity,TDto>
        where TEntity : class ,IEntity
    {
        Task<IEnumerable<TDto>> GetAll();

        Task<TDto?> GetById(int id);

        Task<TDto> Add(TDto dto);

        Task<TDto?> Edit(int id, TDto dto);

        Task<bool> Delete(int id);
    }
}
