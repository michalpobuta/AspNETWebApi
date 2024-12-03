using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ToDoWebApi.Controllers;
using ToDoWebApi.DTOs;
using ToDoWebApi.Models;
using ToDoWebApi.Services;

namespace ToDoWebApiUnitTests.Cotrollers;

public class CategoryControllerTests
{
    [Fact]
    public async Task GetCategories_ShouldReturnOkWithCategories_WhenCategoriesExist()
    {
        // Arrange
        var mockService = new Mock<IResourceService<CategoryModel>>();
        var mockUserService = new Mock<IResourceService<UserModel>>();
        var categories = new List<CategoryModel>
        {
            new() { Id = 1, Name = "Category1" },
            new() { Id = 2, Name = "Category2" }
        };
        mockService.Setup(service => service.GetResources()).ReturnsAsync(categories);

        var controller = new CategoryController(mockService.Object, mockUserService.Object);

        // Act
        var result = await controller.GetCategories();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeEquivalentTo(categories);
        okResult.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async Task GetCategory_ShouldReturnOkWithCategory_WhenCategoryExists()
    {
        // Arrange
        var mockService = new Mock<IResourceService<CategoryModel>>();
        var mockUserService = new Mock<IResourceService<UserModel>>();
        var category = new CategoryModel { Id = 1, Name = "Category1" };
        mockService.Setup(service => service.GetResourceById(1)).ReturnsAsync(category);

        var controller = new CategoryController(mockService.Object, mockUserService.Object);

        // Act
        var result = await controller.GetCategory(1);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeEquivalentTo(category);
        okResult.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async Task PostCategory_ShouldReturnOkWithCreatedCategory()
    {
        // Arrange
        var mockService = new Mock<IResourceService<CategoryModel>>();
        var mockUserService = new Mock<IResourceService<UserModel>>();
        var categoryDTO = new CategoryDTO() { Name = "NewCategory" };
        var category = new CategoryModel(categoryDTO, new UserModel());
        mockService.Setup(service => service.Create(category)).ReturnsAsync(category);

        var controller = new CategoryController(mockService.Object, mockUserService.Object);

        // Act
        var result = await controller.PostCategory(categoryDTO);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().BeEquivalentTo(category);
        okResult.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async Task PutCategory_ShouldReturnOk_WhenCategoryIsUpdated()
    {
        // Arrange
        var mockService = new Mock<IResourceService<CategoryModel>>();
        var mockUserService = new Mock<IResourceService<UserModel>>();
        var categoryDTO = new CategoryDTO() { Name = "UpdateCategory" };
        var category = new CategoryModel(categoryDTO, new UserModel());
        mockService.Setup(service => service.Update(category)).Returns(Task.CompletedTask);

        var controller = new CategoryController(mockService.Object, mockUserService.Object);

        // Act
        var result = await controller.PutCategory(categoryDTO);

        // Assert
        var okResult = result as OkResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);
    }
    
    [Fact]
    public async Task DeleteCategory_ShouldReturnOk_WhenCategoryIsDeleted()
    {
        // Arrange
        var mockService = new Mock<IResourceService<CategoryModel>>();
        var mockUserService = new Mock<IResourceService<UserModel>>();
        var categoryDTO = new CategoryDTO() { Name = "DeleteCategory" };
        var category = new CategoryModel(categoryDTO, new UserModel());
        mockService.Setup(service => service.Delete(category)).Returns(Task.CompletedTask);

        var controller = new CategoryController(mockService.Object, mockUserService.Object);

        // Act
        var result = await controller.DeleteCategory(categoryDTO);

        // Assert
        var okResult = result as OkResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(200);
    }
    
}