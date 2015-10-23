using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.ViewModel
{
    public class SearchDate
    {
        [Display(Name = "Workzone")]
        public string Name { get; set; }

        [Display(Name = "Program Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Factory")]
        public int FactoryId { get; set; }

        [Display(Name = "Machine")]
        public int MachineId { get; set; }
    }
}
