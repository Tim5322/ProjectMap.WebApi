using ProjectMapGroepsproject.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public interface IDagboekRepository
    {
        Task DeleteAsync(Guid id);
        Task<Dagboek> InsertAsync(Dagboek dagboek);
        Task<IEnumerable<Dagboek>> ReadAsync();
        Task<Dagboek?> ReadAsync(Guid id);
        Task<IEnumerable<Dagboek>> ReadByProfielKeuzeIdAsync(Guid profielKeuzeId);
        Task UpdateAsync(Dagboek dagboek);
    }
}
