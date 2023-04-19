namespace Backend.src.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[Route("api/v1/[controller]s")]
public abstract class ApiControllerBase : ControllerBase
{ }