﻿using Dapper;
using Microsoft.Data.SqlClient;
using ProjectMapGroepsproject.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectMapGroepsproject.WebApi.Repositories
{
    public class Progressie3Repository : IProgressie3Repository
    {
        private readonly string _connectionString;

        public Progressie3Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Haalt alle Progressie3 records op
        public async Task<IEnumerable<Progressie3>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Progressie3";
            return await connection.QueryAsync<Progressie3>(query);
        }

        // Haalt een Progressie3 record op via het Id
        public async Task<Progressie3?> GetByIdAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Progressie3 WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Progressie3>(query, new { Id = id });
        }

        // Voegt een nieuwe Progressie3 record toe
        public async Task<Progressie3> CreateAsync(Progressie3 progressie)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"INSERT INTO Progressie3 (Id, NumberCompleet, vakje1, vakje2, vakje3, vakje4, vakje5, vakje6, vakje7, vakje8, ProfielKeuzeId) 
                          VALUES (@Id, @NumberCompleet, @vakje1, @vakje2, @vakje3, @vakje4, @vakje5, @vakje6, @vakje7, @vakje8, @ProfielKeuzeId)";
            await connection.ExecuteAsync(query, progressie);
            return progressie;
        }

        // Update een bestaand Progressie3 record
        public async Task<Progressie3?> UpdateAsync(Guid id, Progressie3 updatedProgressie)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = @"UPDATE Progressie3 SET 
                          NumberCompleet = @NumberCompleet,
                          vakje1 = @vakje1,
                          vakje2 = @vakje2,
                          vakje3 = @vakje3,
                          vakje4 = @vakje4,
                          vakje5 = @vakje5,
                          vakje6 = @vakje6,
                          vakje7 = @vakje7,
                          vakje8 = @vakje8,
                          ProfielKeuzeId = @ProfielKeuzeId
                          WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { updatedProgressie.NumberCompleet, updatedProgressie.vakje1, updatedProgressie.vakje2, updatedProgressie.vakje3, updatedProgressie.vakje4, updatedProgressie.vakje5, updatedProgressie.vakje6, updatedProgressie.vakje7, updatedProgressie.vakje8, updatedProgressie.ProfielKeuzeId, Id = id });

            if (rowsAffected == 0)
                return null;

            return updatedProgressie;
        }

        // Verwijdert een Progressie3 record op basis van het Id
        public async Task DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "DELETE FROM Progressie3 WHERE Id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
