using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.Models;
using Xunit;
using Should;

namespace CarFuel.Facts {
  public class CarFacts {

    public class General {

      [Fact]
      public void NewCar() {
        var c = new Car();

        c.Make = "Honda";
        c.Model = "Accord";

        Assert.NotNull(c.FillUps);
        Assert.Equal(0, c.FillUps.Count);
      }

    }


    public class AddFillUpMethod {

      [Fact]
      public void SingleFillUp() {
        var c = new Car();

        FillUp f = c.AddFillUp(1000, 40);

        Assert.NotNull(f);
        Assert.Equal(1000, f.Odometer);
        Assert.Equal(40.0, f.Liters);
        Assert.Null(f.NextFillUp);

        Assert.Equal(1, c.FillUps.Count);
      }

      [Fact]
      public void TwoFillUps() {
        var c = new Car();

        FillUp f1 = c.AddFillUp(1000, 40);
        FillUp f2 = c.AddFillUp(1600, 50);

        f1.NextFillUp.ShouldBeSameAs(f2);
        Assert.Equal(2, c.FillUps.Count);
      }
    }

    public class AverageKmLProperty {

      [Fact]
      public void NewCar_HasNoValue() {
        var c = new Car();

        Assert.Null(c.AverageKmL);
      }

      [Fact]
      public void SingleFillUp_HasNoValue() {
        var c = new Car();
        c.AddFillUp(1000, 40);
        Assert.Null(c.AverageKmL);
      }

      [Fact]
      public void TwoFillUps_SameAsFirstFillUp() {
        var c = new Car();
        c.AddFillUp(1000, 40);
        c.AddFillUp(1600, 50);
        Assert.Equal(12.0, c.AverageKmL);
      }

      [Fact]
      public void ThreeFillUps() {
        var c = new Car();
        c.AddFillUp(1000, 40);
        c.AddFillUp(1600, 50);
        c.AddFillUp(2000, 40);
        Assert.Equal(11.11, c.AverageKmL);
      }
    }
  }
}
