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
        public string Username { get; set; }

        public int CompanyId { get; set; }

        [StringLength(50)]
        public string WebPermission { get; set; }

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
