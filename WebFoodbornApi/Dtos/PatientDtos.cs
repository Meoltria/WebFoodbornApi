using System;
using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class PatientOutput
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string OutpatientNo { get; set; }
        public string IsHospitalizationCod { get; set; }
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
    }

    public class PatientQueryInput : IPageAndSortInputDto
    {
        public string OutpatientNo { get; set; }
        public string Name { get; set; }
    }

    public class PatientCreateInput
    {
        [Required(ErrorMessage = "请输入患者门诊号")]
        public string OutpatientNo { get; set; }
        [Required(ErrorMessage = "请选择是否住院")]
        public string IsHospitalizationCod { get; set; }
        public string IsHospitalizationName { get; set; }
        public string InpatientNo { get; set; }
        [Required(ErrorMessage = "请选择是否复诊")]
        public string IsReviewCode { get; set; }
        public string IsReviewName { get; set; }
        [Required(ErrorMessage = "请输入患者姓名")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = "请选择患者性别")]
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string Birthday { get; set; }
        public string IdCard { get; set; }
        [Required(ErrorMessage = "请输入发病时间")]
        public DateTime IllnessTime { get; set; }
        [Required(ErrorMessage = "请输入就诊时间")]
        public DateTime TreatmentTime { get; set; }
        [Required(ErrorMessage = "请选择就诊前是否使用抗生素")]
        public string IsAntibioticCode { get; set; }
        public string IsAntibioticName { get; set; }
        public string AntibioticName { get; set; }
        [Required(ErrorMessage = "请输入省市区")]
        public string ProvinceCityDistrict { get; set; }
        [Required(ErrorMessage = "请输入详细地址")]
        public string Address { get; set; }
        public string GuardianName { get; set; }
        [Required(ErrorMessage = "请选择患者职业")]
        public string ProfessionCode { get; set; }
        public string ProfessionName { get; set; }
        [Required(ErrorMessage = "请输入接诊医生")]
        public string ReceivingDoctor { get; set; }
        [Required(ErrorMessage = "请输入填表人")]
        public string FillUser { get; set; }
        [Required(ErrorMessage = "请输入填表时间")]
        public DateTime FillTime { get; set; }
        public string Status { get; set; }
    }

    public class PatientUpdateInput
    {
        [Required(ErrorMessage = "患者Id不能为空")]
        public int Id { get; set; }
        public string Guid { get; set; }
        [Required(ErrorMessage = "请输入患者门诊号")]
        public string OutpatientNo { get; set; }
        [Required(ErrorMessage = "请选择是否住院")]
        public string IsHospitalizationCod { get; set; }
        public string IsHospitalizationName { get; set; }
        public string InpatientNo { get; set; }
        [Required(ErrorMessage = "请选择是否复诊")]
        public string IsReviewCode { get; set; }
        public string IsReviewName { get; set; }
        [Required(ErrorMessage = "请输入患者姓名")]
        public string PatientName { get; set; }
        [Required(ErrorMessage = "请选择患者性别")]
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public string Birthday { get; set; }
        public string IdCard { get; set; }
        [Required(ErrorMessage = "请输入发病时间")]
        public DateTime IllnessTime { get; set; }
        [Required(ErrorMessage = "请输入就诊时间")]
        public DateTime TreatmentTime { get; set; }
        [Required(ErrorMessage = "请选择就诊前是否使用抗生素")]
        public string IsAntibioticCode { get; set; }
        public string IsAntibioticName { get; set; }
        public string AntibioticName { get; set; }
        [Required(ErrorMessage = "请输入省市区")]
        public string ProvinceCityDistrict { get; set; }
        [Required(ErrorMessage = "请输入详细地址")]
        public string Address { get; set; }
        public string GuardianName { get; set; }
        [Required(ErrorMessage = "请选择患者职业")]
        public string ProfessionCode { get; set; }
        public string ProfessionName { get; set; }
        [Required(ErrorMessage = "请输入接诊医生")]
        public string ReceivingDoctor { get; set; }
        [Required(ErrorMessage = "请输入填表人")]
        public string FillUser { get; set; }
        [Required(ErrorMessage = "请输入填表时间")]
        public DateTime FillTime { get; set; }
        public string Status { get; set; }
    }
}
