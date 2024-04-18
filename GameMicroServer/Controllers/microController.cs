using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using GameMicroServer.Services;
using Microsoft.AspNetCore.SignalR;

namespace Micro
{
    [ApiController]
    [Route("[controller]")]
    public class MicroController : ControllerBase
    {
        GameInfo[] info;
        private readonly PongClient _pongClient;
        private readonly TetrisClient _tetrisClient;
        private readonly SnakeClient _snakeClient;
        private static readonly List<GameInfo> TheInfo = new List<GameInfo>
        {
            //Remove this code once individual microservices are set up
            new GameInfo {
                Id = 1,
                Title = "Snake",
                //Content = "~/js/snake.js",
                Author = "Hillary clinton ",
                DateAdded = "01/01/1942",
                Description = "Look at me im a SNEEEEK",
                HowTo = "https://youtu.be/dQw4w9WgXcQ?si=FB4F4RdMOvOoBI85",
                //Thumbnail = "/images/snake.jpg" //640x360 resolution

            },
            new GameInfo {
                Id = 2,
                Title = "Tetris",
                //Content = "~/js/tetris.js",
                Author = "Steve from minecraft",
                DateAdded = "09/09/1541",
                Description = "Block Block Block",
                HowTo = "Put Blocks Down",
                //Thumbnail = "/images/tetris.jpg"
            },
            new GameInfo {
                Id = 3,
                Title = "Pong",
                //Content = "~/js/pong.js",
                Author = "Forest Gump",
                DateAdded = "07/04/1742",
                Description = "RUN FOREST RUN!",
                HowTo = "Hit the back back",
                //Thumbnail = "/images/pong.jpg"
            },


        };

        private readonly ILogger<MicroController> _logger;

        public MicroController(ILogger<MicroController> logger, PongClient pong, TetrisClient tetrisClient, SnakeClient snakeClient)
        {
            _logger = logger;
            _pongClient = pong;
            _tetrisClient = tetrisClient;
            _snakeClient = snakeClient;
        }

        private readonly IGameRepository _gameRepo;
        //public MicroController(IGameRepository gameRepo)
        //{
        //    _gameRepo = gameRepo;
        //}

        //[HttpGet("game/{id}")]
        //public IActionResult Get(int id)
        //{
        //    var game = _gameRepo.GetByIdAsync(id);
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(game);
        //}
        // This method will return the GameInfo object with the specified ID
        [HttpGet("Games/Play/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // orignal code from Noah and chris to pass through gateway
            //var game = TheInfo.FirstOrDefault(g => g.Id == id);
            //if (game == null)
            //{
            //    return NotFound();
            //}
            //return Ok(game);
            if (id == 1)
            {
                info = await _snakeClient.GetGameByIdAsync(1);
            }
            else if (id == 2)
            {
                info = await _tetrisClient.GetGameByIdAsync(2);

            }
            else if(id == 3)
            {
                info = await _pongClient.GetGameByIdAsync(3);
                
            }

            if (info == null)
            {
               return NotFound();
            } 
            return Ok(info[0]);

        }


        [HttpGet]
        public IEnumerable<GameInfo> Get()
        {
            return TheInfo;
        }
    }
}
