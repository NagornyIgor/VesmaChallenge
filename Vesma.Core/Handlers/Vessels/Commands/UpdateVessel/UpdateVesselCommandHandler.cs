using MediatR;
using Vesma.Contracts.Vessels;
using Vesma.Data;
using FluentResults;
using Vesma.Core.Mappers;
using Vesma.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Vesma.Core.Handlers.Vessels.Commands.UpdateVessel;

public class UpdateVesselCommandHandler(VesmaDbContext dbContext, AppMapper mapper)
    : IRequestHandler<UpdateVesselCommand, Result<VesselDto>>
{
    private readonly VesmaDbContext _dbContext = dbContext;
    private readonly AppMapper _mapper = mapper;

    public async Task<Result<VesselDto>> Handle(UpdateVesselCommand request, CancellationToken cancellation)
    {
        Vessel? dbVessel = await _dbContext.Vessels
            .Include(x => x.Imo)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellation);

        if(dbVessel == null)
        {
            return Result.Fail("Failed to find required vessel.");
        }

        string imo = request.IMO.ToLower();
        if (dbVessel.Imo!.Name != imo)
        {
            IMO? dbImo = await _dbContext.IMOs.FirstOrDefaultAsync(x => x.Name == imo, cancellation);
            dbVessel.Imo = dbImo ?? new IMO { Name = imo };
        }

        _mapper.ApplyUpdatesVessel(request, dbVessel);
        await _dbContext.SaveChangesAsync(cancellation);

        return Result.Ok(_mapper.VesselDomainToApi(dbVessel));
    }
}
