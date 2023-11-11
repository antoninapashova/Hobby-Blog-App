﻿using Application.Categories.Commands.Create;
using Application.Categories.Commands.Delete;
using HobbyProject.Application.Categories.Queries.GetAllCategories;
using HobbyProject.Application.Categories.Queries.GetAllNames;
using HobbyProject.Application.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HobbyProject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly IMediator _mediator;
        
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetCategoriesListQuery());
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("names")]
        public async Task<ActionResult> GetAllNames()
        {
            var result = await _mediator.Send(new GetAllNamesQuery());
            return Ok(result);
        }


        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetCategoryByIdQuery { Id = id };
            var result = await _mediator.Send(query); 
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
