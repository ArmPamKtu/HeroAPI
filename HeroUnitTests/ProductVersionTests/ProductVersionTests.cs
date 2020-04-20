using AutoMapper;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Exceptions;
using Logic.ProductVersions;
using Moq;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace HeroUnitTests.ProductVersionTests
{
    [Collection("Tests collection")]
    public class ProductVersionTests
    {


        private readonly Mock<IRepository<ProductVersion>> _productVersionRepository;
        private readonly Mock<IRepository<Product>> _productRepository;
        private readonly ProductVersionLogicTesting _productVersionLogic;

        public ProductVersionTests()
        {
            _productVersionRepository = new Mock<IRepository<ProductVersion>>();

            var productVersions = DataForTesting.GetProductVersions();

            // Return product versions by predicate
            _productVersionRepository.Setup(mr => mr.GetMany(It.IsAny<Expression<Func<ProductVersion, bool>>>()))
                .Returns((Expression<Func<ProductVersion, bool>> predicate) => productVersions.Where(predicate));

            // return product version by Id
            /*  _productVersionRepository.Setup(mr => mr.GetByID(It.IsAny<Guid>()))
                  .Returns((Guid i) => productVersions.Where(x => Guid.Equals(x.Id, i)).SingleOrDefault());
                  */
            _productVersionRepository.Setup(mr => mr.GetByID(It.IsAny<Guid>()))
           .Returns((Guid i) => productVersions.SingleOrDefault(x => x.Id == i));

            //Prepare product repository
            _productRepository = new Mock<IRepository<Product>>();

            var products = DataForTesting.GetProducts();

            // return product by Id
            /*_productRepository.Setup(mr => mr.GetByID(It.IsAny<Guid>()))
                .Returns((Guid i) => products.Where(x => Guid.Equals(x.Id, i)).SingleOrDefault());*/
            _productRepository.Setup(mr => mr.GetByID(It.IsAny<Guid>()))
            .Returns((Guid i) => products.SingleOrDefault(x => x.Id == i));

            // return product by Id
            _productRepository.Setup(mr => mr.GetAll()).Returns(products);

            //mock logic
            _productVersionLogic = new ProductVersionLogicTesting(_productVersionRepository.Object, _productRepository.Object);
        }


        [Fact]
        public void Create_WhenProductNameAlreadyExists_ThrowsDuplicateProductNameBusinessException()
        {
            //Assign
            var productVersion = new FullProductDto
            {
                Description = "Product 5",
                Name = "Product 5",
                Cost = 2,
                ProductId = Guid.NewGuid(),
                SoftDelete = false,
                UrlImg = "",
                ProductVersionId = Guid.NewGuid(),
                IsInStore = true,
                IsOrderable = false,
                Quantity = 0,
                ProductCreated = DateTime.Now,
                ProductVersionCreated = DateTime.Now,
            };

            //Act
            var ex = (BusinessException)Record.Exception(() => _productVersionLogic.Create(productVersion));

            //Assert
            Assert.Equal(ExceptionCode.DuplicateProductNames, ex.Code);
        }


        [Fact]
        public void Create_WhenValidProductVersionProvided_CallsCreateProductAndProductVersion()
        {
            //Assign
            var productVersion = new FullProductDto
            {
                Description = "Product 6",
                Name = "Product 6",
                Cost = 2,
                ProductId = Guid.NewGuid(),
                SoftDelete = false,
                UrlImg = "",
                ProductVersionId = Guid.NewGuid(),   //guid  specifinis
                IsInStore = true,
                IsOrderable = false,
                Quantity = 10,
                ProductCreated = DateTime.Now,
                ProductVersionCreated = DateTime.Now,
            };

            //Act
            _productVersionLogic.Create(productVersion);


            //Assert

            _productRepository.Verify(x => x.Create(It.IsAny<Product>()), Times.Once);
            _productVersionRepository.Verify(x => x.Create(It.IsAny<ProductVersion>()), Times.Once);
        }

        [Fact]
        public void UpdateVersion_WhenProductNameAlreadyExists_ThrowsDuplicateProductNameBusinessException()
        {
            //Assign
            var productVersion = new FullProductDto
            {
                Description = "Product 5",
                Name = "Product 5",
                Cost = 2,
                ProductId = Guid.NewGuid(),
                SoftDelete = false,
                UrlImg = "",
                ProductVersionId = Guid.NewGuid(),
                IsInStore = true,
                IsOrderable = false,
                Quantity = 0,
                ProductVersionCreated = DateTime.Now,
                ProductCreated = DateTime.Now
            };

            //Act
            var ex = (BusinessException)Record.Exception(() => _productVersionLogic.UpdateVersion(productVersion));

            //Assert
            Assert.Equal(ExceptionCode.DuplicateProductNames, ex.Code);
        }

        [Theory]
        [ClassData(typeof(ProductVersionTestingData))]
        public void UpdateVersion_WhenProductChangesAndNewVersionIsNotNeeded_CallsUpdateProductOnce(FullProductDto productVersion)
        {
            //Assign

            //Act
            _productVersionLogic.UpdateVersion(productVersion);

            //Assert
            _productRepository.Verify(x => x.Update(It.IsAny<Product>()), Times.Once);
        }

        [Theory]
        [ClassData(typeof(ProductVersionTestingData2))]
        public void UpdateVersion_WhenProductChangesAndNewVersionIsNeeded_CallsUpdateAndCreateProductVersion(FullProductDto productVersion)
        {
            //Assign

            //Act
            _productVersionLogic.UpdateVersion(productVersion);

            //Assert
            _productVersionRepository.Verify(x => x.Update(It.IsAny<ProductVersion>()), Times.Once);
            _productVersionRepository.Verify(x => x.Create(It.IsAny<ProductVersion>()), Times.Once);
        }

        [Theory]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99653")]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99654")]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99655")]
        public void DeleteVersion_ValidIdProvided_CallsProductVersionUpdateMethodOnce(string id)
        {
            //Assign
            var guid = Guid.Parse(id);

            //Act
            _productVersionLogic.DeleteVersion(guid);

            //Assert
            _productVersionRepository.Verify(x => x.Update(It.IsAny<ProductVersion>()), Times.Once);
        }


        [Theory]
        [InlineData("aaaaaaaa-aaaa-aaaa-aaaa-0f223da99655")]
        [InlineData("aaaaaaaa-aaaa-bbbb-aaaa-0f223da99655")]
        [InlineData("aaaaaaaa-aaaa-aaaa-cccc-0f223da99655")]
        public void DeleteVersion_InvalidIdProvided_ThrowsProductVersionDoesNotExistBusinessException(string id)
        {
            //Assign
            var guid = Guid.Parse(id);
            //Act
            var ex = (BusinessException)Record.Exception(() => _productVersionLogic.DeleteVersion(guid));

            //Assert
            Assert.Equal(ExceptionCode.ProductVersionDoesNotExist, ex.Code);
        }


        [Theory]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99653")]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99654")]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99655")]
        [InlineData("aaaaaaaa-aaaa-aaaa-aaaa-0f223da99655")]
        [InlineData("aaaaaaaa-aaaa-bbbb-aaaa-0f223da99655")]
        public void GetSpecificProductVersion_WhenProductVersionIdProvided_ReturnsProductVersion(string id)
        {
            //Assign
            var guid = Guid.Parse(id);
            var productVersion = _productVersionRepository.Object.GetByID(guid);
            var expectedResult = Mapper.Map<ProductVersionDto>(productVersion);

            //Act
            var actualResult = _productVersionLogic.GetSpecificProductVersion(guid);

            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99653")]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99654")]
        [InlineData("e4e4fccd-04bb-4817-a01a-0f223da99655")]
        [InlineData("aaaaaaaa-aaaa-aaaa-aaaa-0f223da99655")]
        [InlineData("aaaaaaaa-aaaa-bbbb-aaaa-0f223da99655")]
        public void GetAllCombined_ReturnsProductVersion(string id)
        {
            //Assign
            var guid = Guid.Parse(id);
            var productVersion = _productVersionRepository.Object.GetByID(guid);
            var expectedResult = Mapper.Map<ProductVersionDto>(productVersion);

            //Act
            var actualResult = _productVersionLogic.GetSpecificProductVersion(guid);

            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult);
        }



        [Fact]
        public void GetAllCombined_ReturnsCombinedProducts()
        {
            //Assign
            var versions = _productVersionRepository.Object.GetMany(x => x.SoftDelete == false);
            var callCount = versions.Count();

            //Act
            var actualResult = _productVersionLogic.GetAllCombined();

            //Assert
            Assert.Equal(callCount, actualResult.Count());
        }

    }
}
