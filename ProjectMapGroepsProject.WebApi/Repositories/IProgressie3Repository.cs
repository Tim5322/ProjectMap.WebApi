using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMapGroepsproject.WebApi.Models;

namespace ProjectMapGroepsproject.WebApi.Repositories
{
    public interface IProgressie3Repository
    {
        Task<IEnumerable<Progressie3>> GetAllAsync();
        Task<Progressie3?> GetByIdAsync(Guid id);
        Task<Progressie3> CreateAsync(Progressie3 progressie);
        Task<Progressie3?> UpdateAsync(Guid id, Progressie3 updatedProgressie);
        Task DeleteAsync(Guid id);
    }
}
