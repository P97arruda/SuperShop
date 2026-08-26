using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Entities
{
    public class User : IdentityUser
    {
        public string FistName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FistName} {LastName}";
    }
}
