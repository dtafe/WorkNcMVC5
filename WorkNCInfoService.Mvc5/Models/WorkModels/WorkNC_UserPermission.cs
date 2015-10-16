namespace MVCtest.Models.WorkModels
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

        [Required]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Pre Password")]
        public string PrePassword { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Web Permission")]
        public string WebPermission { get; set; }

        [Required]
        [Display(Name = "App Permission")]
        public bool AppPermission { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateAccount { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string ModifiedAccount { get; set; }
    }
}
