using Microsoft.EntityFrameworkCore;
using Roulette.Data.Context;
using Roulette.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Business.Services
{
    public class UserService
    {
        public async Task<User> Authenticate(string username, string password)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using var context = new RouletteContext(optionsBuilder.Options);

                return await context.User.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
