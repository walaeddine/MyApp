using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class RepositoryVehicleExtensions
{
    public static IQueryable<Vehicle> FilterVehicles(this IQueryable<Vehicle> vehicles, uint minAge, uint maxAge) =>
        vehicles.Where(e => (e.Age >= minAge && e.Age <= maxAge));

    public static IQueryable<Vehicle> Search(this IQueryable<Vehicle> vehicles, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return vehicles;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return vehicles.Where(e => e.ChassisNo.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Vehicle> Sort(this IQueryable<Vehicle> vehicles, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return vehicles.OrderBy(e => e.ChassisNo);

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Vehicle>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return vehicles.OrderBy(e => e.ChassisNo);

        return vehicles.OrderBy(orderQuery);
    }
}