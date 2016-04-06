using System;
using System.Collections.Generic;
using System.Linq; 
using CarFuel.Models;

namespace CarFuel.DataAccess {
  public class FakeCarDb : ICarDb {

    private ICollection<Car> cars;

    public FakeCarDb() {
      cars = new HashSet<Car>();
    }

    public Car Add(Car item) {
      cars.Add(item);
      return item;
    }

    public Car Get(Guid id) {
      return cars.SingleOrDefault(x => x.Id == id);
    }

    public IEnumerable<Car> GetAll(Func<Car, bool> predicate) {
      return cars.Where(predicate).AsEnumerable();
    }
  }
}
