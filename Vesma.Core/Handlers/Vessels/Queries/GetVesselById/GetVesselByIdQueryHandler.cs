using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vesma.Contracts.Vessels;
using Vesma.Core.Mappers;
using Vesma.Data;
using Vesma.Data.Models;

namespace Vesma.Core.Handlers.Vessels.Queries.GetVesselById;

public class GetVesselByIdQueryHandler(VesmaDbContext dbContext, AppMapper mapper)
    : IRequestHandler<GetVesselByIdQuery, Result<VesselDto>>
{
    private readonly VesmaDbContext _dbContext = dbContext;
    private readonly AppMapper _mapper = mapper;

    public async Task<Result<VesselDto>> Handle(GetVesselByIdQuery request, CancellationToken cancellation)
    {
        Vessel? dbVessel = await _dbContext.Vessels
            .Include(x => x.Imo)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellation);

        if (dbVessel == null)
        {
            return Result.Fail("Failed to find required vessel.");
        }

        return Result.Ok(_mapper.VesselDomainToApi(dbVessel));
    }
}
