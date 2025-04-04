using ProjectMapGroepsproject.WebApi.Models;

public interface IAgendaRepository
{
    Task DeleteAsync(Guid id);
    Task<Agenda> InsertAsync(Agenda agenda);
    Task<IEnumerable<Agenda>> ReadAsync();
    Task<Agenda?> ReadAsync(Guid id);
    Task<IEnumerable<Agenda>> ReadByProfielKeuzeIdAsync(Guid profielKeuzeId);
    Task UpdateAsync(Agenda agenda);
}
