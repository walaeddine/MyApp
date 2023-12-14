using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IVehicleRepository
{
    Task<PagedList<Vehicle>> GetVehiclesAsync(Guid companyId, VehicleParameters vehicleParameters, bool trackChanges);
    Task<Vehicle?> GetVehicleAsync(Guid companyId, Guid id, bool trackChanges);
    void CreateVehicleForCompany(Guid companyId, Vehicle vehicle);
    void DeleteVehicle(Vehicle vehicle);
}