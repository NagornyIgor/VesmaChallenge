using MediatR;
using Vesma.Contracts.Vessels;
using Vesma.Data;
using FluentResults;
using Vesma.Core.Mappers;
using Vesma.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Vesma.Core.Handlers.Vessels.Commands.RegisterVessel;

public class RegisterVesselCommandHandler(VesmaDbContext dbContext, AppMapper mapper)
    : IRequestHandler<RegisterVesselCommand, Result<VesselDto>>
{
    private readonly VesmaDbContext _dbContext = dbContext;
    private readonly AppMapper _mapper = mapper;

    public async Task<Result<VesselDto>> Handle(RegisterVesselCommand request, CancellationToken cancellation)
    {
        Vessel vessel = _mapper.VesselApiToDomain(request);

        IMO? dbImo = await _dbContext.IMOs
            .FirstOrDefaultAsync(x => x.Name == vessel.Imo!.Name, cancellation);

        if(dbImo != null)
        {
            vessel.Imo = dbImo;
        }

        await _dbContext.Vessels.AddAsync(vessel, cancellation);
        await _dbContext.SaveChangesAsync(cancellation);

        return Result.Ok(_mapper.VesselDomainToApi(vessel));
    }
}