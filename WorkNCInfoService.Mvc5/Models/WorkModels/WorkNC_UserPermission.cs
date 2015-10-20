namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkNC_UserPermission
    {
        [Key]
        [StringLength(20)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Web Permission")]
        public string WebPermission { get; set; }


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
