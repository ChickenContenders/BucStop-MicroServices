using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using GameMicroServer.Services;

namespace Micro
{
    [ApiController]
    [Route("[controller]")]
    public class MicroController : ControllerBase
    {
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
            }
        };

        private readonly ILogger<MicroController> _logger;

        public MicroController(ILogger<MicroController> logger)
        {
            _logger = logger;
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
        public IActionResult GetById(int id)
        {
            var game = TheInfo.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpGet]
        public IEnumerable<GameInfo> Get()
        {
            return TheInfo;
        }
    }
}
