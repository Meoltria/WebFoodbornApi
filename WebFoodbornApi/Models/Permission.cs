using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Parent { get; set; }
        public string Icon { get; set; }
        public string Path { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public string Status { get; set; }

        public ICollection<RolePermission> RolePermission { get; set; }
    }
}
