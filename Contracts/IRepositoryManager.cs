namespace Contracts;

public interface IRepositoryManager
{
    ICompanyRepository Company { get; }
    IEmployeeRepository Employee { get; }
    IVehicleRepository Vehicle { get; }
    Task SaveAsync();
}
