using System;
using System.Collections.Generic; 
using CarFuel.Models;

namespace CarFuel.DataAccess {
  public interface ICarDb {

    IEnumerable<Car> GetAll(Func<Car, bool> predicate);
    Car Get(Guid id);
    Car Add(Car item);

  }
}
