using FluentAssertions;
using ToDoWebApi.Models;
using ToDoWebApi.Services;


namespace ToDoWebApiUnitTests.Services;

public class CategoryServiceTests
{
    [Fact]
    public async Task Create_ShouldAddCategory_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryService = new CategoryService();
        var category = new CategoryModel { Id = 1, Name = "TestCategory" };

        // Act
        var result = await categoryService.Create(category);

        // Assert
        result.Should().BeEquivalentTo(category);
    }
    
    [Fact]
    public async Task GetResourceById_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryService = new CategoryService();
        var category = new CategoryModel { Id = 1, Name = "TestCategory" };
        await categoryService.Create(category);

        // Act
        var result = await categoryService.GetResourceById(1);

        // Assert
        result.Should().BeEquivalentTo(category);
    }

    [Fact]
    public async Task GetResources_ShouldReturnAllCategories()
    {
        // Arrange
        var categoryService = new CategoryService();
        var category1 = new CategoryModel { Id = 1, Name = "Category1" };
        var category2 = new CategoryModel { Id = 2, Name = "Category2" };
        await categoryService.Create(category1);
        await categoryService.Create(category2);

        // Act
        var result = await categoryService.GetResources();

        // Assert
        result.Should().HaveCount(2)
            .And.ContainEquivalentOf(category1)
            .And.ContainEquivalentOf(category2);
    }

    
    [Fact]
    public async Task Update_ShouldUpdateCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryService = new CategoryService();
        var category = new CategoryModel { Id = 1, Name = "OldName" };
        await categoryService.Create(category);
        var updatedCategory = new CategoryModel { Id = 1, Name = "NewName" };

        // Act
        await categoryService.Update(updatedCategory);
        var result = await categoryService.GetResourceById(1);

        // Assert
        result.Name.Should().Be("NewName");
    }

    [Fact]
    public async Task Delete_ShouldRemoveCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryService = new CategoryService();
        var category = new CategoryModel { Id = 1, Name = "ToDelete" };
        await categoryService.Create(category);

        // Act
        await categoryService.Delete(category);

        // Assert
        var act = async () => await categoryService.GetResourceById(1);
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }


}