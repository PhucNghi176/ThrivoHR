﻿using EXE201_BE_ThrivoHR.API.Services;
using EXE201_BE_ThrivoHR.Application.UseCase.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_BE_ThrivoHR.API.Controllers.Authenticate
{

    public class Authentication(ISender sender, JwtService jwtService) : BaseController(sender)
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate([FromBody] LoginQuery loginQuery)
        {
            var result = await _sender.Send(new LoginQuery(loginQuery.EmployeeCode, loginQuery.Password));
            var token = jwtService.CreateToken(result.Value);
            return Ok(token);
        }
    }
}