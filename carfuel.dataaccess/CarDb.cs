using System;
using System.Collections.Generic;
using System.Linq; 
using CarFuel.Models;

namespace CarFuel.DataAccess {
  public class CarDb : ICarDb {

    private CarFuelDb db = new CarFuelDb();

    public Car Add(Car item) {
      db.Cars.Add(item);
      db.SaveChanges();
      return item;
    }

    public Car Get(Guid id) {
      return db.Cars.Find(id);
    }

    public IEnumerable<Car> GetAll(Func<Car, bool> predicate) {
      return db.Cars.Where(predicate).AsEnumerable();
    }
  }
}
