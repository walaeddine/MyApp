using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Api.Controllers;

[Route("api/companies/{companyId}/vehicles")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IServiceManager _service;

    public VehiclesController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetVehiclesForCompany(Guid companyId, [FromQuery] VehicleParameters vehicleParameters)
    {
        var pagedResult = await _service.VehicleService.GetVehiclesAsync(
            companyId, vehicleParameters, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

        return Ok(pagedResult.vehicles);
    }

    [HttpGet("{id:guid}", Name = "GetVehicleForCompany")]
    public async Task<IActionResult> GetVehicleForCompany(Guid companyId, Guid id)
    {
        var vehicle = await _service.VehicleService.GetVehicleAsync(companyId, id, trackChanges: false);
        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehiclesForCompany
        (Guid companyId, [FromBody] VehicleForCreationDto vehicle)
    {
        if (vehicle is null)
            return BadRequest("VehicleForCreationDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var vehicleToReturn = await _service.VehicleService.CreateVehicleForCompanyAsync(companyId, vehicle, trackChanges: false);

        return CreatedAtRoute("GetVehicleForCompany", new { companyId, id = vehicleToReturn.Id }, vehicleToReturn);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteVehicleForCompany(Guid companyId, Guid id)
    {
        await _service.VehicleService.DeleteVehicleForCompanyAsync(companyId, id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateVehicleForCompany(Guid companyId, Guid id,
        [FromBody] VehicleForUpdateDto vehicle)
    {
        if (vehicle is null)
            return BadRequest("VehicleForUpdateDto object is null");

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _service.VehicleService.UpdateVehicleForCompanyAsync(
            companyId, id, vehicle, compTrackChanges: false, vehTrackChanges: true);

        return NoContent();
    }

}
