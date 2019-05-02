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
    public class CategoriesModel : PageModel
    {
        public List<RideCategory> Categories { get; set; } = new List<RideCategory>();
        public List<Ride> Rides { get; set; } = new List<Ride>();
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        public void OnGet()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            Categories = categoryRepo.GetAllRideCategories();
            RideRepository rideRepo = new RideRepository();
            Rides = rideRepo.GetRidesBasedOnCategory(Name);
        }

    }
}