using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class PastMedicalHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string MedicalHistory { get; set; }
        public string MedicalHistoryDesc { get; set; }
        public string Status { get; set; }

        public Patient Patient { get; set; }
    }
}
