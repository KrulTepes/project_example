using Microsoft.AspNetCore.Mvc;
using System;
using vtbbook.Application.Service;
using vtbbook.Application.Service.Models;
using vtbbook.Models;

namespace vtbbook.Controllers
{
    [ApiController]
    public class SomeConroller : ControllerBase
    {
        private readonly ISomeService _someService;

        public SomeConroller(ISomeService someService)
        {
            _someService = someService;
        }

        [Route("ping")]
        [HttpGet]
        public IActionResult SomePing([FromBody] RequestSomeModel request)
        {
            if (string.IsNullOrEmpty(request.SomeSender) || string.IsNullOrEmpty(request.SomeText))
            {
                return BadRequest();
            }

            var id = _someService.Some(new SomeModel 
            { 
                SomeText = request.SomeText,
                SomeSender = request.SomeSender
            });

            if (id == Guid.Empty)
            {
                return new StatusCodeResult(500);
            }

            return Ok($"Some ping id = {id}");
        }
    }
}
