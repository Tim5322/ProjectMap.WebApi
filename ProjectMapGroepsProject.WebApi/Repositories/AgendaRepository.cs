using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMapGroepsproject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly string _sqlConnectionString;

        public AgendaRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public async Task<Agenda> InsertAsync(Agenda agenda)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    agenda.Id = Guid.NewGuid();
                    await sqlConnection.ExecuteAsync(
                        "INSERT INTO [Agenda] (Id, date1, location1, date2, location2, date3, location3, ProfielKeuzeId) " +
                        "VALUES (@Id, @date1, @location1, @date2, @location2, @date3, @location3, @ProfielKeuzeId)", agenda);
                    return agenda;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error occurred while inserting agenda.", ex);
            }
        }

        public async Task<Agenda?> ReadAsync(Guid id)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    return await sqlConnection.QuerySingleOrDefaultAsync<Agenda>(
                        "SELECT * FROM [Agenda] WHERE Id = @Id", new { id });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error occurred while reading agenda.", ex);
            }
        }

        public async Task<IEnumerable<Agenda>> ReadAsync()
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    return await sqlConnection.QueryAsync<Agenda>("SELECT * FROM [Agenda]");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error occurred while reading agendas.", ex);
            }
        }

        public async Task<IEnumerable<Agenda>> ReadByProfielKeuzeIdAsync(Guid profielKeuzeId)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    return await sqlConnection.QueryAsync<Agenda>(
                        "SELECT * FROM [Agenda] WHERE ProfielKeuzeId = @ProfielKeuzeId", new { profielKeuzeId });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error occurred while reading agendas by profiel keuze id.", ex);
            }
        }

        public async Task UpdateAsync(Agenda agenda)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync(
                        "UPDATE [Agenda] SET " +
                        "date1 = @date1, location1 = @location1, " +
                        "date2 = @date2, location2 = @location2, " +
                        "date3 = @date3, location3 = @location3, " +
                        "ProfielKeuzeId = @ProfielKeuzeId " +
                        "WHERE Id = @Id", agenda);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error occurred while updating agenda.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(_sqlConnectionString))
                {
                    await sqlConnection.ExecuteAsync("DELETE FROM [Agenda] WHERE Id = @Id", new { id });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new Exception("Error occurred while deleting agenda.", ex);
            }
        }
    }
}
