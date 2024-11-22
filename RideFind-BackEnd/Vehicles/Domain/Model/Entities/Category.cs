
using RideFind_BackEnd.Vehicles.Domain.Model.Aggregates;

namespace RideFind_BackEnd.Vehicles.Domain.Model.Entities;

public class Category
{
    public Category()
    {
        Name = string.Empty;
    }

    public Category(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Vehicle> Tutorials { get; }
}