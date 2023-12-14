using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Vehicle
{
    [Column("VehicleId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Chassis number is a required field.")]
    [MaxLength(9, ErrorMessage = "Maximum length for the Chassis Number is 9 characters.")]
    public string? ChassisNo { get; set; }
    [Required(ErrorMessage = "Age is a required field.")]
    public int Age { get; set; }
    
    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; }
}