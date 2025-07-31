using AutoMapper;
using Moq;
using SuperMarketAPI;
using SuperMarketAPI.DTOs;
using SuperMarketAPI.Models;
using SuperMarketAPI.Repositories.Interfaces;
using SuperMarketAPI.Services;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _repoMock;
    private readonly IMapper _mapper;
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _repoMock = new Mock<ICategoryRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();

        _service = new CategoryService(_repoMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        // Arrange
        var fakeCategories = new List<Category>
        {
            new Category { Id = 1, Name = "Drinks" },
            new Category { Id = 2, Name = "Snacks" }
        };
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeCategories);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        var list = Assert.IsAssignableFrom<IEnumerable<CategoryReadDto>>(result);
        Assert.Collection(list,
            item => Assert.Equal("Drinks", item.Name),
            item => Assert.Equal("Snacks", item.Name));
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsMappedDto_WhenCategoryExists()
    {
        // Arrange
        var category = new Category { Id = 1, Name = "Dairy" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(category);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Dairy", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsCategoryAndReturnsMappedDto()
    {
        // Arrange
        var dto = new CategoryCreateDto { Name = "Fruits" };
        var category = new Category { Id = 1, Name = "Fruits" };
        _repoMock.Setup(r => r.AddAsync(It.IsAny<Category>())).Callback<Category>(c => c.Id = 1);
        // Act
        var result = await _service.CreateAsync(dto);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Fruits", result.Name);
        _repoMock.Verify(r => r.AddAsync(It.IsAny<Category>()), Times.Once);
    }
    [Fact]
    public async Task UpdateAsync_UpdatesCategory_WhenExists()
    {
        // Arrange
        var existingCategory = new Category { Id = 1, Name = "Vegetables" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingCategory);
        var updateDto = new CategoryUpdateDto { Name = "Green Vegetables" };
        // Act
        await _service.UpdateAsync(1, updateDto);
        // Assert
        Assert.Equal("Green Vegetables", existingCategory.Name);
        _repoMock.Verify(r => r.UpdateAsync(existingCategory), Times.Once);
    }
    [Fact]
    public async Task UpdateAsync_ThrowsException_WhenCategoryDoesNotExist()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category?)null);
        var updateDto = new CategoryUpdateDto { Name = "Non-Existent" };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(999, updateDto));
        _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Category>()), Times.Never);
    }
    [Fact]
    public async Task DeleteAsync_DeletesCategory_WhenExists()
    {
        // Arrange
        var existingCategory = new Category { Id = 1, Name = "Bakery" };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingCategory);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _repoMock.Verify(r => r.DeleteAsync(existingCategory), Times.Once);
    }
    [Fact]
    public async Task DeleteAsync_ThrowsException_WhenCategoryDoesNotExist()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(999));
        _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Category>()), Times.Never);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyList_WhenNoCategoriesExist()
    {
        // Arrange
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Category>());
        // Act
        var result = await _service.GetAllAsync();
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenIdIsNegative()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(-1)).ReturnsAsync((Category?)null);
        // Act
        var result = await _service.GetByIdAsync(-1);
        // Assert
        Assert.Null(result);
    }

}

