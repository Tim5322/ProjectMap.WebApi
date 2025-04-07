using ProjectMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Repositories
{
    public interface IDagboekRepository
    {
        Task<Dagboek> InsertAsync(Dagboek dagboek);
        Task<Dagboek?> ReadAsync(Guid id);
        Task<IEnumerable<Dagboek>> ReadAsync();
        Task<IEnumerable<Dagboek>> ReadByProfielKeuzeIdAsync(Guid profielKeuzeId);
        Task UpdateAsync(Dagboek dagboek);
        Task DeleteAsync(Guid id);
    }
}
