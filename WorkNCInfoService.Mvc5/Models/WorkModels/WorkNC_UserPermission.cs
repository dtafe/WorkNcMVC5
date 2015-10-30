namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WorkNC_UserPermission: WorkNcBaseClass
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

    }
}
