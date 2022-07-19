using Microsoft.AspNetCore.Mvc;
using GeometryCurrencyApp.CircleApiFeature.Models;
using GeometryCurrencyApp.CircleApiFeature.Query;
using MediatR;


namespace GeometryCurrencyApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GeometryCurrencyController : ControllerBase
{
    private readonly IMediator _mediator;

    public GeometryCurrencyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GeometryCurrencyResponseModel>> Index([FromQuery] GeometryCurrencyRequestModel request)
    {
        try
        {
            return await _mediator.Send(new GetCurrencyInCircleQuery()
            { 
                XCoord = request.XCoord,
                YCoord = request.YCoord,
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }       
    }
}
