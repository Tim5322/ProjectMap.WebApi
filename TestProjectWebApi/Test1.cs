using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Moq;
using Xunit;
using ProjectMapGroepsproject.WebApi.Repositories;
using ProjectMapGroepsproject.WebApi.Models;


public class DagboekRepositoryTests
{
    private readonly Mock<IDbConnection> _mockDbConnection;
    private readonly DagboekRepository _repository;

    public DagboekRepositoryTests()
    {
        _mockDbConnection = new Mock<IDbConnection>();
        _repository = new DagboekRepository("FakeConnectionString");
    }

    [Fact]
    public async Task ReadAsync_ReturnsDagboek_WhenIdExists()
    {
        // Arrange
        var mockReader = new Mock<IDataReader>();
        mockReader.Setup(r => r.Read()).Returns(true); // Simulate a row exists
        mockReader.Setup(r => r["Id"]).Returns(Guid.NewGuid());
        mockReader.Setup(r => r["DagboekBladzijde1"]).Returns("Page 1 Content");

        var mockCommand = new Mock<IDbCommand>();
        mockCommand.Setup(c => c.ExecuteReader()).Returns(mockReader.Object);

        _mockDbConnection.Setup(db => db.CreateCommand()).Returns(mockCommand.Object);

        // Act
        var result = await _repository.ReadAsync(Guid.NewGuid());

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Page 1 Content", result.DagboekBladzijde1);
    }
}
