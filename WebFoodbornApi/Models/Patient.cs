using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string OutpatientNo { get; set; }
        public string IsHospitalizationCode { get; set; }
        public string IsHospitalizationName { get; set; }
        public string InpatientNo { get; set; }
        public string IsReviewCode { get; set; }
        public string IsReviewName { get; set; }
        public string PatientName { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string Birthday { get; set; }
        public string IdCard { get; set; }
        public DateTime IllnessTime { get; set; }
        public DateTime TreatmentTime { get; set; }
        public string IsAntibioticCode { get; set; }
        public string IsAntibioticName { get; set; }
        public string AntibioticName { get; set; }
        public string ProvinceCityDistrict { get; set; }
        public string Address { get; set; }
        public string GuardianName { get; set; }
        public string ProfessionCode { get; set; }
        public string ProfessionName { get; set; }
        public string ReceivingDoctor { get; set; }
        public string FillUser { get; set; }
        public DateTime FillTime { get; set; }
        public string Status { get; set; }

        public ICollection<InitialDiagnosis> InitialDiagnoses { get; set; }
        public ICollection<PastMedicalHistory> PastMedicalHistories { get; set; }
        public ICollection<Symptom> Symptoms { get; set; }
        public ICollection<FoodInfo> FoodInfos { get; set; }
    }
}
