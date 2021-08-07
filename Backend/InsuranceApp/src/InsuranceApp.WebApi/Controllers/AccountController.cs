﻿using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] CreateUserDto newUserDto)
        {
            var user = await _userService.CreateUser(newUserDto);

            return Created($"api/users/{user.Id}", user);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = await _userService.SignIn(loginUserDto);

            if (result)
                return Ok("You have signed in!");
            else
                return Unauthorized();
            
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken(LoginUserDto loginDataDto)
        {
            var token = await _tokenService.GetToken(loginDataDto);

            if (token == null)
                return BadRequest();

            return Ok(token);
        }

    }
}