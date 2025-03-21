using Vesma.Contracts.Vessels;
using Vesma.Core.Base;

namespace Vesma.Core.Handlers.Vessels.Queries.GetAllVessels;

public record GetAllVesselsQuery : IRequestWrapper<VesselDto[]>;