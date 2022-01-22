using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Web.Dtos;
using Shop.Web.Entities.Model;
using Shop.Web.Entities.Repository;
using Shop.Web.Exceptions;

namespace Shop.Web.Services
{
    public interface IOrderService
    {
        Task Create(OrderDto dto);
    }

    public class OrderService : IOrderService
    {
        private readonly OrdersRepository _ordersRepository;
        private readonly KlientKontoRepository _kontoRepository;
        private readonly ProductRepository _productRepository;
        private readonly PriceRepository _priceRepository;
        private readonly ClientRepository _clientRepository;
        private readonly Xkom_ProjektContext _dbContext;
        private readonly KlientRepository _klientRepository;

        public OrderService(OrdersRepository ordersRepository, KlientKontoRepository kontoRepository, ProductRepository productRepository, PriceRepository priceRepository, ClientRepository clientRepository, Xkom_ProjektContext dbContext, KlientRepository klientRepository)
        {
            _ordersRepository = ordersRepository;
            _kontoRepository = kontoRepository;
            _productRepository = productRepository;
            _priceRepository = priceRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
            this._klientRepository = klientRepository;
        }


        public async Task Create(OrderDto dto)
        {
            List<Produkt> products = new();

            Dictionary<int, int> lists = new();

            var count = dto.ProductsId.Count(e => e == ';');
            for (int i = 0; i < count; i++)
            {
                var toAddList = dto.ProductsId[..dto.ProductsId.IndexOf(';')];
                dto.ProductsId = dto.ProductsId.Remove(0, dto.ProductsId.IndexOf(';') + 1);
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
            
            foreach((var key, var value) in lists)
            {
                var product = _productRepository.Find(x => x.Id == key);
                products.Add(product);
            }

            List<Zamowienium> orders = new();

            KlientKonto klientKontos = new()
            {
                Mail = dto.Email,
                Haslo = new byte[0],
                Klient = SetClient(dto)
            };

            products.ForEach(pp =>
            {
                Zamowienium order = new()
                {
                    Cena = _priceRepository.Find(price => price.Produkt == pp),
                    jakiezamow_id = _dbContext.Zamowienia.OrderByDescending(x => x.jakiezamow_id).FirstOrDefault().jakiezamow_id + 1,
                    IdKlientaNavigation = klientKontos.Klient,
                    Produkt = pp,
                    StatusPlatnosci = _dbContext.StatusPlatnoscis.FirstOrDefault(x => x.Id == 3),
                    StatusZamowienia = _dbContext.StatusZamowienia.FirstOrDefault(x => x.Id == 4),
                    Ilosc = lists[pp.Id]
                };

                orders.Add(order);
            });

            await _ordersRepository.AddRangeAsync(orders);

        }

        private Klient SetClient(OrderDto dto)
        {
            var isExist = _clientRepository.Find(x => x.Imie == dto.FirstName && x.Nazwisko == dto.LastName && x.Mail == dto.Email && x.Telefon == dto.Phone);
            if (isExist is not null) return isExist;
            return new Klient()
            {
                Imie = dto.FirstName,
                Nazwisko = dto.LastName,
                Telefon = dto.Phone,
                Miasto = dto.City,
                KodPocztowy = dto.PostalCode,
            };
        }
    }
}
