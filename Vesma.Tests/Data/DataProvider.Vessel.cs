using Vesma.Core.Handlers.Vessels.Commands.RegisterVessel;
using Vesma.Core.Handlers.Vessels.Commands.UpdateVessel;

namespace Vesma.Tests.Data;

internal static partial class DataProvider
{
    public static class Vessel
    {
        public static RegisterVesselCommand GenerateRegisterVesselCommand(
            string? name = null,
            string? imo = null,
            string? type = null,
            decimal? capacity = null)
        {
            var generator = DataGenerator;

            return new(
                Name: generator.Random.Word(),
                IMO: generator.Company.CompanyName(),
                Type: generator.Random.Word(),
                Capacity: capacity ?? generator.Random.Decimal(max: 1000));
        }


        public static UpdateVesselCommand GenerateUpdateVesselCommand(
            Guid id,
            string? name = null,
            string? imo = null,
            string? type = null,
            decimal? capacity = null)
        {
            var generator = DataGenerator;

            return new(
                Id: id,
                Name: generator.Random.Word(),
                IMO: generator.Company.CompanyName(),
                Type: generator.Random.Word(),
                Capacity: capacity ?? generator.Random.Decimal(max: 1000));
        }

        //public static UpdateVesselCommand GenerateUpdateVesselCommand(
        //    Guid id,
        //    string? name = null,
        //    string? imo = null,
        //    string? type = null,
        //    decimal? capacity = null)
        //{
        //    var generator = DataGenerator;

        //    return new()
        //    {
        //        Id = id,
        //        Name = generator.Random.Word(),
        //        IMO = generator.Company.CompanyName(),
        //        Type = generator.Random.Word(),
        //        Capacity = capacity ?? generator.Random.Decimal(max: 1000),
        //    };
        //}
    }
}
