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
            //return games.ToList();
        }
        [HttpPost("addgame")]
        public async Task<ActionResult<Game>> AddGame([FromBody]Game game)
        {
            if(ModelState.IsValid)
            {
                await _gameService.AddGame(game);
                return RedirectToAction("GetAllGames");
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteGame(int gameId)
        {
            var gameToDelete = await _gameService.GetGame(gameId);
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
            return RedirectToAction("GetAllGames");
        }
        // add {id}? 
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
            await _gameService.SaveChangesAsync();
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
        [HttpPatch("updateImage/{gameId}")]
        public async Task<ActionResult> UpdateGameImage(int gameId, [FromBody]string imagePath)
        {
            var gameImage = await _gameService.GetGameImageAsync(gameId);
            if(gameImage == null)
            {
                return NotFound();
            }
            JsonPatchDocument patchDocument = new JsonPatchDocument();
            string imagePathBase64 = _gameService.ConvertImagePathToBase64(imagePath);
            patchDocument.Replace("/GameImageData", imagePathBase64).ApplyTo(gameImage);
            await _gameService.SaveChangesAsync();

            return Ok();
        }
    }
}