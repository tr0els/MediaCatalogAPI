using System;
using Xunit;
using Moq;
using MediaCatalog.Core.Interfaces;
using MediaCatalog.Core.Entities;
using MediaCatalog.Core.Services;
using System.Collections.Generic;

namespace MediaCatalog.UnitTests
{
    public class CatalogManagerTests
    {
        private ICatalogManager catalogManager;
        private Mock<IRepository<Catalog>> fakeCatalogRepo;
        private Mock<IProductRepository<Product>> fakeProductRepo;
        private Mock<IRepository<Image>> fakeImageRepo;
        private Mock<IRepository<ImageVariant>> fakeImageVariantRepo;

        public CatalogManagerTests()
        {
            // Setup fakeRepositories
            List<Catalog> catalogs = new List<Catalog> {
                    new Catalog { Id=1, Name="Catalog 1" },
                    new Catalog { Id=2, Name="Catalog 2" },
                };

            fakeCatalogRepo = new Mock<IRepository<Catalog>>();
            fakeCatalogRepo.Setup(x => x.GetAll()).Returns(catalogs);
            fakeCatalogRepo.Setup(x => x.Add(It.IsAny<Catalog>())).Verifiable();

            fakeProductRepo = new Mock<IProductRepository<Product>>();
            fakeImageRepo = new Mock<IRepository<Image>>();
            fakeImageVariantRepo = new Mock<IRepository<ImageVariant>>();

            // Setup managers
            catalogManager = new CatalogManager(
                fakeCatalogRepo.Object,
                fakeProductRepo.Object,
                fakeImageRepo.Object,
                fakeImageVariantRepo.Object
                );
        }

        [Fact]
        public void CreateCatalog_WithNameMissing_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog();

            // Act
            Action act = () => catalogManager.CreateCatalog(catalog);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void CreateCatalog_WithNameAlreadyExists_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Name = "Catalog 1"
            };

            // Act
            Action act = () => catalogManager.CreateCatalog(catalog);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void CreateCatalog_WithValidCatalog_ShouldCallCatalogRepoOnce()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Name = "New catalog"
            };

            // Act
            catalogManager.CreateCatalog(catalog);

            // Assert
            fakeCatalogRepo.Verify(x => x.Add(It.IsAny<Catalog>()), Times.Once);
        }

        [Fact]
        public void GetAllCatalogs_ReturnsListWithCorrectNumberOfItems_ShouldCallCatalogRepoOnce()
        {
            // Act
            var catalogs = catalogManager.GetAllCatalogs();

            // Assert
            Assert.Equal(2, catalogs.Count);
            fakeCatalogRepo.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Fact]
        public void GetCatalogById_ReturnsCorrectCatalog_ShouldCallCatalogRepoOnce()
        {
            // Arrange
            var catalogToGet = new Catalog()
            {
                Id = 1,
                Name = "Catalog 1"
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalogToGet.Id)))
                .Returns(() => catalogToGet);

            // Act
            var catalog = catalogManager.GetCatalog(catalogToGet.Id);

            // Assert
            Assert.Equal(catalogToGet.Id, catalog.Id);
            fakeCatalogRepo.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public void UpdateCatalog_WithValidCatalog_ShouldCallCatalogRepoOnce()
        {
            // Arrange
            var catalogToUpdate = new Catalog()
            {
                Id = 1,
                Name = "Updated name"
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalogToUpdate.Id)))
                .Returns(() => catalogToUpdate);

            // Act
            catalogManager.EditCatalog(catalogToUpdate);

            // Assert
            fakeCatalogRepo.Verify(repo => repo.Edit(It.IsAny<Catalog>()), Times.Once());
        }

        [Fact]
        public void UpdateCatalog_WithCatalogIdLessThanOne_ShouldThrowException()
        {
            // Arrange
            var catalogToUpdate = new Catalog()
            {
                Id = 0,
                Name = "Updated name"
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalogToUpdate.Id)))
                .Returns(() => catalogToUpdate);

            // Act
            Action act = () => catalogManager.EditCatalog(catalogToUpdate);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("No catalog was given", ex.Message);
        }

        [Fact]
        public void UpdateCatalog_WithCatalogIdNotExisting_ShouldThrowException()
        {
            // Arrange
            var catalogToUpdate = new Catalog()
            {
                Id = 10,
                Name = "Updated name"
            };

            // Act
            Action act = () => catalogManager.EditCatalog(catalogToUpdate);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Catalog not found", ex.Message);
        }

        [Fact]
        public void UpdateCatalog_WithNameMissing_ShouldThrowException()
        {
            // Arrange
            var existingCatalog = new Catalog()
            {
                Id = 1,
                Name = "Name"
            };

            // Making sure a catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == existingCatalog.Id)))
                .Returns(() => existingCatalog);

            var catalogToUpdate = new Catalog()
            {
                Id = 1
            };

            // Act
            Action act = () => catalogManager.EditCatalog(catalogToUpdate);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("A name must be given", ex.Message);
        }

        [Fact]
        public void UpdateCatalog_WithNameAlreadyExists_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1,
                Name = "Catalog 1"
            };

            // Act
            Action act = () => catalogManager.CreateCatalog(catalog);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Catalog name already exists", ex.Message);
        }

        [Fact]
        public void DeleteCatalog_WhenCatalogExists_ShouldCallCatalogRepoOnce()
        {
            // Arrange
            var existingCatalog = new Catalog()
            {
                Id = 1,
                Name = "Name"
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == existingCatalog.Id)))
                .Returns(() => existingCatalog);

            // Act
            catalogManager.DeleteCatalog(1);

            // Assert
            fakeCatalogRepo.Verify(repo => repo.Remove(1), Times.Once);
        }

        [Fact]
        public void DeleteCatalog_WhenIdDoesNotExist_RemoveThrowsExceptionAndShouldCallCatalogRepoNever()
        {
            // Arrange

            // Act + Assert
            Action act = () => catalogManager.DeleteCatalog(3);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Catalog not found", ex.Message);
            fakeCatalogRepo.Verify(repo => repo.Remove(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void AddImageVariantToCatalog_WithValidCatalog_ShouldCallCatalogRepoOnce()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalog.Id)))
                .Returns(() => catalog);

            var image = new Image()
            {
                Id = 1,
            };

            // Making sure the image exists before test
            fakeImageRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == image.Id)))
                .Returns(() => image);

            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                ImageId = 1,
                CatalogId = 1,
                Name = "ImageVariant to be added to catalog",
                Width = 100,
                Height = 100
            };

            // Act
            catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            fakeImageVariantRepo.Verify(x => x.Add(It.IsAny<ImageVariant>()), Times.Once);
        }

        [Fact]
        public void AddImageVariantToCatalog_WhenCatalogIdIsLessThanOne_ThrowsException()
        {
            // Arrange
            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                ImageId = 2,
                CatalogId = 0
            };

            // Act
            Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("No catalog was given", ex.Message);
        }

        [Fact]
        public void AddImageVariantToCatalog_WhenCatalogIdDoesNotExist_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalog.Id)))
                .Returns(() => catalog);

            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                CatalogId = 2,
            };

            // Act
            Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void AddImageVariantToCatalog_WhenImageIdIsLessThanOne_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalog.Id)))
                .Returns(() => catalog);

            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                ImageId = 0,
                CatalogId = 1
            };

            // Act
            Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("No image was given", ex.Message);
        }

        [Fact]
        public void AddImageVariantToCatalog_WhenImageIdDoesNotExist_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalog.Id)))
                .Returns(() => catalog);

            var image = new Image()
            {
                Id = 1,
            };

            // Making sure the image exists before test
            fakeImageRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == image.Id)))
                .Returns(() => image);

            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                ImageId = 2,
                CatalogId = 1
            };

            // Act
            Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Image not found", ex.Message);
        }

        [Fact]
        public void AddImageVariantToCatalog_WithNameMissing_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
        {
            Id = 1
            };

        // Making sure the catalog exists before test
        fakeCatalogRepo.Setup(repo => repo
            .Get(It.Is<int>(id => id == catalog.Id)))
                .Returns(() => catalog);

        var image = new Image()
        {
            Id = 1,
        };

        // Making sure the image exists before test
        fakeImageRepo.Setup(repo => repo
            .Get(It.Is<int>(id => id == image.Id)))
                .Returns(() => image);

        var imageVariantToAdd = new ImageVariant()
        {
            Id = 1,
            ImageId = 1,
            CatalogId = 1
        };

        // Act
        Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

        // Assert
        Exception ex = Assert.Throws<ArgumentException>(act);
        Assert.Equal("A name must be given", ex.Message);
        }

        [Fact]
        public void AddImageVariantToCatalog_WithDimensionTooLow_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalog.Id)))
                    .Returns(() => catalog);

            var image = new Image()
            {
                Id = 1,
            };

            // Making sure the image exists before test
            fakeImageRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == image.Id)))
                    .Returns(() => image);

            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                Name = "ImageVariant for testing",
                ImageId = 1,
                CatalogId = 1,
                Width = 0,
                Height = 0
            };

            // Act
            Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Dimension size is below minimum", ex.Message);
        }

        [Fact]
        public void AddImageVariantToCatalog_WithDimensionTooHigh_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalog.Id)))
                    .Returns(() => catalog);

            var image = new Image()
            {
                Id = 1,
            };

            // Making sure the image exists before test
            fakeImageRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == image.Id)))
                    .Returns(() => image);

            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                Name = "ImageVariant for testing",
                ImageId = 1,
                CatalogId = 1,
                Width = 48000001,
                Height = 1
            };

            // Act
            Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Dimension size is above maximum", ex.Message);
        }


        [Fact]
        public void AddImageVariantToCatalog_WithIdenticalImageVariantAlreadyExists_ThrowsException()
        {
            // Arrange
            var catalog = new Catalog()
            {
                Id = 1
            };

            // Making sure the catalog exists before test
            fakeCatalogRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == catalog.Id)))
                .Returns(() => catalog);

            var image = new Image()
            {
                Id = 1,
            };

            // Making sure the image exists before test
            fakeImageRepo.Setup(repo => repo
                .Get(It.Is<int>(id => id == image.Id)))
                .Returns(() => image);

            var imageVariantToAdd = new ImageVariant()
            {
                Id = 1,
                ImageId = 1,
                CatalogId = 1,
                Name = "ImageVariant to be added to catalog",
                Width = 100,
                Height = 100
            };

            List<ImageVariant> existingImageVariants = new List<ImageVariant> {
                imageVariantToAdd
            };

            // Making sure the imageVariant exists before test
            fakeImageVariantRepo.Setup(repo => repo
                .GetAll())
                .Returns(existingImageVariants);

            // Act
            Action act = () => catalogManager.AddImageVariantToCatalog(imageVariantToAdd);

            // Assert
            Exception ex = Assert.Throws<ArgumentException>(act);
            Assert.Equal("This image with the given width and height already exists in this catalog", ex.Message);
        }
    }
}