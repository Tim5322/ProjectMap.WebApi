using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMapGroepsproject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMapGroepsproject.WebApi.Repositories
{
    public class Progressie1Repository : IProgressie1Repository
    {
        private readonly string _connectionString;

        public Progressie1Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Haalt alle Progressie1 records op
        public async Task<IEnumerable<Progressie1>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Progressie1";
            return await connection.QueryAsync<Progressie1>(query);
        }

        // Haalt een Progressie1 record op via het Id
        public async Task<Progressie1?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Progressie1 WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Progressie1>(query, new { Id = id });
        }

        // Voegt een nieuwe Progressie1 record toe
        public async Task<Progressie1> CreateAsync(Progressie1 progressie)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Progressie1 (Id, NumberCompleet, vakje1, vakje2, vakje3, vakje4, vakje5, vakje6, ProfielKeuzeId) 
                          VALUES (@Id, @NumberCompleet, @vakje1, @vakje2, @vakje3, @vakje4, @vakje5, @vakje6, @ProfielKeuzeId)";
            await connection.ExecuteAsync(query, progressie);
            return progressie;
        }

        // Update een bestaand Progressie1 record
        public async Task<Progressie1?> UpdateAsync(Guid id, Progressie1 updatedProgressie)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Progressie1 SET 
                          NumberCompleet = @NumberCompleet,
                          vakje1 = @vakje1,
                          vakje2 = @vakje2,
                          vakje3 = @vakje3,
                          vakje4 = @vakje4,
                          vakje5 = @vakje5,
                          vakje6 = @vakje6,
                          ProfielKeuzeId = @ProfielKeuzeId
                          WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { updatedProgressie.NumberCompleet, updatedProgressie.vakje1, updatedProgressie.vakje2, updatedProgressie.vakje3, updatedProgressie.vakje4, updatedProgressie.vakje5, updatedProgressie.vakje6, updatedProgressie.ProfielKeuzeId, Id = id });

            if (rowsAffected == 0)
                return null;

            return updatedProgressie;
        }

        // Verwijdert een Progressie1 record op basis van het Id
        public async Task DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Progressie1 WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
