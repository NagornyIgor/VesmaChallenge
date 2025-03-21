using FluentValidation;
using System.Text.Json.Serialization;
using Vesma.Contracts.Vessels;
using Vesma.Core.Base;

namespace Vesma.Core.Handlers.Vessels.Commands.UpdateVessel;

public record UpdateVesselCommand
    (Guid Id, string Name, string IMO, string Type, decimal Capacity)
    : IRequestWrapper<VesselDto>
{
    [JsonIgnore]
    public Guid Id { get; set; } = Id;
}

public class UpdateVesselCommandValidator : AbstractValidator<UpdateVesselCommand>
{
    public UpdateVesselCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.IMO).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Type).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Capacity).GreaterThanOrEqualTo(0);
    }
}
