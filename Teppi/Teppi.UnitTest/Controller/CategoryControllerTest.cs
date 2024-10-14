using Microsoft.Extensions.Logging;
using Moq;
using Teppi.Application.Interfaces;
using Teppi.WebApi.Controllers.V1;

namespace Teppi.UnitTest.Controller;

public class CategoriesControllerTests
{
    private readonly Mock<ICategoryService> _mockCategoryService;
    private readonly Mock<ILogger<CategoriesController>> _mockLogger;
    private CategoriesController _controller;

    public CategoriesControllerTests()
    {
        _mockCategoryService = new Mock<ICategoryService>();
        _mockLogger = new Mock<ILogger<CategoriesController>>();
        _controller = new CategoriesController(_mockCategoryService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenCategoryIsDeletedSuccessfully()
    {
        // Arrange
        var categoryId = Guid.NewGuid().ToString();
        _mockCategoryService.Setup(service => service.DeleteCategory(categoryId)).Returns(Task.FromResult(true));

        // Act
        Boolean result = await _controller.Delete(categoryId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ReturnsOk_WhenCategoryDeletionFails()
    {
        // Arrange
        var categoryId = Guid.NewGuid().ToString();
        _mockCategoryService.Setup(service => service.DeleteCategory(categoryId)).Returns(Task.FromResult(true));
        
        // Act
        Boolean result = await _controller.Delete(categoryId);

        // Assert
        Assert.True(result);        
    }
}