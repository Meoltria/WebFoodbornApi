using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class Orgnazition
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string OrgTypeCode { get; set; }
        public string OrgTypeName { get; set; }
        public int Parent { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
