//using AutoMapper;
//using Moq;
//using SuperMarketAPI;
//using SuperMarketAPI.DTOs;
//using SuperMarketAPI.Models;
//using SuperMarketAPI.Repositories.Interfaces;
//using SuperMarketAPI.Services;

//public class ProductServiceTests
//{
//    private readonly Mock<IProductRepository> _repoMock;
//    private readonly IMapper _mapper;
//    private readonly ProductService _service;

//    public ProductServiceTests()
//    {
//        _repoMock = new Mock<IProductRepository>();

//        var config = new MapperConfiguration(cfg =>
//        {
//            cfg.AddProfile<MappingProfile>();
//        });
//        _mapper = config.CreateMapper();

//        _service = new ProductService(_repoMock.Object, _mapper);
//    }

//    [Fact]
//    public async Task GetAllAsync_ReturnsMappedDtos()
//    {
//        // Arrange
//        var fakeProducts = new List<Product>
//        {
//            new Product { 
//                Id = 1, 
//                Name = "Apple", 
//                Price = 2.50m, 
//                Stock = 100, 
//                CategoryId = 1,
//                Category = new Category { Id = 1, Name = "Fruits" }
//            },
//            new Product { 
//                Id = 2, 
//                Name = "Banana", 
//                Price = 1.75m, 
//                Stock = 150, 
//                CategoryId = 1,
//                Category = new Category { Id = 1, Name = "Fruits" }
//            }
//        };
//        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeProducts);

//        // Act
//        var result = await _service.GetAllAsync();

//        // Assert
//        Assert.NotNull(result);
//        var list = Assert.IsAssignableFrom<List<ProductReadDto>>(result);
//        Assert.Collection(list,
//            item => {
//                Assert.Equal("Apple", item.Name);
//                Assert.Equal("Fruits", item.CategoryName);
//            },
//            item => {
//                Assert.Equal("Banana", item.Name);
//                Assert.Equal("Fruits", item.CategoryName);
//            });
//    }

//    [Fact]
//    public async Task GetAllAsync_ReturnsEmptyList_WhenNoProductsExist()
//    {
//        // Arrange
//        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product>());

//        // Act
//        var result = await _service.GetAllAsync();

//        // Assert
//        Assert.NotNull(result);
//        Assert.Empty(result);
//    }

//    [Fact]
//    public async Task GetByIdAsync_ReturnsMappedDto_WhenProductExists()
//    {
//        // Arrange
//        var product = new Product { 
//            Id = 1, 
//            Name = "Milk", 
//            Price = 3.99m, 
//            Stock = 50, 
//            CategoryId = 2,
//            Category = new Category { Id = 2, Name = "Dairy" }
//        };
//        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

//        // Act
//        var result = await _service.GetByIdAsync(1);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("Milk", result.Name);
//        Assert.Equal(3.99m, result.Price);
//        Assert.Equal(50, result.Stock);
//        Assert.Equal("Dairy", result.CategoryName);
//    }

//    [Fact]
//    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
//    {
//        // Arrange
//        _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Product?)null);

//        // Act
//        var result = await _service.GetByIdAsync(999);

//        // Assert
//        Assert.Null(result);
//    }

//    [Fact]
//    public async Task GetByIdAsync_ReturnsNull_WhenIdIsNegative()
//    {
//        // Arrange
//        _repoMock.Setup(r => r.GetByIdAsync(-1)).ReturnsAsync((Product?)null);

//        // Act
//        var result = await _service.GetByIdAsync(-1);

//        // Assert
//        Assert.Null(result);
//    }

//    [Fact]
//    public async Task CreateAsync_AddsProductAndReturnsMappedDto()
//    {
//        // Arrange
//        var dto = new ProductCreateDto { Name = "Bread", Price = 2.99m, Stock = 75, CategoryId = 3 };
//        var product = new Product { 
//            Id = 1, 
//            Name = "Bread", 
//            Price = 2.99m, 
//            Stock = 75, 
//            CategoryId = 3,
//            Category = new Category { Id = 3, Name = "Bakery" }
//        };
//        _repoMock.Setup(r => r.AddAsync(It.IsAny<Product>())).Callback<Product>(p => p.Id = 1);

//        // Act
//        var result = await _service.CreateAsync(dto);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("Bread", result.Name);
//        Assert.Equal(2.99m, result.Price);
//        Assert.Equal(75, result.Stock);
//        _repoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
//    }

//    [Fact]
//    public async Task CreateAsync_ThrowsArgumentNullException_WhenDtoIsNull()
//    {
//        // Arrange
//        ProductCreateDto? dto = null;

//        // Act & Assert
//        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.CreateAsync(dto!));
//        _repoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
//    }

//    [Theory]
//    [InlineData("")]
//    [InlineData(" ")]
//    [InlineData(null)]
//    public async Task CreateAsync_ThrowsArgumentException_WhenNameIsNullOrWhiteSpace(string name)
//    {
//        // Arrange
//        var dto = new ProductCreateDto { Name = name, Price = 1.99m, Stock = 10, CategoryId = 1 };

//        // Act & Assert
//        await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
//        _repoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
//    }

//    [Fact]
//    public async Task UpdateAsync_UpdatesProductAndReturnsTrue_WhenProductExists()
//    {
//        // Arrange
//        var existingProduct = new Product { 
//            Id = 1, 
//            Name = "Old Name", 
//            Price = 5.99m, 
//            Stock = 25, 
//            CategoryId = 1,
//            Category = new Category { Id = 1, Name = "Original Category" }
//        };
//        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingProduct);
//        var updateDto = new ProductUpdateDto { Name = "New Name", CategoryId = 2 };

//        // Act
//        var result = await _service.UpdateAsync(1, updateDto);

//        // Assert
//        Assert.True(result);
//        Assert.Equal("New Name", existingProduct.Name);
//        Assert.Equal(2, existingProduct.CategoryId);
//        _repoMock.Verify(r => r.UpdateAsync(existingProduct), Times.Once);
//    }

//    [Fact]
//    public async Task UpdateAsync_ReturnsFalse_WhenProductDoesNotExist()
//    {
//        // Arrange
//        _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Product?)null);
//        var updateDto = new ProductUpdateDto { Name = "Non-Existent", CategoryId = 1 };

//        // Act
//        var result = await _service.UpdateAsync(999, updateDto);

//        // Assert
//        Assert.False(result);
//        _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
//    }

//    [Fact]
//    public async Task UpdateAsync_ReturnsFalse_WhenIdIsNegative()
//    {
//        // Arrange
//        _repoMock.Setup(r => r.GetByIdAsync(-1)).ReturnsAsync((Product?)null);
//        var updateDto = new ProductUpdateDto { Name = "Negative ID", CategoryId = 1 };

//        // Act
//        var result = await _service.UpdateAsync(-1, updateDto);

//        // Assert
//        Assert.False(result);
//        _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
//    }

//    [Fact]
//    public async Task DeleteAsync_DeletesProductAndReturnsTrue_WhenProductExists()
//    {
//        // Arrange
//        var existingProduct = new Product { 
//            Id = 1, 
//            Name = "To Delete", 
//            Price = 1.99m, 
//            Stock = 10, 
//            CategoryId = 1,
//            Category = new Category { Id = 1, Name = "Test Category" }
//        };
//        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingProduct);

//        // Act
//        var result = await _service.DeleteAsync(1);

//        // Assert
//        Assert.True(result);
//        _repoMock.Verify(r => r.DeleteAsync(existingProduct), Times.Once);
//    }

//    [Fact]
//    public async Task DeleteAsync_ReturnsFalse_WhenProductDoesNotExist()
//    {
//        // Arrange
//        _repoMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Product?)null);

//        // Act
//        var result = await _service.DeleteAsync(999);

//        // Assert
//        Assert.False(result);
//        _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Product>()), Times.Never);
//    }

//    [Fact]
//    public async Task DeleteAsync_ReturnsFalse_WhenIdIsNegative()
//    {
//        // Arrange
//        _repoMock.Setup(r => r.GetByIdAsync(-1)).ReturnsAsync((Product?)null);

//        // Act
//        var result = await _service.DeleteAsync(-1);

//        // Assert
//        Assert.False(result);
//        _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Product>()), Times.Never);
//    }

//    [Fact]
//    public async Task CreateAsync_WithValidData_MapsCorrectly()
//    {
//        // Arrange
//        var dto = new ProductCreateDto 
//        { 
//            Name = "Test Product", 
//            Price = 15.50m, 
//            Stock = 200, 
//            CategoryId = 5 
//        };
//        _repoMock.Setup(r => r.AddAsync(It.IsAny<Product>())).Callback<Product>(p => p.Id = 10);

//        // Act
//        var result = await _service.CreateAsync(dto);

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal(10, result.Id);
//        Assert.Equal("Test Product", result.Name);
//        Assert.Equal(15.50m, result.Price);
//        Assert.Equal(200, result.Stock);
//    }

//    [Fact]
//    public async Task UpdateAsync_WithNullName_UpdatesOnlyCategoryId()
//    {
//        // Arrange
//        var existingProduct = new Product { 
//            Id = 1, 
//            Name = "Original Name", 
//            Price = 10.00m, 
//            Stock = 50, 
//            CategoryId = 1,
//            Category = new Category { Id = 1, Name = "Original Category" }
//        };
//        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingProduct);
//        var updateDto = new ProductUpdateDto { Name = null, CategoryId = 3 };

//        // Act
//        var result = await _service.UpdateAsync(1, updateDto);

//        // Assert
//        Assert.True(result);
//        Assert.Equal("Original Name", existingProduct.Name); // Name should remain unchanged
//        Assert.Equal(3, existingProduct.CategoryId); // CategoryId should be updated
//        _repoMock.Verify(r => r.UpdateAsync(existingProduct), Times.Once);
//    }
//}
