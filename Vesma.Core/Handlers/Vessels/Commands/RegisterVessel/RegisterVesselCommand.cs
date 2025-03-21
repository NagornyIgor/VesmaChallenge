using FluentValidation;
using Vesma.Contracts.Vessels;
using Vesma.Core.Base;

namespace Vesma.Core.Handlers.Vessels.Commands.RegisterVessel;

public record RegisterVesselCommand
    (string Name, string IMO, string Type, decimal Capacity)
    : IRequestWrapper<VesselDto>;

public class RegisterVesselCommandValidator : AbstractValidator<RegisterVesselCommand>
{
    public RegisterVesselCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.IMO).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Type).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Capacity).GreaterThanOrEqualTo(0);
    }
}
