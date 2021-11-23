using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shop.Web.Dtos;
using Shop.Web.Entities.Model;
using Shop.Web.Entities.Repository;
using Shop.Web.Exceptions;

namespace Shop.Web.Services
{
    public interface IAccountService
    {
        Task Register(RegisterDto dto);
        string Login(LoginDto dto);
    }

    public class AccountService : IAccountService
    {
        private readonly KlientKontoRepository _kontoRepository;
        private readonly AuthenticationSettings _settings;
        private readonly ClientRepository _clientRepository;


        public AccountService(KlientKontoRepository kontoRepository, AuthenticationSettings settings, ClientRepository clientRepository)
        {
            _kontoRepository = kontoRepository;
            _settings = settings;
            _clientRepository = clientRepository;
        }
        public async Task Register(RegisterDto dto)
        {
            if ((dto.Email.ToLower() != dto.ConfirmEmail.ToLower()) | (dto.Password != dto.ConfirmPassword)) throw new NotFoundException("Email or password isn't same!");
            var isExist = _kontoRepository.Find(rr => rr.Mail == dto.Email);
            if (isExist is not null) throw new BadRequestException("This email already exist!");

            var newUser = new KlientKonto()
            {
                Mail = dto.Email
            };
            var passwordHash = KlientKonto.Encrypt(dto.Password);

            newUser.Haslo = passwordHash;
            newUser.Sol = Guid.NewGuid();

            var client = new Klient
            {
                Imie = "test",
                Nazwisko = "test",
                Nip = null,
                NazwaFirmy = null,
                KodPocztowy = null,
                Miasto = "test",
                Mail = dto.Email,
                Telefon = "123-456-789"
            };

            var clientAfterSave = await _clientRepository.AddAsync(client);

            newUser.Klient = clientAfterSave;

            await _kontoRepository.AddAsync(newUser);
        }

        public string Login(LoginDto dto)
        {
            var thisUserExist = _kontoRepository.Find(k => k.Mail == dto.Email);
            if (thisUserExist is null) throw new NotFoundException("Password or email is un valid!");

            var encryptPasswordDto = KlientKonto.Encrypt(dto.Password);
            if (thisUserExist.Haslo != encryptPasswordDto) throw new NotFoundException("Password or email is un valid!");

            return GenerateToken(thisUserExist.Id);
        }

        private string GenerateToken(int id)
        {
           var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtIssuer));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_settings.JwtExpireDay);


            var token = new JwtSecurityToken(_settings.JwtIssuer,
                _settings.JwtIssuer,
                claim,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenHandler;
        }
    }
}
