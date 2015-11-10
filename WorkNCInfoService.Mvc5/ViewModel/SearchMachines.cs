using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.ViewModel
{
    public class SearchMachines
    {
        [Display(Name = "Factory Name")]
        public int FacrotyId { get; set; }

        [Display(Name = "Machine Name")]
        public string Name { get; set; }

        [Display(Name = "Deleted")]
        public bool? isDeleted { get; set; }
    }
}
