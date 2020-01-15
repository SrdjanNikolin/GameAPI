﻿using GamesApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesApi.Domain.Repositores
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGame(int id);
        Task AddGame(Game gameToAdd);
        Task DeleteGame(Game gameToDelete);
        Task SaveChangesAsync();
        Task AddGameImageAsync(GameImage gameImage);
        Task<GameImage> GetGameImageAsync(int gameId);
        Task UpdateGameImageAsync(GameImage gameImage, string newImage);
        string ConvertImagePathToBase64(string imagePath);
    }
}