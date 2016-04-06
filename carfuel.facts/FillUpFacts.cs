using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.Models;
using Xunit;
using Should;

namespace CarFuel.Facts {
  public class FillUpFacts {

    public class General {

      [Fact]
      public void BasicUsage() {
        var f = new FillUp();
        f.Odometer = 1000;
        f.Liters = 40.0;

        Assert.Equal(1000, f.Odometer);
      }

      [Fact]
      public void SingleFillUp() {
        var f = new FillUp();
        f.Odometer = 1000;
        f.Liters = 40.0;

        double? kml = f.KmL;

        Assert.Null(kml);
      }

      [Fact]
      public void TwoFillUps() {
        var f1 = new FillUp();
        f1.Odometer = 1000;
        f1.Liters = 40.0;

        var f2 = new FillUp();
        f2.Odometer = 1600;
        f2.Liters = 50.0;

        f1.NextFillUp = f2; // manual wire-up.

        double? kml_1 = f1.KmL;
        double? kml_2 = f2.KmL;

        Assert.Equal(12.0, kml_1);
        Assert.Null(kml_2);
      }      
    }
  }
}
