using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Search.API.Validator;
using Search.Application.Commands;
using Search.Application.Queries;
using System;
using System.Threading.Tasks;

namespace Search.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates user records in User table
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [HttpPost("/api/v1.0/user/create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
        {
            //FluentValidation validation for CreateUserCommandRequest
            CreateUserRequestValidator validator = new();
            ValidationResult validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                var response = await _mediator.Send(request);
                return Ok(response.UserId);
            }
            else
            {
                throw new Exception("Validation failed!");
            }
        }

        /// <summary>
        /// Gets all user details from User table
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("/api/v1.0/user/users")]
        public async Task<IActionResult> GetUsers()
        {
            //if user is null than throw Unauthorized error 
            var user = HttpContext.Items["User"] as string;
            if (user == null)
            {
                return Unauthorized("Please provide valid authentication token.");
            }
            else
            {
                var result = await _mediator.Send(new GetUserListQuery());
                return Ok(result);
            }
        }
    }
}
