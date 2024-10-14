using Microsoft.EntityFrameworkCore;
using Moq;
using Teppi.Application.Services;
using Teppi.Data.Persistence;
using Teppi.Share.Entities;

namespace Teppi.UnitTest.Service;

public class CategoryServiceTest
{
    private readonly CategoryService _service;

    public CategoryServiceTest()
    {
        var options = new DbContextOptions<TeppiDbContext>();
        _service = new CategoryService(new TeppiDbContext(options));
    }

    [Fact]
    public async Task CreateCategory_ReturnsTrue_WhenIdIsEven()
    {
        // Arrange
        string id = "2";

        // Act
        var result = await _service.DeleteCategory(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteCategory_ReturnsFalse_WhenIdIsOdd()
    {
        // Arrange
        string id = "3";

        // Act
        var result = await _service.DeleteCategory(id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteCategory_ThrowsException_WhenIdIsNotANumber()
    {
        // Arrange
        string id = "not a number";

        // Act & Assert
        await Assert.ThrowsAsync<System.FormatException>(() => _service.DeleteCategory(id));
    }
}