namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("WorkNC_Company")]
    public partial class WorkNC_Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }

        [StringLength(200)]
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [StringLength(200)]
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [StringLength(50)]
        [Display(Name = "TEL")]
        public string TEL { get; set; }

        [StringLength(50)]
        [Display(Name = "FAX")]
        public string FAX { get; set; }

        public bool isDeleted { get; set; }

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
