using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roulette.Business.Services;
using static Roulette.Business.Enum;
using Microsoft.AspNetCore.Authorization;

namespace RouletteMezubo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RouletteController : ControllerBase
    {
        private readonly RouletteService _rouletteService;
        private readonly BetService _betService;
        public RouletteController(RouletteService rouletteService, BetService betService)
        {
            _rouletteService = rouletteService;
            _betService = betService;
        }
        [HttpPost]
        public async Task<IActionResult> SaveRoulette([FromBody] Roulette.Entities.Roulette roulette)
        {
            try
            {
                var result = await _rouletteService.SaveRoulette(roulette);
                if (result != 0)
                {
                    return Ok(result);
                }
                return BadRequest(ResponseMessages[ResponseCode.ErrorSave]);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseMessages[ResponseCode.InternalError]);
            }
        }
        [HttpPost]
        public async Task<IActionResult> OpenRoulette(int rouletteId)
        {
            try
            {
                var result = await _rouletteService.OpenRoulette(rouletteId);
                if (result == ResponseCode.Ok)
                {
                    return Ok(result);
                }
                return BadRequest(ResponseMessages[ResponseCode.Rejected]);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseMessages[ResponseCode.InternalError]);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GameRoulette(Roulette.Entities.Bet betGame)
        {
            try
            {
                var result = await _betService.GameRoulette(betGame);
                if (result == ResponseCode.Ok)
                {
                    return Ok(result);
                }
                return BadRequest(ResponseMessages[ResponseCode.Rejected]);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseMessages[ResponseCode.InternalError]);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CloseRoulette(int rouletteId)
        {
            try
            {
                var result = await _rouletteService.CloseRoulette(rouletteId);
                if (result != 0)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseMessages[ResponseCode.InternalError]);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ListRoulette()
        {
            try
            {
                var result = await _rouletteService.ListRoulette();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseMessages[ResponseCode.InternalError]);
            }
        }

    }
}
