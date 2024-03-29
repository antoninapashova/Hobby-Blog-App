﻿using Application.Hobby.Commands.Create;
using Application.Hobby.Commands.Delete;
using Application.Hobby.Commands.Edit;
using HobbyProject.Application.Hobby.Queries.GetAllUsers;
using HobbyProject.Application.Hobby.Queries.GetHobbiesByUsername;
using HobbyProject.Application.Hobby.Queries.GetHobbyById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HobbyArticleController : ControllerBase
    {
        public readonly IMediator _mediator;

        public HobbyArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [Authorize]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllHobbyArticles()
        {
            var result = await _mediator.Send(new GetHobbyListQuery());
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetHobbyByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var query = new GetHobbiesByUsernameQuery { Username = username };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddHobbyArticle([FromBody] CreateHobbyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHobbyArticle([FromBody] UpdateHobbyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var command = new DeleteHobbyCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
