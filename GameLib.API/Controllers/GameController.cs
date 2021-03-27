using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GameLib.Service;
using GameLib.Model.Entity;
using Microsoft.AspNetCore.Http;
using GameLib.Model.ViewModel;
using AutoMapper;

namespace GameLib.API.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController : UserScopedController
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Game>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListGames()
        {
            return Ok(await _gameService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var game = await _gameService.GetById(id.Value);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        
        [HttpPut]
        [Route("")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([Bind("Name,Year")] CreateEditGameVM model)
        {
            var game = _mapper.Map<Game>(model);
            if (ModelState.IsValid)
            {
                await _gameService.Save(game);
            }
            return Ok(game);
        }

        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(typeof(Game), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Year")] CreateEditGameVM model)
        {
            var game = _mapper.Map<Game>(model);
            game.Id = id;

            if (ModelState.IsValid)
            {
                try
                {
                    await _gameService.Update(game);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok(game);
        }

        private async Task<bool> GameExists(Guid id)
        {
            return (await _gameService.GetById(id)) != null;
        }
    }
}
