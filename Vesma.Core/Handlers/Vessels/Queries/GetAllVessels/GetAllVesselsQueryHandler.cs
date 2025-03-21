using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vesma.Contracts.Vessels;
using Vesma.Core.Mappers;
using Vesma.Data;
using Vesma.Data.Models;

namespace Vesma.Core.Handlers.Vessels.Queries.GetAllVessels;

public class GetAllVesselsQueryHandler(VesmaDbContext dbContext, AppMapper mapper)
    : IRequestHandler<GetAllVesselsQuery, Result<VesselDto[]>>
{
    private readonly VesmaDbContext _dbContext = dbContext;
    private readonly AppMapper _mapper = mapper;

    public async Task<Result<VesselDto[]>> Handle(GetAllVesselsQuery request, CancellationToken cancellation)
    {
        Vessel[] dbVessels = await _dbContext.Vessels
            .AsNoTracking()
            .Include(x => x.Imo)
            .ToArrayAsync(cancellation);

        return Result.Ok(dbVessels.Select(_mapper.VesselDomainToApi).ToArray());
    }
}
