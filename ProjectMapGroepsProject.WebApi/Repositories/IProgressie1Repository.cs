using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMapGroepsproject.WebApi.Models;

namespace ProjectMapGroepsproject.WebApi.Repositories
{
    public interface IProgressie1Repository
    {
        Task<IEnumerable<Progressie1>> GetAllAsync();
        Task<Progressie1?> GetByIdAsync(Guid id);
        Task<Progressie1> CreateAsync(Progressie1 progressie);
        Task<Progressie1?> UpdateAsync(Guid id, Progressie1 updatedProgressie);
        Task DeleteAsync(Guid id);
    }
}
