using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Common
{
    public class OppointmentApiOptions
    {
        public string HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string Version { get; set; }
        public string SecretKey { get; set; }
    }
}
