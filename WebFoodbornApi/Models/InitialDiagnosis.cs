using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class InitialDiagnosis
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string DiagnosisResult { get; set; }
        public string DiagnosisDesc { get; set; }
        public string Status { get; set; }

        public Patient Patient { get; set; }
    }
}
