using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [BindProperty]
        [Display(Name = "Navn på kategori")]
        public string CategoryName { get; set; }
        [BindProperty]
        [Display(Name = "Beskrivelse")]
        public string CategoryDescription { get; set; }
        public bool NameUsed = false;
        [BindProperty]
        public string Message { get; set; } = "";


        public void OnGet()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            Categories = categoryRepo.GetAllRideCategories();
            RideRepository rideRepo = new RideRepository();
            Rides = rideRepo.GetRidesBasedOnCategory(Name);
        }


        public void OnPost()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            Categories = categoryRepo.GetAllRideCategories();

            if (!string.IsNullOrWhiteSpace(CategoryName))
            {
                if (!string.IsNullOrWhiteSpace(CategoryDescription))
                {
                    RideCategory newCategory = new RideCategory
                    {
                        Name = CategoryName,
                        Description = CategoryDescription
                    };

                    foreach (RideCategory rideCategory in Categories)
                    {
                        if (rideCategory.Name == newCategory.Name)
                        {
                            NameUsed = true;
                        }
                    }

                    if (!NameUsed)
                    {
                        categoryRepo.AddCategory(newCategory);
                        Message = "Kategori oprettet";
                    }
                    else
                    {
                        Message = "Kategorinavnet er allerede i brug, og blev derfor ikke oprettet";
                    }

                }
                else
                {
                    Message = "Beskrivelsen skal have en værdi";
                }
            }
            else
            {
                Message = "Navn skal have en værdi";
            }
        }
    }
}