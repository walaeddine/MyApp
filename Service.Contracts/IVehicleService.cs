using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IVehicleService
{
    Task<(IEnumerable<VehicleDto> vehicles, MetaData metaData)> GetVehiclesAsync(
            Guid companyId, VehicleParameters vehicleParameters, bool trackChanges);
    Task<VehicleDto> GetVehicleAsync(Guid companyId, Guid id, bool trackChanges);
    Task<VehicleDto> CreateVehicleForCompanyAsync(Guid companyId,
        VehicleForCreationDto vehicleForCreation, bool trackChanges);
    Task UpdateVehicleForCompanyAsync(Guid companyId, Guid id,
        VehicleForUpdateDto vehicleForUpdate, bool compTrackChanges, bool vehTrackChanges);
    Task DeleteVehicleForCompanyAsync(Guid companyId, Guid id, bool trackChanges);
}
