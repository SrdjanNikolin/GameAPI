using GamesApi.Domain.Models;
using System.Collections.Generic;

namespace GamesApi.Domain.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}