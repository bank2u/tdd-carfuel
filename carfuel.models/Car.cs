using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CarFuel.Models {
  public class Car {

    public Car() {
      FillUps = new HashSet<FillUp>();
    }


    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public virtual ICollection<FillUp> FillUps { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }

    public FillUp AddFillUp(int odometer, double liters) {
      var f = new FillUp(odometer, liters);

      var lastOne = FillUps.SingleOrDefault(x => x.NextFillUp == null);
      if (lastOne != null) {
        lastOne.NextFillUp = f;
      }

      FillUps.Add(f);

      return f;
    }

    public double? AverageKmL {
      get {
        if (FillUps.Count >= 2) {
          var distance = FillUps.Last().Odometer - FillUps.First().Odometer;
          var totalLiters = FillUps.Sum(x => x.Liters) - FillUps.First().Liters;
          return Math.Round(distance / totalLiters, 2, MidpointRounding.AwayFromZero);
        }
        return null;
      }
    }

  }
}