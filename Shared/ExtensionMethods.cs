using GamesApi.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Shared
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPassword(this IEnumerable<User> users)
        {
            return users.Select(u => u.WithoutPassword());
        }
        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }
        public static CookieOptions SetCookie()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.UtcNow.AddDays(7);
            options.HttpOnly = true;
            //set secure to true in production.
            options.Secure = false;
            return options;
        }
    }
}