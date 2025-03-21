using FluentValidation;
using Vesma.Contracts.Vessels;
using Vesma.Core.Base;

namespace Vesma.Core.Handlers.Vessels.Queries.GetVesselById;

public record GetVesselByIdQuery(Guid Id) : IRequestWrapper<VesselDto>;

public class GetVesselByIdQueryValidator : AbstractValidator<GetVesselByIdQuery>
{
    public GetVesselByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}