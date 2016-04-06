using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFuel.Models {
  public class FillUp {

    public FillUp() {
      //
    }

    public FillUp(int odometer, double liters) {
      Odometer = odometer;
      Liters = liters;
    }

    public int Id { get; set; }

    public double Liters { get; set; }
    public int Odometer { get; set; }
    public virtual FillUp NextFillUp { get; set; }

    public double? KmL {
      get {
        if (NextFillUp == null) {
          return null;
        }

        return (NextFillUp.Odometer - Odometer) / NextFillUp.Liters;
      }
    }
  }
}
