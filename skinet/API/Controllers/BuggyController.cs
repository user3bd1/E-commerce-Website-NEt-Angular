using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
  [HttpGet("unauthorized")]
  public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("badrequest")]
  public IActionResult GetBadRequest()
    {
        return BadRequest("this is an unvaild request");
    }

    [HttpGet("notfound")]
  public IActionResult GetNotFound()
    {
        return NotFound("request was not found");
    }

    [HttpGet("internalerror")]
  public IActionResult GetInternalError()
    {
        throw new Exception("this is an internal error");
    }

    [HttpPost("validationerror")]
  public IActionResult GetValidationError(CreateProductDto product)
    {
        return Ok();
    }

}
