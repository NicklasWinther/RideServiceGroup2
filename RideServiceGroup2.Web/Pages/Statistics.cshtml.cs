using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RideServiceGroup2.DAL;
using RideServiceGroup2.Entities;

namespace RideServiceGroup2.Web.Pages
{
    public class StatisticsModel : PageModel
    {
        public Ride LatestBroken { get; set; }
        public Ride MostBroken { get; set; }
        public int TimesBroken { get; set; }
        public void OnGet()
        {
            RideRepository rideRepo = new RideRepository();
            LatestBroken = rideRepo.GetLatestBrokenRide();
            (MostBroken, TimesBroken) = rideRepo.GetMostBrokenRide();
        }
    }
}