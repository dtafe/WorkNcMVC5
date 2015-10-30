namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("WorkNC_Factory")]
    public partial class WorkNC_Factory : WorkNcBaseClass
    {
        [Key]
        public int FactoryId { get; set; }

        public int CompanyId { get; set; }

        [StringLength(50)]
        public string No { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool isDeleted { get; set; }

        [StringLength(250)]
        public string ImageFile { get; set; }
    }
}
