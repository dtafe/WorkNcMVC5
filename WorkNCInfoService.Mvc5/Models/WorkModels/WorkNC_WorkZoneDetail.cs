namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkNC_WorkZoneDetail")]
    public partial class WorkNC_WorkZoneDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkZoneId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkZoneDetailId { get; set; }

        [StringLength(50)]
        [Display(Name ="No")]
        public string No { get; set; }

        [StringLength(100)]
        [Display(Name = "Path")]
        public string PathType { get; set; }

        [Display(Name = "Tool Lenth")]
        public double? ToolLenth { get; set; }

        [Display(Name = "Tno")]
        public double? Tno { get; set; }

        [Display(Name = "Stock Allowance")]
        public double? StockAllowance { get; set; }

        [Display(Name = "Tolerance")]
        public double? Tolerance { get; set; }

        [StringLength(100)]
        [Display(Name = "File Name")]
        public string NCFileName { get; set; }

        [StringLength(10)]
        [Display(Name = "Machine Time")]
        public string MachineTime { get; set; }

        [Display(Name = "Machine Distance")]
        public double? MachineDistance { get; set; }

        [StringLength(255)]
        [Display(Name = "Tool Shape")]
        public string ToolShape { get; set; }

        [Display(Name = "Diameter")]
        public double? ToolDia { get; set; }

        [Display(Name = "Tool Coner Radius")]
        public double? ToolConerR { get; set; }

        [StringLength(255)]
        [Display(Name = "Holder Name")]
        public string HolderName { get; set; }

        [Display(Name = "Spindle")]
        public double? Spindle { get; set; }

        [Display(Name = "Cutting Feed Rate")]
        public double? CuttingFeedRate { get; set; }

        [Display(Name = "Approach Feed Rate")]
        public double? ApproachFeedRate { get; set; }

        [StringLength(255)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }

        [StringLength(250)]
        [Display(Name = "Image File")]
        public string ImageFile { get; set; }

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
