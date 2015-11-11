using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.ViewModel
{
    public class SearchCompany
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Show Deleted")]
        public bool isDeleted { get; set; }
    }
}
