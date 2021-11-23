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

        public OrderService(OrdersRepository ordersRepository, KlientKontoRepository kontoRepository, ProductRepository productRepository, PriceRepository priceRepository, ClientRepository clientRepository, Xkom_ProjektContext dbContext)
        {
            _ordersRepository = ordersRepository;
            _kontoRepository = kontoRepository;
            _productRepository = productRepository;
            _priceRepository = priceRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
        }


        public async Task Create(OrderDto dto)
        {
            var clientKonto = await _kontoRepository.FindAsync(dto.ClientId);
            if (clientKonto is null) throw new ForbiddenException("You are not authorized!");
            List<Produkt> products = new();

            dto.ProductsId.ForEach(p =>
            {
                var product = _productRepository.Find(c => c.Id == p);

                products.Add(product);
            });

            List<Zamowienium> orders = new();

            var client = _clientRepository.Find(c => c.KlientKontos.Contains(clientKonto));

            products.ForEach(pp =>
            {
                Zamowienium order = new()
                {
                    Cena = _priceRepository.Find(price => price.Produkt == pp),
                    IdKlientaNavigation = client,
                    Produkt = pp,
                    StatusPlatnosci = _dbContext.StatusPlatnoscis.FirstOrDefault(x => x.Id == 3),
                    StatusZamowienia = _dbContext.StatusZamowienia.FirstOrDefault(x => x.Id == 4)
                };

                orders.Add(order);
            });

            await _ordersRepository.AddRangeAsync(orders);

        }
    }
}
