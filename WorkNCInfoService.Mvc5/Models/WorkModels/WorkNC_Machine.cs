namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("WorkNC_Machine")]
    public partial class WorkNC_Machine : WorkNcBaseClass
    {
        [Key]
        public int MachineId { get; set; }

        public int CompanyId { get; set; }

        public int FactoryId { get; set; }

        [StringLength(50)]
        public string No { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool isDeleted { get; set; }
    }
}
