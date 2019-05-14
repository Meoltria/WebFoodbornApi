using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string UserTypeCode { get; set; }
        public string UserTypeName { get; set; }
        public int OrganazitionId { get; set; }
        public int RoleId { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string UserRankCode { get; set; }
        public string UserRankName { get; set; }
        public string Tel { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }

        public Orgnazition Organazition { get; set; }
        public Role Role { get; set; }
    }
}
