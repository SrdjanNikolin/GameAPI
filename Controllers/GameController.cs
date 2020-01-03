using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GamesApi.Domain.Services;
using GamesApi.Domain.Models;

namespace GamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        private IGameService _gameService;

        [HttpGet("", Name = "Index")]      
        public ActionResult<Task> Index()
        {
            return HttpContext.Response.WriteAsync("Hello from Index");
        }
        [HttpGet("games/{id}", Name = "GetGame")]
        public ActionResult<Game> GetGame(int id)
        {
            return _gameService.GetGame(id);
        }
        [HttpGet("games", Name = "GetAllGames")]
        public ActionResult<IEnumerable<Game>> GetAllGames()
        {
            return _gameService.GetAllGames().ToList();
        }
        [HttpPost("addgame")]
        public async Task<ActionResult<Game>> AddGame([FromBody]Game game)
        {
           await _gameService.AddGame(game);
           return Ok();
        }
    }
}