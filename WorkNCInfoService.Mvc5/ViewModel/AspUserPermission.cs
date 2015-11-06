using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkNCInfoService.Mvc5.ViewModel
{
    public class AspUserWorkNc
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string WebPermission { get; set; }
        public bool AppPermission { get; set; }
        public bool LockoutEnable { get; set; }
        public int CompanyId { get; set; }
    }
}
