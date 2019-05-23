using System;
using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class PastMedicalHistoryOutput
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string MedicalHistory { get; set; }
        public string MedicalHistoryDesc { get; set; }
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
        [Required(ErrorMessage = "请输入既往史")]
        public string MedicalHistory { get; set; }
        public string MedicalHistoryDesc { get; set; }
        public string Status { get; set; }
    }

    public class PastMedicalHistoryUpdateInput
    {
        [Required(ErrorMessage = "请输入既往史Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "请输入既往史")]
        public string MedicalHistory { get; set; }
        public string MedicalHistoryDesc { get; set; }
        public string Status { get; set; }
    }
}
