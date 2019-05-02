using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RideServiceGroup2.Entities
{
    public enum Status
    {
        [Display(Name = "Virker")]
        Working = 1,
        [Display(Name = "Virker ikke")]
        Broken = 2,
        [Display(Name = "Under reperation")]
        BeingRepaired = 3
    }
}
