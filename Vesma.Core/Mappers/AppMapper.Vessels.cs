using Riok.Mapperly.Abstractions;
using Vesma.Contracts.Vessels;
using Vesma.Core.Handlers.Vessels.Commands.RegisterVessel;
using Vesma.Core.Handlers.Vessels.Commands.UpdateVessel;
using Vesma.Data.Models;

namespace Vesma.Core.Mappers;

public partial class AppMapper
{
    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    [MapProperty(source: nameof(Vessel.Imo.Name), target: nameof(VesselDto.IMO))]
    public partial VesselDto VesselDomainToApi(Vessel vessel);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    [MapperIgnoreTarget(nameof(Vessel.Id))]
    [MapperIgnoreTarget(nameof(Vessel.ImoId))]
    [MapProperty(source: nameof(RegisterVesselCommand.IMO), target: nameof(Vessel.Imo), Use = nameof(MapIMO))]
    public partial Vessel VesselApiToDomain(RegisterVesselCommand vessel);

    [MapperRequiredMapping(RequiredMappingStrategy.Target)]
    [MapperIgnoreTarget(nameof(Vessel.Id))]
    [MapperIgnoreTarget(nameof(Vessel.ImoId))]
    [MapperIgnoreTarget(nameof(Vessel.Imo))]
    public partial void ApplyUpdatesVessel(UpdateVesselCommand api, Vessel domain);

    [UserMapping(Default = false)]
    public IMO MapIMO(string name) => new() { Name = name.ToLower() };
}
