using System;
using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class InitialDiagnosisOutput
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string DiagnosisResult { get; set; }
        public string DiagnosisDesc { get; set; }
        public string Status { get; set; }
    }

    public class InitialDiagnosisQueryInput : IPageAndSortInputDto
    {
        public int PatientId { get; set; }
    }

    public class InitialDiagnosisCreateInput
    {
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "请输入诊断名称")]
        public string DiagnosisResult { get; set; }
        public string DiagnosisDesc { get; set; }
        public string Status { get; set; }
    }

    public class InitialDiagnosisUpdateInput
    {
        [Required(ErrorMessage = "请输入初次诊断Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "请输入诊断名称")]
        public string DiagnosisResult { get; set; }
        public string DiagnosisDesc { get; set; }
        public string Status { get; set; }
    }
}
