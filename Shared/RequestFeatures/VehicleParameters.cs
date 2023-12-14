namespace Shared.RequestFeatures;

public class VehicleParameters : RequestParameters
{
    public VehicleParameters() => OrderBy = "chassisno";
    public uint MinAge { get; set; }
    public uint MaxAge { get; set; } = int.MaxValue;
    public bool ValidAgeRange => MaxAge > MinAge;
    public string? SearchTerm { get; set; }
}