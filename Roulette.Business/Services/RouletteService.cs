using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Roulette.Data.Context;
using Roulette.Entities;
using static Roulette.Business.Enum;
using System.Linq;

namespace Roulette.Business.Services
{
    public class RouletteService
    {
        private readonly BetService _betService;

        public RouletteService(BetService betService)
        {
            _betService = betService;
        }
        public async Task<int> SaveRoulette(Entities.Roulette roulette)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<Data.Context.RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using var context = new RouletteContext(optionsBuilder.Options);
                context.Roulette.Add(roulette);
                if (await context.SaveChangesAsync() > 0)
                    return roulette.Id;

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateRoulette(Entities.Roulette roulette)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using (var context = new RouletteContext(optionsBuilder.Options))
                {
                    context.Entry(roulette).State = EntityState.Modified;
                    return await context.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Entities.Roulette GetRoulette(int rouletteId)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using var context = new RouletteContext(optionsBuilder.Options);

                return context.Roulette.FirstOrDefault(x => x.Id == rouletteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Entities.Roulette>> ListRoulette()
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using var context = new RouletteContext(optionsBuilder.Options);
                var listResult = await context.Roulette.ToListAsync();
                if (listResult != null && listResult.Count > 0)
                {
                    foreach (var item in listResult)
                    {
                        item.StateDesc = StateGameDes[item.State];
                    }
                }
                return listResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseCode> OpenRoulette(int rouletteId)
        {
            try
            {
                var rouletteGet = GetRoulette(rouletteId);
                if (rouletteGet != null)
                    rouletteGet.State = Convert.ToInt32(StateGame.Open);
                rouletteGet.DateIni = DateTime.Now;
                if (await UpdateRoulette(rouletteGet))
                    return ResponseCode.Ok;

                return ResponseCode.Rejected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<double> CloseRoulette(int rouletteId)
        {
            try
            {
                double sumAmount = 0;
                var optionsBuilder = new DbContextOptionsBuilder<RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using var context = new RouletteContext(optionsBuilder.Options);
                var rouletteGet = await context.Roulette.FirstOrDefaultAsync(x => x.Id == rouletteId);
                if (rouletteGet != null)
                    rouletteGet.State = Convert.ToInt32(StateGame.Close);
                rouletteGet.DateEnd = DateTime.Now;
                if (await UpdateRoulette(rouletteGet))
                {
                    var listBet = _betService.ListBet(rouletteId);
                    foreach (var item in listBet)
                        sumAmount += item.Amount;
                }

                return sumAmount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
