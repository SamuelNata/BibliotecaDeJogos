using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GameLib.Service;
using GameLib.Model.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using GameLib.Model.Entity;
using AutoMapper;
using GameLib.Model.ViewModel;

namespace GameLib.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UsersController : UserScopedController
    {
        
        private readonly IUserService _userService;
        private readonly IUserGameService _userGameService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public UsersController(
            IUserService userService,
            IUserGameService userGameService,
            IGameService gameService,
            IMapper mapper)
        {
            _userService = userService;
            _userGameService = userGameService;
            _gameService = gameService;
            _mapper = mapper;
        }

        #region User Endpoints

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<UserVM>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAll();
            var usersVM = _mapper.Map<List<UserVM>>(users);
            return Ok(usersVM);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(UserVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            var userVM = _mapper.Map<UserVM>(user);

            return Ok(userVM);
        }
    
        #endregion User Endpoints


        #region User's Game Endpoints        
        
        [HttpGet]
        [Route("my-games")]
        [ProducesResponseType(typeof(List<GameInfoDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListMyGames()
        {
            return Ok(await _userGameService.SearchGamesBy(new Guid(CurrentUserId)));
        }
        
        [HttpPut]
        [Route("my-games")]
        [ProducesResponseType(typeof(MessageApiResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AcquireGame([FromBody]CreateUserGameVM model)
        {
            if (ModelState.IsValid)
            {
                var userGame = new UserGame
                {
                    UserId = new Guid(CurrentUserId),
                    GameId = model.GameId
                };
                var result = await _userGameService.Save(userGame);
                if(result > 0)
                {
                    var game = await _gameService.GetById(model.GameId);
                    return Ok(new MessageApiResult{
                        Success = true,
                        Message = $"Agora '{game.Name}' faz parte da sua coleção!"
                    });
                }
                else
                {
                    return StatusCode(400, new MessageApiResult{
                        Success = false,
                        Message = "Não foi possível adicionar o este jogo a sua coleção"
                    });
                }
            }
            else
            {
                var erros = new List<string>();
                ModelState.Values.ToList().ForEach(v => 
                    erros.AddRange(v.Errors.Select(e => e.ErrorMessage).ToList())
                );
                return StatusCode(400, new MessageApiResult
                { 
                    Success = false,
                    Message = string.Join("; ", erros)
                });
            }
            
        }

        [HttpDelete]
        [Route("my-games/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGameFromUser(Guid id)
        {
            var result = await _userGameService.RemoveGameFromUser(id, new Guid(CurrentUserId));
            return NoContent();
        }

        #endregion User's Game Endpoints
    }
}
