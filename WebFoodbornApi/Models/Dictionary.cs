using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string RemarkName { get; set; }
        public string RemarkValue { get; set; }
    }
}
