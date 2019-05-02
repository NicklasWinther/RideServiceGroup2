using RideServiceGroup2.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace RideServiceGroup2.EntitiesTest
{
    public class RideTest
    {
        private Ride CreateCorrectRide()
        {
            Ride ride = new Ride();
            return ride;
        }

        private Ride CreateCorrectRideWithNoBrokenReports()
        {
            Ride ride = new Ride
            {
                Reports = CreateWorkingReport()
            };
            return ride;
        }

        private Ride CreateCorrectRideWithOneBrokenReport()
        {
            Ride ride = new Ride
            {
                Reports = CreateBrokenReport()
            };
            return ride;
        }

        private Ride CreateCorrectRideWithMultipleBrokenReports(int numberOfReports)
        {
            Ride ride = new Ride
            {
                Reports = CreateBrokenReports(numberOfReports)
            };
            return ride;
        }

        private Ride CreateCorrectRideWithOtherThanBrokenReports()
        {
            Ride ride = new Ride
            {
                Reports = CreateOtherReportsThanBroken()
            };

            return ride;
        }

     //Working reports

        private List<Report> CreateWorkingReport()
        {
            List<Report> reports = new List<Report>();
            Report report = new Report
            {
                Status = Status.Working,
                ReportTime = DateTime.Today
            };
            reports.Add(report);
            return reports;
        }
        private List<Report> CreateOtherReportsThanBroken()
        {
            List<Report> reports = new List<Report>();

            Report workingReport = new Report
            {
                Status = Status.Working
            };
            Report beingRepairedReport = new Report
            {
                Status = Status.BeingRepaired
            };
            reports.Add(workingReport);
            reports.Add(beingRepairedReport);

            return reports;
        }

    //Broken reports

        private List<Report> CreateBrokenReport()
        {
            List<Report> reports = new List<Report>();
            Report report = new Report
            {
                Status = Status.Broken,
                ReportTime = DateTime.Today
            };
            reports.Add(report);
            return reports;
        }

        private List<Report> CreateBrokenReports(int numberOfReports)
        {
            List<Report> reports = new List<Report>();
            for (int i = 0; i < numberOfReports; i++)
            {
                Report report = new Report
                {
                    Status = Status.Broken,
                    ReportTime = DateTime.Today.AddDays(-i)
                };
                reports.Add(report);
            }
            
            return reports;
        }

    //TESTS
    //DAYS SINCE LAST SHUTDOWN

        [Fact]
        public void DaysSinceLastShutdown_ReturnsHighNumberWithNoReports()
        {
            //Arrange
            Ride ride = CreateCorrectRide();

            //Act
            int daysSinceLastShutdown = ride.DaysSinceLastShutdown();

            //Assert
            Assert.Equal(737179, daysSinceLastShutdown);
        }

        [Fact]
        public void DaysSinceLastShutdown_ReturnsHighNumberWithNoBrokenReports()
        {
            //Arrange
            Ride ride = CreateCorrectRideWithNoBrokenReports();

            //Act
            int daysSinseLastShutdown = ride.DaysSinceLastShutdown();

            //Assert
            Assert.Equal(737179, daysSinseLastShutdown);
        }

        [Fact]
        public void DaysSinceLastShutdown_ReturnsCorrectWithOneBrokenReport()
        {
            //Arrange
            Ride ride = CreateCorrectRideWithOneBrokenReport();

            //Act
            int daysSinceLastShutdown = ride.DaysSinceLastShutdown();

            //Assert
            Assert.Equal(0, daysSinceLastShutdown);
        }

        [Fact]
        public void DaysSinceLastShutdown_ReturnsCorrectWithMultipleBrokenReports()
        {
            //Arrange
            int amount = 2;
            Ride ride = CreateCorrectRideWithMultipleBrokenReports(amount);

            //Act
            int daysSinceLastShutDown = ride.DaysSinceLastShutdown();

            //Assert
            Assert.Equal(0, daysSinceLastShutDown);
        }

    //NUMBER OF SHUTDOWNS

        [Fact]
        public void NumberOfShutDowns_ReturnsZeroWithNoReports()
        {
            //Arrange
            Ride ride = CreateCorrectRide();

            //Act
            int numberOfShutDowns = ride.NumberOfShutdowns();

            //Assert
            Assert.Equal(0, numberOfShutDowns);
        }

        [Fact]
        public void NumberOfShutDowns_ReturnsZeroWithNoBrokenReports()
        {
            //Arrange
            Ride ride = CreateCorrectRideWithOtherThanBrokenReports();
            //Act
            int numberOfShutdowns = ride.NumberOfShutdowns();

            //Assert
            Assert.Equal(0, numberOfShutdowns);
        }

        [Fact]
        public void NumberOfShutdowns_ReturnsCorrectValueWithBrokenReports()
        {
            //Arrange
            int numberOfReports = 2;
            Ride ride = CreateCorrectRideWithMultipleBrokenReports(numberOfReports);

            //Act
            int numberOfShutDowns = ride.NumberOfShutdowns();

            //Assert
            Assert.Equal(numberOfReports, numberOfShutDowns);
        }



    }
}
