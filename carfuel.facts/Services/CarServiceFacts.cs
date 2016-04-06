using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarFuel.DataAccess;
using CarFuel.Models;
using CarFuel.Services;
using Xunit;
using System.Web.Mvc;
using Xunit.Abstractions;

namespace CarFuel.Facts.Services
{
    public class SharedService
    {
        public CarService CarService { get; set; }
        public SharedService()
        {
            CarService = new CarService(new FakeCarDb());
        }
    }

    [CollectionDefinition("collection1")]
    public class CarServiceFactCollection : ICollectionFixture<SharedService>
    {
        //
    }

    public class CarServiceFacts
    {
        [Collection("collection1")]
        public class AddCarMethod
        {
            private CarService s;
            private ITestOutputHelper ouput;


            public AddCarMethod(ITestOutputHelper output, SharedService service)
            {
                s = service.CarService;
                this.ouput = output;

                output.WriteLine("ctor");
            }

            [Fact]
            public void AddSingleCar()
            {
                var c = new Car();
                c.Make = "Honda";
                c.Model = "Civic";
                var userId = Guid.NewGuid();

                var c2 = s.AddCar(c, userId);

                Assert.NotNull(c2);
                Assert.Equal(c2.Make, c.Make);
                Assert.Equal(c2.Model, c.Model);

                var cars = s.GetCarsByMember(userId);

                Assert.Equal(1, cars.Count());
                Assert.Contains(cars, x => x.OwnerId == userId);
            }
        }

        [Collection("collection1")]
        public class GetCarsByMemberMethod
        {
            private CarService s;

            public GetCarsByMemberMethod(SharedService service)
            {
                s = service.CarService;
            }

            [Fact]
            public void MemberCanGetOnlyHisOrHerOwnCar()
            {
                var member1_Id = Guid.NewGuid();
                var member2_Id = Guid.NewGuid();
                var member3_Id = Guid.NewGuid();

                s.AddCar(new Car(), member1_Id);
                s.AddCar(new Car(), member1_Id);

                s.AddCar(new Car(), member2_Id);
                s.AddCar(new Car(), member2_Id);

                Assert.Equal(2, s.GetCarsByMember(member1_Id).Count());
                Assert.Equal(2, s.GetCarsByMember(member2_Id).Count());
                Assert.Equal(0, s.GetCarsByMember(member3_Id).Count());
            }

            [Fact]
            public void AddThreeCars_ThrowException()
            {
                var memberId = Guid.NewGuid();

                s.AddCar(new Car(), memberId);
                s.AddCar(new Car(), memberId);
                var ex = Assert.Throws<OverQuotaException>(() =>
                {
                    s.AddCar(new Car(), memberId);
                });

                Assert.Equal("Cannot add more car.", ex.Message);
            }
        }

        [Collection("collection1")]
        public class CanAddMoreCarsMethod
        {
            private CarService s;

            public CanAddMoreCarsMethod(SharedService service)
            {
                s = service.CarService;
            }

            [Fact]
            public void MemberCanAddNotMoreThanTwoCar()
            {
                var member1_Id = Guid.NewGuid();

                Assert.True(s.CanAddMoreCars(member1_Id));

                // Have 1 Car
                s.AddCar(new Car(), member1_Id);
                Assert.True(s.CanAddMoreCars(member1_Id));

                // Have 2 Cars
                s.AddCar(new Car(), member1_Id);
                Assert.False(s.CanAddMoreCars(member1_Id));
            }
        }

    }
}
