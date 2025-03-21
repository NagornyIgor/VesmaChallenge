using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vesma.Api.Common;
using Vesma.Contracts.Vessels;
using Vesma.Core.Handlers.Vessels.Commands.RegisterVessel;
using Vesma.Core.Handlers.Vessels.Commands.UpdateVessel;
using Vesma.Core.Handlers.Vessels.Queries.GetAllVessels;
using Vesma.Core.Handlers.Vessels.Queries.GetVesselById;

namespace Vesma.Api.Controllers;

[ApiController]
[Route("vessels")]
public class VesselsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VesselsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get list of all registered vessels.
    /// </summary>
    /// <param name="cancellation">Cancelation token</param>
    /// <returns>List of registered vessels</returns>
    [HttpGet]
    [ProducesResponseType<ICollection<VesselDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetVessels(CancellationToken cancellation)
    {
        var result = await _mediator.Send(new GetAllVesselsQuery(), cancellation);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ToActionResult());
    }

    /// <summary>
    /// Get vessel by id
    /// </summary>
    /// <param name="id">Vessel id</param>
    /// <param name="cancellation">Cancelation token</param>
    /// <returns>Requested vessel data</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<VesselDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetVesselById([FromRoute] Guid id, CancellationToken cancellation)
    {
        var result = await _mediator.Send(new GetVesselByIdQuery(id), cancellation);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ToActionResult());
    }

    /// <summary>
    /// Create vessel
    /// </summary>
    /// <param name="command">Vessel data</param>
    /// <param name="cancellation">Cancelation token</param>
    /// <returns>Created vessel data</returns>
    [HttpPost]
    [ProducesResponseType<VesselDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateVessel([FromBody] RegisterVesselCommand command, CancellationToken cancellation)
    {
        var result = await _mediator.Send(command, cancellation);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ToActionResult());
    }

    /// <summary>
    /// Update vessel
    /// </summary>
    /// <param name="id">Vessel id</param>
    /// <param name="command">Vessel data</param>
    /// <param name="cancellation">Cancelation token</param>
    /// <returns>Created vessel data</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType<VesselDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateVessel([FromRoute] Guid id, [FromBody] UpdateVesselCommand command, CancellationToken cancellation)
    {
        command.Id = id;
        var result = await _mediator.Send(command, cancellation);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.ToActionResult());
    }
}
