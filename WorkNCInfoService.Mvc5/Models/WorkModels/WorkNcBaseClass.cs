using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    public class WorkNcBaseClass
    {
        [Display(Name = "Create Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Create Account")]
        public string CreateAccount { get; set; }

        [Display(Name = "Modified Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Modified Account")]
        public string ModifiedAccount { get; set; }
    }
}
