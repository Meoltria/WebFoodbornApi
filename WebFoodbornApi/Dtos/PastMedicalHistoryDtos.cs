using System;
using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class PastMedicalHistoryOutput
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public bool No { get; set; }
        public bool GeneralGastrointestinalInflammation { get; set; }
        public bool CrohnsDisease { get; set; }
        public bool GastrointestinalUlcer { get; set; }
        public bool GastrointestinalCancer { get; set; }
        public bool IrritableBowelSyndrome { get; set; }
        public bool Meningitis { get; set; }
        public bool BrainTumor { get; set; }
        public bool Other { get; set; }
        public string OtherInfo { get; set; }
        public string Status { get; set; }
    }

    public class PastMedicalHistoryQueryInput : IPageAndSortInputDto
    {
        public int PatientId { get; set; }
    }

    public class PastMedicalHistoryCreateInput
    {
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        public bool No { get; set; }
        public bool GeneralGastrointestinalInflammation { get; set; }
        public bool CrohnsDisease { get; set; }
        public bool GastrointestinalUlcer { get; set; }
        public bool GastrointestinalCancer { get; set; }
        public bool IrritableBowelSyndrome { get; set; }
        public bool Meningitis { get; set; }
        public bool BrainTumor { get; set; }
        public bool Other { get; set; }
        public string OtherInfo { get; set; }
        public string Status { get; set; }
    }

    public class PastMedicalHistoryUpdateInput
    {
        [Required(ErrorMessage = "请输入既往史Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        public bool No { get; set; }
        public bool GeneralGastrointestinalInflammation { get; set; }
        public bool CrohnsDisease { get; set; }
        public bool GastrointestinalUlcer { get; set; }
        public bool GastrointestinalCancer { get; set; }
        public bool IrritableBowelSyndrome { get; set; }
        public bool Meningitis { get; set; }
        public bool BrainTumor { get; set; }
        public bool Other { get; set; }
        public string OtherInfo { get; set; }
        public string Status { get; set; }
    }
}
