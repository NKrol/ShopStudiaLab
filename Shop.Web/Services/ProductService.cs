using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Entities.Model;
using Shop.Web.Entities.Repository;
using Shop.Web.Dtos;

namespace Shop.Web.Services
{
    public interface IProductService
    {
        PagedResult<ProductDto> GetProducts(ProductQuery query);
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> AddProduct(AddProductDto dto);
        List<ProductToCart> GetProductToCart(string numbers);
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
        private readonly Xkom_ProjektContext _dbContext;


        public ProductService(ProductRepository productRepository, DescRepository descRepository, CategoryRepository categoryRepository, SubCategoryRepository subCategoryRepository, PhotoRepository photoRepository,
            QuantityRepository quantityRepository, PriceRepository priceRepository, Xkom_ProjektContext dbContext)
        {
            _productRepository = productRepository;
            _descRepository = descRepository;
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
            _photoRepository = photoRepository;
            _quantityRepository = quantityRepository;
            _priceRepository = priceRepository;
            _dbContext = dbContext;
        }

        public PagedResult<ProductDto> GetProducts(ProductQuery query)
        {

            var cos = "asd";
            //var baseQuery = _productRepository
            //    .Where(x => x.NazwaProduktu.Contains(query.SearchPhrase ?? ""));

            //if (!string.IsNullOrEmpty(query.Category))
            //{
            //    var categorySearch =
            //        _categoryRepository.Find(x => x.NazwaKategorii.ToLower() == query.Category.ToLower()).Id;
            //    if (!string.IsNullOrEmpty(query.SubCategory))
            //    {
            //        var subCategorySearch =
            //            _subCategoryRepository.Find(x => x.NazwaPodkategorii.ToLower() == query.SubCategory.ToLower()).Id;

            //        baseQuery = baseQuery.Where(x =>
            //            x.KategorieId == categorySearch & x.PodkategorieId == subCategorySearch);
            //    }
            //    else
            //    {
            //        baseQuery = baseQuery.Where(x => x.KategorieId == categorySearch);
            //    }
            //}

            //int totalItems = baseQuery.Count();



            var baseQuery = _dbContext.Produkts.Include(p => p.Cena)
                .Include(c => c.Kategorie)
                .Include(sc => sc.Podkategorie)
                .Include(d => d.ProduktOpi)
                .Include(zdj => zdj.ZdjProduktu)
                .Include(q => q.Ilosc).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
            {
                baseQuery = baseQuery.Where(x => x.NazwaProduktu.Contains(query.SearchPhrase));
            }

            if (!string.IsNullOrEmpty(query.Category))
            {
                if (!string.IsNullOrEmpty(query.SubCategory))
                {
                    baseQuery = baseQuery.Where(x =>
                        x.Kategorie.NazwaKategorii == query.Category & x.Podkategorie.NazwaPodkategorii == query.SubCategory);
                }
                else
                {
                    baseQuery = baseQuery.Where(x => 
                        x.Kategorie.NazwaKategorii == query.Category);
                }
            }

            if (query.SortPrice)
            {
                baseQuery = query.Asc
                    ? baseQuery.OrderBy(x => 
                        x.Cena.CenaBrutto)
                    : baseQuery.OrderByDescending(x =>
                        x.Cena.CenaBrutto);
            }
            else
            {
                baseQuery = query.Asc
                    ? baseQuery.OrderBy(x => 
                        x.NazwaProduktu)
                    : baseQuery.OrderByDescending(x => 
                        x.NazwaProduktu);
            }

            var totalItems = baseQuery.Count();

            var productPage = baseQuery.Skip(query.PageSize * (query.PageNumber - 1)).Take(query.PageSize);

            List<ProductDto> dtoList = productPage.Select(x  => new ProductDto
                {
                    ProductId = x.Id,
                    ProductName = x.NazwaProduktu ?? null,
                    ProductDesc = x.ProduktOpi.Opis ?? null,
                    ProductCode = x.KodProduktu,
                    Category = x.Kategorie.NazwaKategorii ?? null,
                    Subcategory = x.Podkategorie.NazwaPodkategorii ?? null,
                    BruttoPrice = x.Cena.CenaBrutto ,
                    NettoPrice = x.Cena.CenaNetto,
                    Quantity = x.Ilosc.Ilosc1,
                    ImgPath = x.ZdjProduktu.PathDoZdj ?? null
            }).ToList();
            
            return new PagedResult<ProductDto>(dtoList, totalItems, query.PageSize, query.PageNumber);
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            var singleProduct = await _productRepository.FindAsync(id);

            if (singleProduct is null) throw new NotImplementedException();

            var category = _categoryRepository.Find(c => c.Id == singleProduct.KategorieId);
            var subCategory = _subCategoryRepository.Find(sc => sc.Id == singleProduct.PodkategorieId);
            var desc = _descRepository?.Find(d => d.ProduktId == singleProduct.Id).Opis;
            var price = _priceRepository.Find(p => p.ProduktId == singleProduct.Id);
            var quantity = _quantityRepository.Find(q => q.ProduktId == singleProduct.Id)?.Ilosc1;
            var photo = _photoRepository.Find(pd => pd.ProduktId == singleProduct.Id)?.PathDoZdj;

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
                Quantity = quantity ?? 0,
                ImgPath = photo
            };

            return result;
        }

        public async Task<ProductDto> AddProduct(AddProductDto dto)
        {
            
            var category =  _categoryRepository.Find(x => x.Id == dto.CategoryId);
            var subCategory = _subCategoryRepository.Find(x => x.Id == dto.SubCategoryId);
            var product = await _productRepository.AddAsync(new Produkt
            {
                NazwaProduktu = dto.ProductName,
                Kategorie = category,
                Podkategorie = subCategory
            });

            var descToSave = new ProduktOpi()
            {
                Opis = dto.ProductDesc,
                Produkt = product
            };

            var desc = await _descRepository.AddAsync(descToSave);

            var priceToSave = new Cena()
            {
                CenaBrutto = dto.ProductPrice,
                Produkt = product
            };
            var price = await _priceRepository.AddAsync(priceToSave);

            var quantityToSave = new Ilosc()
            {
                Ilosc1 = dto.Quantity,
                Produkt = product
            };

            var quantity = await _quantityRepository.AddAsync(quantityToSave);

            var productDto = new ProductDto
            {
                ProductId = product.Id,
                ProductName = product.NazwaProduktu,
                ProductDesc = desc.Opis,
                ProductCode = 0,
                Category = category.NazwaKategorii,
                Subcategory = subCategory.NazwaPodkategorii,
                BruttoPrice = price.CenaBrutto,
                NettoPrice = price.CenaNetto,
                Quantity = quantity.Ilosc1,
                ImgPath = null
            };

            return productDto;
        }

        public List<ProductToCart> GetProductToCart(string numbers)
        {
            Dictionary<int,int> lists = new();

            var count = numbers.Count(e => e == ';');
            for (int i = 0; i < count; i++)
            {
                var toAddList = numbers[..numbers.IndexOf(';')];
                numbers = numbers.Remove(0, numbers.IndexOf(';') + 1);
                if (lists.Any(x => x.Key == int.Parse(toAddList)))
                {
                    foreach (var (key, value1) in lists)
                    {
                        var valueToadd = value1;
                        if (key == int.Parse(toAddList)) lists[key] = valueToadd + 1;
                    }
                }
                else lists.Add(int.Parse(toAddList), 1);
            }

            return (from x in lists 
                let name = _productRepository.Find(c => c.Id == x.Key).NazwaProduktu 
                let price = _priceRepository.Find(p => p.ProduktId == x.Key).CenaBrutto 
                select new ProductToCart
            {
                ProductId = x.Key,
                Quantity = x.Value.ToString(),
                ProductName = name, 
                BruttoPrice = price.ToString(CultureInfo.InvariantCulture)
            }).ToList();
        }
    }
}
