namespace WorkNCInfoService.Mvc5.Models.WorkModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WorkNC_WorkZone")]
    public partial class WorkNC_WorkZone
    {
        [Key]
        public int WorkZoneId { get; set; }

        [StringLength(100)]
        [Display(Name ="Name")]
        public string Name { get; set; }

        public int CompanyId { get; set; }

        [StringLength(50)]
        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }

        [StringLength(50)]
        [Display(Name = "Factory Name")]
        public string FactoryName { get; set; }

        [Display(Name = "Machine")]
        public int MachineId { get; set; }

        [StringLength(100)]
        [Display(Name = "Work Zone Path")]
        public string WorkZonePath { get; set; }

        [StringLength(100)]
        [Display(Name = "Model Data Programer")]
        public string ModelDataProgramer { get; set; }

        [StringLength(100)]
        [Display(Name = "NC Data Programer")]
        public string NCDataProgramer { get; set; }

        [Display(Name = "Program Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ProgramDate { get; set; }

        [StringLength(100)]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        [StringLength(100)]
        [Display(Name = "Parts")]
        public string Parts { get; set; }

        [StringLength(100)]
        [Display(Name = "Part Name")]
        public string PartName { get; set; }

        [StringLength(10)]
        [Display(Name = "Machining Time Total")]
        public string MachiningTimeTotal { get; set; }

        [StringLength(255)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }

        [StringLength(250)]
        [Display(Name = "Image File")]
        public string ImageFile { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Create Account")]
        public string CreateAccount { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Modified Account")]
        public string ModifiedAccount { get; set; }
    }
}
