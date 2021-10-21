using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Logic.Models;
using Shop.Repository;
using Shop.Web.Dtos;

namespace Shop.Web.Services
{
    public interface IProductService
    {
        PagedResult<ProductDto> GetProducts(ProductQuery query);
        Task<ProductDto> GetProduct(int id);
    }
    
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly DescRepository _descRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly SubCategoryRepository _subCategoryRepository;
        private readonly PhotoRepository _photoRepository;
        private readonly QuantityRepository _quantityRepository;
        private readonly PriceRepository _priceRepository;


        public ProductService(ProductRepository productRepository, DescRepository descRepository, CategoryRepository categoryRepository, SubCategoryRepository subCategoryRepository, PhotoRepository photoRepository,
            QuantityRepository quantityRepository, PriceRepository priceRepository)
        {
            _productRepository = productRepository;
            _descRepository = descRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _photoRepository = photoRepository;
            _quantityRepository = quantityRepository;
            _priceRepository = priceRepository;
        }

        public PagedResult<ProductDto> GetProducts(ProductQuery query)
        {
            var baseQuery = _productRepository.Where(x => x.NazwaProduktu.Contains(query.SearchPhrase ?? ""));

            if (!string.IsNullOrEmpty(query.Category))
            {
                var categorySearch =
                    _categoryRepository.Find(x => x.NazwaKategorii.ToLower() == query.Category.ToLower()).Id;
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    var subCategorySearch =
                        _subCategoryRepository.Find(x => x.NazwaPodkategorii.ToLower() == query.SubCategory.ToLower()).Id;

                    baseQuery = baseQuery.Where(x =>
                        x.KategorieId == categorySearch & x.PodkategorieId == subCategorySearch);
                }
                else
                {
                    baseQuery = baseQuery.Where(x => x.KategorieId == categorySearch);
                }
            }
            
            int totalItems = baseQuery.Count();

            var productPage = baseQuery.Skip(query.PageSize * (query.PageNumber - 1)).Take(query.PageSize).ToList();

            List<ProductDto> dtoList = new();

            productPage.ForEach(x =>
            {
                var category = _categoryRepository.Find(c => c.Id == x.KategorieId);
                var subCategory = _subCategoryRepository.Find(sc => sc.Id == x.PodkategorieId);
                var desc = _descRepository.Find(d => d.ProduktId == x.Id).Opis;
                var price = _priceRepository.Find(p => p.ProduktId == x.Id);
                var quantity = _quantityRepository.Find(q => q.ProduktId == x.Id).Ilosc1;
                var photo = _photoRepository.Find(pd => pd.ProduktId == x.Id).PathDoZdj;

                var newProduct = new ProductDto
                {
                    ProductId = x.Id,
                    ProductName = x.NazwaProduktu,
                    ProductDesc = desc,
                    ProductCode = x.KodProduktu,
                    Category = category.NazwaKategorii,
                    Subcategory = subCategory.NazwaPodkategorii,
                    BruttoPrice = price.CenaBrutto,
                    NettoPrice = price.CenaNetto,
                    Quantity = quantity,
                    ImgPath = photo
                };
                dtoList.Add(newProduct);
            });

            return new PagedResult<ProductDto>(dtoList, totalItems, query.PageSize, query.PageNumber);
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var singleProduct = await _productRepository.FindAsync(id);

            if (singleProduct is null) throw new NotImplementedException();

            var category = _categoryRepository.Find(c => c.Id == singleProduct.KategorieId);
            var subCategory = _subCategoryRepository.Find(sc => sc.Id == singleProduct.PodkategorieId);
            var desc = _descRepository.Find(d => d.ProduktId == singleProduct.Id).Opis;
            var price = _priceRepository.Find(p => p.ProduktId == singleProduct.Id);
            var quantity = _quantityRepository.Find(q => q.ProduktId == singleProduct.Id).Ilosc1;
            var photo = _photoRepository.Find(pd => pd.ProduktId == singleProduct.Id).PathDoZdj;

            var result = new ProductDto
            {
                ProductId = singleProduct.Id,
                ProductName = singleProduct.NazwaProduktu,
                ProductDesc = desc,
                ProductCode = singleProduct.KodProduktu,
                Category = category.NazwaKategorii,
                Subcategory = subCategory.NazwaPodkategorii,
                BruttoPrice = price.CenaBrutto,
                NettoPrice = price.CenaNetto,
                Quantity = quantity,
                ImgPath = photo
            };

            return result;
        }

        /*
         var category = _categoryRepository.Find(c => c.Id == x.KategorieId);
                var subCategory = _subCategoryRepository.Find(sc => sc.Id == x.PodkategorieId);
                var desc = _descRepository.Find(d => d.ProduktId == x.Id).Opis;
                var price = _priceRepository.Find(p => p.ProduktId == x.Id);
                var quantity = _quantityRepository.Find(q => q.ProduktId == x.Id).Ilosc1;
                var photo = _photoRepository.Find(pd => pd.ProduktId == x.Id).PathDoZdj;

                var newProduct = new ProductDto
                {
                    ProductId = x.Id,
                    ProductName = x.NazwaProduktu,
                    ProductDesc = desc,
                    ProductCode = x.KodProduktu,
                    Category = category.NazwaKategorii,
                    Subcategory = subCategory.NazwaPodkategorii,
                    BruttoPrice = price.CenaBrutto,
                    NettoPrice = price.CenaNetto,
                    Quantity = quantity,
                    ImgPath = photo
                };
                productDtos.Add(newProduct);
         */
    }
}
