using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record VehicleForManipulationDto
{
    [Required(ErrorMessage = "Employee name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string? ChassisNo { get; init; }
    [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
    public int Age { get; init; }
}