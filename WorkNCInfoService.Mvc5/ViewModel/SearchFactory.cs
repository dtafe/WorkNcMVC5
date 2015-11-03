using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.ViewModel
{
    public class SearchFactory
    {
        [Display(Name ="Factory Name")]
        public string SearchString { get; set; }

        [Display(Name ="Deleted")]
        public bool isDeleted { get; set; }
    }
}
