using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

internal sealed class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
{
    public VehicleRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<PagedList<Vehicle>> GetVehiclesAsync(Guid companyId, VehicleParameters vehicleParameters,
        bool trackChanges)
    {
        var vehicles = await FindByCondition(e => e.CompanyId.Equals(companyId),trackChanges)
            .FilterVehicles(vehicleParameters.MinAge, vehicleParameters.MaxAge)
            .Search(vehicleParameters.SearchTerm)
            .Sort(vehicleParameters.OrderBy)
            .Skip((vehicleParameters.PageNumber - 1) * vehicleParameters.PageSize)
            .Take(vehicleParameters.PageSize)
            .ToListAsync();

        var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .CountAsync();

        return new PagedList<Vehicle>(vehicles, count, vehicleParameters.PageNumber, vehicleParameters.PageSize);
    }

    public async Task<Vehicle> GetVehicleAsync(Guid companyId, Guid id, bool trackChanges) =>
        await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
        .SingleOrDefaultAsync();

    public void CreateVehicleForCompany(Guid companyId, Vehicle vehicle)
    {
        vehicle.CompanyId = companyId;
        Create(vehicle);
    }

    public void DeleteVehicle(Vehicle vehicle) => Delete(vehicle);
}
