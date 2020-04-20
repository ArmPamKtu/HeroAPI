using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Exceptions;
using Logic.Generic;
using Logic.ProductVersions;

namespace Logic.ProductVersions
{
    public class ProductVersionLogic : IProductVersionLogic
    {
        private IRepository<ProductVersion> _productVersionRepository;
        private IRepository<Product> _productRepository;

        public ProductVersionLogic(IRepository<ProductVersion> repository, IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
            _productVersionRepository = repository;
        }

        public void Create(FullProductDto combined)
        {

            Product product;
            ProductVersion productVersion;
            
            if (_productVersionRepository.Count() > 0)
            {
                
                var sameNameProductVersion = _productVersionRepository.GetMany(x => x.Name.Equals(combined.Name) && !x.SoftDelete && !Guid.Equals(x.Id, combined.ProductVersionId));

                if (sameNameProductVersion.Count() != 0)
                    throw new BusinessException(ExceptionCode.DuplicateProductNames);
            }

            combined.SplitCombinedProduct(out productVersion, out product);

                if(product != null && productVersion != null)
                    using (var scope = _productVersionRepository.DatabaseFacade.BeginTransaction())
                    {
                        try
                        {
                            _productRepository.Create(product);
                            _productRepository.SaveChanges();
                            _productVersionRepository.Create(productVersion);
                            _productVersionRepository.SaveChanges();
                            scope.Commit();

                        }
                        catch (Exception e)
                        {
                            throw new BusinessException(ExceptionCode.Unhandled);
                        }
                    }
                else
                    throw new BusinessException(ExceptionCode.Unhandled);
        }

        public bool UpdateVersion(FullProductDto combined)
        {
            var success = false;

            using (var scope = _productVersionRepository.DatabaseFacade.BeginTransaction())
            {

                var sameNameProductVersion = _productVersionRepository.GetMany(x => x.Name.Equals(combined.Name) && x.SoftDelete == false && x.Id != combined.ProductVersionId);

                if (sameNameProductVersion.Count() != 0)
                    throw new BusinessException(ExceptionCode.DuplicateProductNames);

                var oldProductVersion = _productVersionRepository.GetByID(combined.ProductVersionId);

                if (oldProductVersion != null)
                {
                    var oldProduct = _productRepository.Get(x => x.Id == oldProductVersion.ProductId);
                    oldProduct.IsInStore = combined.IsInStore;
                    oldProduct.Quantity = combined.Quantity;
                    oldProduct.IsOrderable = combined.IsOrderable;
                    _productRepository.Update(oldProduct);
                    _productRepository.SaveChanges();

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
                        _productVersionRepository.SaveChanges();
                    }
                    scope.Commit();
                    success = true;
                }

                return success;
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

        public bool DeleteVersion(Guid id)
        {
            var success = false;


            using (var scope = _productVersionRepository.DatabaseFacade.BeginTransaction())
            {
                var productVersion = _productVersionRepository.GetByID(id);
                if (productVersion != null)
                {
                    productVersion.SoftDelete = true;
                    _productVersionRepository.Update(productVersion);
                    _productVersionRepository.SaveChanges();

                    scope.Commit();
                    success = true;
                }
            }
            return success;
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



                if (product != null)
                    temp = version.ToCombinedProduct(product);


                listOfActiveProducts.Add(temp);
            }
            return listOfActiveProducts;
        }


        public ProductDto GetProductById(Guid id)
        {
            var chosenProduct = _productRepository.GetByID(id);
            var mappedData = Mapper.Map<ProductDto>(chosenProduct);

            return mappedData;
        }
    }
}