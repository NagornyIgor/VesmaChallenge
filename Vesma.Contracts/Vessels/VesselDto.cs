namespace Vesma.Contracts.Vessels;

public class VesselDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public required string IMO { get; set; }

    public required string Type { get; set; }

    public decimal Capacity { get; set; }
}
