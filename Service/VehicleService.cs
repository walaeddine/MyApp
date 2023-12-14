using AutoMapper;
using Contract;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class VehicleService : IVehicleService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public VehicleService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
    }

    private async Task<Vehicle> GetVehicleForCompanyAndCheckIfItExists
        (Guid companyId, Guid id, bool trackChanges)
    {
        var vehicleDb = await _repository.Vehicle.GetVehicleAsync(companyId, id, trackChanges);
        if (vehicleDb is null)
            throw new VehicleNotFoundException(id);

        return vehicleDb;
    }

    public async Task<(IEnumerable<VehicleDto> vehicles, MetaData metaData)> GetVehiclesAsync(
        Guid companyId, VehicleParameters vehicleParameters, bool trackChanges)
    {
        if (!vehicleParameters.ValidAgeRange)
            throw new MaxAgeRangeBadRequestException();

        await CheckIfCompanyExists(companyId, trackChanges);
        var vehiclesWithMetaData = await _repository.Vehicle.GetVehiclesAsync(
            companyId, vehicleParameters, trackChanges);

        var vehiclesDto = _mapper.Map<IEnumerable<VehicleDto>>(vehiclesWithMetaData);
        return (vehicles: vehiclesDto, metaData: vehiclesWithMetaData.MetaData);
    }

    public async Task<VehicleDto> GetVehicleAsync(Guid companyId, Guid id, bool trackChanges)
    {
        await CheckIfCompanyExists(companyId, trackChanges);
        var vehicleDb = await GetVehicleForCompanyAndCheckIfItExists(companyId, id, trackChanges);
        var vehicle = _mapper.Map<VehicleDto>(vehicleDb);
        return vehicle;
    }

    public async Task<VehicleDto> CreateVehicleForCompanyAsync(Guid companyId,
        VehicleForCreationDto vehicleForCreation, bool trackChanges)
    {
        await CheckIfCompanyExists(companyId, trackChanges);
        var vehicleEntity = _mapper.Map<Vehicle>(vehicleForCreation);

        _repository.Vehicle.CreateVehicleForCompany(companyId, vehicleEntity);
        await _repository.SaveAsync();

        var vehicleToReturn = _mapper.Map<VehicleDto>(vehicleEntity);

        return vehicleToReturn;
    }

    public async Task UpdateVehicleForCompanyAsync(Guid companyId, Guid id, VehicleForUpdateDto vehicleForUpdate,
    bool compTrackChanges, bool vehTrackChanges)
    {
        await CheckIfCompanyExists(companyId, compTrackChanges);
        var vehicleEntity = await GetVehicleForCompanyAndCheckIfItExists(companyId, id, vehTrackChanges);
        _mapper.Map(vehicleForUpdate, vehicleEntity);
        await _repository.SaveAsync();
    }

    public async Task DeleteVehicleForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
    {
        await CheckIfCompanyExists(companyId, trackChanges);
        var vehicleForCompany = await GetVehicleForCompanyAndCheckIfItExists(companyId, id, trackChanges);
        _repository.Vehicle.DeleteVehicle(vehicleForCompany);
        await _repository.SaveAsync();
    }
}
