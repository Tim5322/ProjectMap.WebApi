using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Models;
using ProjectMapGroepsproject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMap.WebApi.Repositories
{
    public class DagboekRepository
    {
        private readonly string _sqlConnectionString;

        public DagboekRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public async Task<Dagboek> InsertAsync(Dagboek dagboek)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                dagboek.Id = Guid.NewGuid();
                await sqlConnection.ExecuteAsync(
                    "INSERT INTO [Dagboek] (Id, DagboekBladzijde1, DagboekBladzijde2, DagboekBladzijde3, DagboekBladzijde4) " +
                    "VALUES (@Id, @DagboekBladzijde1, @DagboekBladzijde2, @DagboekBladzijde3, @DagboekBladzijde4)", dagboek);
                return dagboek;
            }
        }

        public async Task<Dagboek?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Dagboek>(
                    "SELECT * FROM [Dagboek] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Dagboek>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Dagboek>("SELECT * FROM [Dagboek]");
            }
        }

        public async Task UpdateAsync(Dagboek dagboek)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync(
                    "UPDATE [Dagboek] SET " +
                    "DagboekBladzijde1 = @DagboekBladzijde1, " +
                    "DagboekBladzijde2 = @DagboekBladzijde2, " +
                    "DagboekBladzijde3 = @DagboekBladzijde3, " +
                    "DagboekBladzijde4 = @DagboekBladzijde4 " +
                    "WHERE Id = @Id", dagboek);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Dagboek] WHERE Id = @Id", new { id });
            }
        }
    }
}
