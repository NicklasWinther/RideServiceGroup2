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
    public class CreateRideModel : PageModel
    {
        [BindProperty]
        public Ride Ride { get; set; } = new Ride();
        public List<RideCategory> Categories { get; set; }
        public string PageHandler { get; set; } = "";
        public string SubmitValue { get; set; } = "Opret";
        public string HeaderText { get; set; } = "Opret forlystelse";
        public string CategoryValue { get; set; } = "";

        public CreateRideModel()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            Categories = categoryRepo.GetAllRideCategories();
        }
        public void OnGet()
        {
        }

        public void OnGetEdit(int id)
        {
            RideRepository rideRepo = new RideRepository();
            Ride = rideRepo.GetById(id);
            PageHandler = "Edit";
            SubmitValue = "Redigér";
            HeaderText = "Rediger forlystelse";
            CategoryValue = Ride.Category.Name;
        }

        public void OnPost()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            Ride = new Ride()
            {
                Name = Ride.Name,
                ImgUrl = Ride.ImgUrl,
                Description = Ride.Description
            };
        }

        public void OnPostEdit()
        {
            RideRepository rideRepository = new RideRepository();
            rideRepository.Update(Ride);
        }
    }
}