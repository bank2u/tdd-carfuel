using System.Data.Entity;
using CarFuel.Models;

namespace CarFuel.DataAccess {
  public class CarFuelDb : DbContext {

    public DbSet<Car> Cars { get; set; }

  }
}
