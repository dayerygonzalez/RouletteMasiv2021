using Microsoft.EntityFrameworkCore;
using Roulette.Data.Context;
using Roulette.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static Roulette.Business.Enum;

namespace Roulette.Business.Services
{
   public class BetService
    {

        private readonly RouletteService _rouletteService;

        public BetService(RouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }
        public async Task<int> SaveBet(Entities.Bet gameBet)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<Data.Context.RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using var context = new RouletteContext(optionsBuilder.Options);
                context.Bet.Add(gameBet);
                if (await context.SaveChangesAsync() > 0)
                    return gameBet.Id;

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Bet> ListBet(int rouletteId)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<RouletteContext>();
                optionsBuilder.UseSqlServer("RouletteContext");
                using var context = new RouletteContext(optionsBuilder.Options);
                return context.Bet.Where(x => x.RouletteId == rouletteId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidateBet(Entities.Bet betGame)
        {
            try
            {
                if (betGame.Number > 36)
                    return false;
                if (betGame.Color != Convert.ToInt32(ColorGame.Black) && betGame.Color != Convert.ToInt32(ColorGame.Red)) 
                    return false;
                if (betGame.Amount > 10000)
                    return false;
               
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsDefined(Type enumType, Object value)
        {
            if (enumType == null)
                throw new ArgumentNullException("enumType");
            return enumType.IsEnumDefined(value);
        }
        public async Task<ResponseCode> GameRoulette(Entities.Bet betGame)
        {
            try
            {
                var rouletteGet = _rouletteService.GetRoulette(betGame.RouletteId);
                if (rouletteGet != null)
                    if (rouletteGet.State == Convert.ToInt32(StateGame.Open))
                    {
                        if (ValidateBet(betGame))
                            await SaveBet(betGame);

                        return ResponseCode.Ok;
                    }

                return ResponseCode.Rejected;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
