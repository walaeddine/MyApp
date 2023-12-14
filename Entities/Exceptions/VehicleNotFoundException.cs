namespace Entities.Exceptions;

public class VehicleNotFoundException : NotFoundException
{
    public VehicleNotFoundException(Guid vehicleId) : base($"Vehicle with id: {vehicleId} doesn't exist in the database.")
    {
    }
}