using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDCore.Pages
{
    public class UserItemViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public int Phone { get; set; }
        public string Workplace { get; set; }
        public IEnumerable<RoleItemViewModel> Roles { get; set; }

    }
    public class RoleItemViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
