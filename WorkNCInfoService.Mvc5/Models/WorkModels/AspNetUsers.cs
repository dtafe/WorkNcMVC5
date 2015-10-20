namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class AspNetUsers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(128)]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
