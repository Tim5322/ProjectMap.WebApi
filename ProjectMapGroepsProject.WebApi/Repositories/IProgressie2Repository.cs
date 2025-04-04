using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectMapGroepsproject.WebApi.Models;

namespace ProjectMapGroepsproject.WebApi.Repositories
{
    public interface IProgressie2Repository
    {
        Task<IEnumerable<Progressie2>> GetAllAsync();
        Task<Progressie2?> GetByIdAsync(Guid id);
        Task<Progressie2> CreateAsync(Progressie2 progressie);
        Task<Progressie2?> UpdateAsync(Guid id, Progressie2 updatedProgressie);
        Task DeleteAsync(Guid id);
    }
}
