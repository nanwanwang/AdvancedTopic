using MassTransit;
using MassTransitCommon;
using Microsoft.AspNetCore.Mvc;

namespace MassTransitWebServer;

public class ValueController:ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ValueController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<ActionResult> Post(string value)
    {
        await _publishEndpoint.Publish<ValueEntered>(new {Value = value}, context =>
        {
            context.
        } );
        return Ok();
    }
}