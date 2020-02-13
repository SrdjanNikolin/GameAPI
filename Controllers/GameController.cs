using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GamesApi.Domain.Services;
using GamesApi.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;

namespace GamesApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        //TODO: Should check exception handling.
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        private IGameService _gameService;

        [HttpGet("all")]
        public async Task<ActionResult<string>> GetAllGames()
        {
            var games = await _gameService.GetAllGames();
            if (games == null)
            {
                return NotFound();
            }
            string json = JsonConvert.SerializeObject(games, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetGame(int id)
        {
            //TODO: Validate all methods that can return null etc.
            var game =  await _gameService.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }
            string json = JsonConvert.SerializeObject(game, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
        [HttpGet("getLastGame")]
        public async Task<ActionResult<string>> GetLastGame()
        {
            var lastGame = await _gameService.GetLastGame();
            if (lastGame != null)
            {
                string json = JsonConvert.SerializeObject(lastGame, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return json;
            }
            return NotFound();
        }
        [HttpPost("addGame")]
        public async Task<ActionResult> AddGame([FromBody]Game gameToAdd)
        {
            if (ModelState.IsValid)
            {
                await _gameService.AddGame(gameToAdd);
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            var gameToDelete = await _gameService.GetGame(id);
            if(gameToDelete != null)
            {
               await _gameService.DeleteGame(gameToDelete);
                return Ok();
            }
            return NotFound();
        }
        [HttpPatch("update/{gameId}")]
        public async Task<ActionResult> UpdateGame(int gameId, [FromBody] JsonPatchDocument<Game> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var gameToUpdate = await _gameService.GetGame(gameId);
            if (gameToUpdate == null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(gameToUpdate, ModelState);
            //For some reason it does not validate automatically, so i need to re-validate here.
            TryValidateModel(gameToUpdate);
            if (!ModelState.IsValid) //Might not be needed
            {
                return BadRequest(ModelState);
            }
            await _gameService.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("addImage")]
        public async Task<ActionResult> AddGameImage([FromBody]GameImage gameImage)
        {
            if(gameImage == null)
            {
                return BadRequest();
            }
            else if (_gameService.GetGame(gameImage.GameId) == null)
            {
                return NotFound();
            }
            await _gameService.AddGameImageAsync(gameImage);
            return Ok();
        }
        [HttpGet("getImage/{gameId}")]
        public async Task<ActionResult<GameImage>> GetGameImage(int gameId)
        {
           var gameImage = await _gameService.GetGameImageAsync(gameId);
            if (gameImage == null)
            {
                return NotFound();
            }
            return gameImage;
        }
        [HttpPatch("updateImage/{gameImageId}")]
        public async Task<ActionResult> UpdateGameImage(int gameImageId, [FromBody] JsonPatchDocument<GameImage> patchDoc)
        {
            var gameImage = await _gameService.GetGameImageAsync(gameImageId);
            if(gameImage == null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(gameImage, ModelState);
            await _gameService.SaveChangesAsync();

            return Ok();
        }
    }
}