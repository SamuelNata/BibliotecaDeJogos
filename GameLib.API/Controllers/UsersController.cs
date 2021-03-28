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
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        
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
    }
}
