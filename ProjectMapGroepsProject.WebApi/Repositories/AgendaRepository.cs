using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMapGroepsproject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Repositories
{
    public class AgendaRepository
    {
        private readonly string _sqlConnectionString;

        public AgendaRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public async Task<Agenda> InsertAsync(Agenda agenda)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                agenda.Id = Guid.NewGuid();
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO [Agenda] (Id, date1, location1, date2, location2, date3, location3, UserId) " +
                    "VALUES (@Id, @date1, @location1, @date2, @location2, @date3, @location3, @UserId)", agenda);
                return agenda;
            }
        }

        public async Task<Agenda?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Agenda>(
                    "SELECT * FROM [Agenda] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Agenda>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Agenda>("SELECT * FROM [Agenda]");
            }
        }

        public async Task UpdateAsync(Agenda agenda)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "UPDATE [Agenda] SET " +
                    "date1 = @date1, location1 = @location1, " +
                    "date2 = @date2, location2 = @location2, " +
                    "date3 = @date3, location3 = @location3 " +
                    "WHERE Id = @Id", agenda);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Agenda] WHERE Id = @Id", new { id });
            }
        }
    }
}
