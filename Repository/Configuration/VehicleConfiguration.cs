using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasData
        (
            new Vehicle
            {
                Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                ChassisNo = "123456789",
                CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Vehicle
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                ChassisNo = "987654321",
                CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Vehicle
            {
                Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                ChassisNo = "001122334",
                CompanyId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
            }
        );
    }
}