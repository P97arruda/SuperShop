using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class ChangerUserViewModel
    {
        [Required]
        [Display(Name = "Fist Name")]
        public string FistName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
