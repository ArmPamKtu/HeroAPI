using AutoMapper;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.ProductVersions
{
    public class ProductVersionLogicTesting 
    {
        private IRepository<ProductVersion> _productVersionRepository;
        private IRepository<Product> _productRepository;

        public ProductVersionLogicTesting(IRepository<ProductVersion> repository, IRepository<Product> productRepository)
        {
            _productVersionRepository = repository;
            _productRepository = productRepository;
        }

        public void Create(FullProductDto combined)
        {
            Product product;
            ProductVersion productVersion;
            var sameNameProductVersion = _productVersionRepository.GetMany(x => x.Name.Equals(combined.Name) && !x.SoftDelete && !Guid.Equals(x.Id, combined.ProductVersionId));

            if (sameNameProductVersion.Count() != 0)
                throw new BusinessException(ExceptionCode.DuplicateProductNames);

            combined.SplitCombinedProduct(out productVersion, out product);
            _productRepository.Create(product);
            _productVersionRepository.Create(productVersion);

        }

        public void UpdateVersion(FullProductDto combined)
        {
            var productId = _productVersionRepository.GetAll().ToList();
            var sameNameProductVersion = _productVersionRepository.GetMany(x => x.Name.Equals(combined.Name) && x.SoftDelete == false && !Guid.Equals(x.Id, combined.ProductVersionId));

            if (sameNameProductVersion.Count() != 0)
                throw new BusinessException(ExceptionCode.DuplicateProductNames);

            var oldProductVersion = _productVersionRepository.GetByID(combined.ProductVersionId);
            if (oldProductVersion != null)
            {
                var oldProduct = _productRepository.GetByID(oldProductVersion.ProductId);
                oldProduct.IsInStore = combined.IsInStore;
                oldProduct.Quantity = combined.Quantity;
                oldProduct.IsOrderable = combined.IsOrderable;
                _productRepository.Update(oldProduct);

                if (IsNewProductVersionNeeded(combined, oldProductVersion))
                {
                    oldProductVersion.SoftDelete = true;
                    ProductVersion newProductVersion = new ProductVersion();
                    newProductVersion.Description = combined.Description;
                    newProductVersion.Cost = combined.Cost;
                    newProductVersion.Created = DateTime.Now;
                    newProductVersion.Name = combined.Name;
                    newProductVersion.ProductId = oldProduct.Id;
                    newProductVersion.UrlImg = combined.UrlImg;
                    newProductVersion.SoftDelete = false;

                    _productVersionRepository.Update(oldProductVersion);
                    _productVersionRepository.Create(newProductVersion);
                }
            }
        }

        //Returns true only if name, category or cost changes. Changes to quantity, active or orderable fields do not require new version creation.
        private bool IsNewProductVersionNeeded(FullProductDto newProductVersion, ProductVersion currentProductVersion)
        {
            if (!currentProductVersion.Name.Equals(newProductVersion.Name))
                return true;
            if (currentProductVersion.Cost != newProductVersion.Cost)
                return true;
            if (currentProductVersion.Description != newProductVersion.Description)
                return true;
            if (currentProductVersion.UrlImg != newProductVersion.UrlImg)
                return true;
            return false;
        }

        public void DeleteVersion(Guid id)
        {
            var productVersion = _productVersionRepository.GetByID(id);

            if (productVersion == null)
                throw new BusinessException(ExceptionCode.ProductVersionDoesNotExist);
           
            productVersion.SoftDelete = true;
            _productVersionRepository.Update(productVersion);
            _productVersionRepository.SaveChanges();
            
        }

        public ProductVersionDto GetSpecificProductVersion(Guid id)
        {
            var chosenProductVersion = _productVersionRepository.GetByID(id);

            var mappedData = Mapper.Map<ProductVersionDto>(chosenProductVersion);

            return mappedData;
        }

        private List<ProductVersionDto> GetExistingProductVersions()
        {
            var allVersions = _productVersionRepository.GetMany(x => x.SoftDelete == false);

            var mappedVersions = Mapper.Map<List<ProductVersionDto>>(allVersions);

            return mappedVersions.ToList();
        }

        private List<ProductDto> GetProducts()
        {
            var allProducts = _productRepository.GetAll();
            var mappedVersions = Mapper.Map<List<ProductDto>>(allProducts);

            return mappedVersions.ToList();
        }

        public ICollection<FullProductDto> GetAllCombined()
        {
            var versions = GetExistingProductVersions();
            var products = GetProducts();
     
            List<FullProductDto> listOfActiveProducts = new List<FullProductDto>();

            foreach (var version in versions)
            {
                FullProductDto temp = new FullProductDto();
                var product = products.Where(x => x.Id == version.ProductId).FirstOrDefault();
                temp = version.ToCombinedProduct(product);
                listOfActiveProducts.Add(temp);
            }
            return listOfActiveProducts;
        }
 
    }
}
