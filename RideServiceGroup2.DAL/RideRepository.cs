using RideServiceGroup2.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RideServiceGroup2.DAL
{
    public class RideRepository : BaseRepository
    {
        public List<Ride> GetAllRides()
        {
            List<Ride> rides = new List<Ride>();
            string sql = $"SELECT * FROM Rides";
            DataTable ridesTable = ExecuteQuery(sql);
            CategoryRepository categoryRepo = new CategoryRepository();
            foreach (DataRow row in ridesTable.Rows)
            {
                int id = (int)row["RideId"];
                string name = (string)row["Name"];
                string imgUrl = (string)row["ImgUrl"];
                string description = (string)row["Description"];
                int categoryId = (int)row["CategoryId"];

                Ride ride = new Ride()
                {
                    Id = id,
                    Name = name,
                    ImgUrl = imgUrl,
                    Description = description,
                    Category = categoryRepo.GetCategory(categoryId)
                };
                rides.Add(ride);
            }
            ReportRepository reportRepository = new ReportRepository();
            foreach (Ride ride in rides)
            {
                ride.Reports = reportRepository.GetReportsFor(ride.Id);
            }

            return rides;
        }

        public (Ride ride, int timesBroken) GetMostBrokenRide()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();
            string sql = $"SELECT TOP(1) COUNT(status) AS TimesBroken, Rides.Name, Rides.RideId, ImgUrl, Description, CategoryId FROM Rides JOIN Reports ON Reports.RideId = Rides.RideId WHERE Status = 2 GROUP BY Rides.Name, Rides.RideId, ImgUrl, Description, CategoryId ";

            DataTable rideTable = ExecuteQuery(sql);
            DataRow row = rideTable.Rows[0];
            int timesBroken = (int)row["TimesBroken"];
            Ride ride = new Ride()
            {
                Id = (int)row["RideId"],
                Name = (string)row["Name"],
                ImgUrl = (string)row["ImgUrl"],
                Description = (string)row["Description"],
                Category = categoryRepo.GetCategory((int)row["CategoryId"]),
                Reports = reportRepository.GetReportsFor((int)row["RideId"])
            };
            return (ride, timesBroken);
        }

        public Ride GetLatestBrokenRide()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();
            string sql = "SELECT TOP(1) * FROM Reports JOIN Rides ON Reports.RideId = Rides.RideId WHERE Status = 2 ORDER BY ReportTime DESC";
            DataTable rideTable = ExecuteQuery(sql);
            DataRow row = rideTable.Rows[0];
            Ride ride = new Ride()
            {
                Id = (int)row["RideId"],
                Name = (string)row["Name"],
                ImgUrl = (string)row["ImgUrl"],
                Description = (string)row["Description"],
                Category = categoryRepo.GetCategory((int)row["CategoryId"]),
                Reports = reportRepository.GetReportsFor((int)row["RideId"])
            };
            return ride;
        }

        public Ride GetById(int id)
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            ReportRepository reportRepository = new ReportRepository();
            string sql = $"SELECT * FROM Rides WHERE RideId = {id}";
            DataTable rideTable = ExecuteQuery(sql);
            DataRow row = rideTable.Rows[0];
            Ride ride = new Ride()
            {
                Id = (int)row["RideId"],
                Name = (string)row["Name"],
                ImgUrl = (string)row["ImgUrl"],
                Description = (string)row["Description"],
                Category = categoryRepo.GetCategory((int)row["CategoryId"]),
                Reports = reportRepository.GetReportsFor((int)row["RideId"])
            };
            return ride;
        }

        public void Update(Ride ride)
        {
            string sql = $"UPDATE Rides SET Name = '{ride.Name}', ImgUrl = '{ride.ImgUrl}', Description = '{ride.Description}', CategoryId = '{ride.Category.Id}' WHERE RideId = {ride.Id}";
            ExecuteNonQuery(sql);
        }

        public List<Ride> GetRidesBasedOnCategory(string categoryName)
        {
            string sql = $"SELECT * FROM Rides JOIN RideCategories ON RideCategories.RideCategoryId = Rides.CategoryId WHERE RideCategories.Name = '{categoryName}'";
            DataTable ridesTable = ExecuteQuery(sql);
            List<Ride> rides = new List<Ride>();
            CategoryRepository categoryRepo = new CategoryRepository();

            foreach (DataRow row in ridesTable.Rows)
            {
                int id = (int)row["RideId"];
                string name = (string)row["Name"];
                string imgUrl = (string)row["ImgUrl"];
                string description = (string)row["Description"];
                int categoryId = (int)row["CategoryId"];

                Ride ride = new Ride()
                {
                    Id = id,
                    Name = name,
                    ImgUrl = imgUrl,
                    Description = description,
                    Category = categoryRepo.GetCategory(categoryId)
                };
                rides.Add(ride);

            }
            ReportRepository reportRepository = new ReportRepository();
            foreach (Ride ride in rides)
            {
                ride.Reports = reportRepository.GetReportsFor(ride.Id);
            }
            return rides;
        }
    }
}
