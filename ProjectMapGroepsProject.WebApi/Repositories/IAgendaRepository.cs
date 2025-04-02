using ProjectMapGroepsproject.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public interface IAgendaRepository
    {
        Task DeleteAsync(Guid id);
        Task<Agenda> InsertAsync(Agenda agenda);
        Task<IEnumerable<Agenda>> ReadAsync();
        Task<Agenda?> ReadAsync(Guid id);
        Task UpdateAsync(Agenda agenda);
    }
}