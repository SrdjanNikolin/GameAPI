using GamesApi.Domain.Models;
using GamesApi.Domain.Services;
using GamesApi.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GamesApi.Services
{
    public class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;
        public UserService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        //should get users from a database.
        private List<User> users = new List<User>
        {
            new User {UserId = 1, Username = "Bob", Password = "Jenkins", Email = "123@gmail.com"}
        };

        public User Authenticate(string username, string password)
        {
            User user = users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                return null;
            }

            //authentication successful so generate jwt token.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
               }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return users.WithoutPassword();
        }
    }
}