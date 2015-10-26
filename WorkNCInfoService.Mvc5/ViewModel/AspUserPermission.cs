using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.ViewModel
{
    public class AspUserWorkNc
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirm { get; set; }
        public string WebPermission { get; set; }
        public bool AppPermission { get; set; }
    }
}
