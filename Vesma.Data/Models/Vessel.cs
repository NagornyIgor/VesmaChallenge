namespace Vesma.Data.Models;

public class Vessel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public Guid ImoId { get; set; }

    public IMO? Imo { get; set; }

    public required string Type { get; set; }

    public decimal Capacity { get; set; }
}
