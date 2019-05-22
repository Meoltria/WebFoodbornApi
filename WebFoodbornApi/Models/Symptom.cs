using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class Symptom
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string SymptomType { get; set; }
        public string SymptomName { get; set; }
        public string SymptomChildType { get; set; }
        public string SymptomDesc { get; set; }
        public string Status { get; set; }

        public Patient Patient { get; set; }
    }
}
