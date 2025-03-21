using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMap.WebApi.Models;

namespace ProjectMap.WebApi.Repositories
{
    public class ProfielKeuzeRepository
    {
        private readonly string sqlConnectionString;

        public ProfielKeuzeRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<ProfielKeuze> InsertAsync(ProfielKeuze profielKeuze)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO [ProfielKeuze] (Id, Name, Leeftijd) VALUES (@Id, @Name, @Leeftijd)", profielKeuze);
                return profielKeuze;
            }
        }

        public async Task<ProfielKeuze?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<ProfielKeuze>("SELECT * FROM [ProfielKeuze] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<ProfielKeuze>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<ProfielKeuze>("SELECT * FROM [ProfielKeuze]");
            }
        }

        public async Task UpdateAsync(ProfielKeuze profielKeuze)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [ProfielKeuze] SET " +
                                                 "Name = @Name, " +
                                                 "Leeftijd = @Leeftijd " +
                                                 "WHERE Id = @Id", profielKeuze);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [ProfielKeuze] WHERE Id = @Id", new { id });
            }
        }
    }
}
