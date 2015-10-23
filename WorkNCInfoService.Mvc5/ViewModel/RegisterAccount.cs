using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.ViewModel
{
    public class RegisterAccount
    {
        [StringLength(20)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Pre Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PrePassword { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [StringLength(50)]
        [Display(Name = "Web Permission")]
        public string WebPermission { get; set; }

        [Required]
        [Display(Name = "App Permission")]
        public bool AppPermission { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }


        [StringLength(50)]
        public string CreateAccount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }


        [StringLength(50)]
        public string ModifiedAccount { get; set; }
    }
}
