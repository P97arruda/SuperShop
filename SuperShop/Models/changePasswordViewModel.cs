using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class changePasswordViewModel
    {
        [Required]
        [Display(Name = "Current password")]
        public string OldPassWord { get; set; }

        [Required]
        [Display(Name = "New password")]
        public string NewPassWord { get; set; }

        [Required]
        [Compare("NewPassWord")]
        public string Confirm { get; set; }
    }
}
