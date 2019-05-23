using System;
using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class InitialDiagnosisOutput
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public bool AcuteGastroenteritis { get; set; }
        public bool InfectiousDiarrhea { get; set; }
        public bool PoisonousMushroomPoisoning { get; set; }
        public bool BeanPoisoning { get; set; }
        public bool PufferfishPoisoning { get; set; }
        public bool Botulism { get; set; }
        public bool NitritePoisoning { get; set; }
        public bool RhabdomyolysisSyndrome { get; set; }
        public bool ShellfishToxinPoisoning { get; set; }
        public bool Other { get; set; }
        public string OtherInfo { get; set; }
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
        public bool AcuteGastroenteritis { get; set; }
        public bool InfectiousDiarrhea { get; set; }
        public bool PoisonousMushroomPoisoning { get; set; }
        public bool BeanPoisoning { get; set; }
        public bool PufferfishPoisoning { get; set; }
        public bool Botulism { get; set; }
        public bool NitritePoisoning { get; set; }
        public bool RhabdomyolysisSyndrome { get; set; }
        public bool ShellfishToxinPoisoning { get; set; }
        public bool Other { get; set; }
        public string OtherInfo { get; set; }
        public string Status { get; set; }
    }

    public class InitialDiagnosisUpdateInput
    {
        [Required(ErrorMessage = "请输入初次诊断Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        public bool AcuteGastroenteritis { get; set; }
        public bool InfectiousDiarrhea { get; set; }
        public bool PoisonousMushroomPoisoning { get; set; }
        public bool BeanPoisoning { get; set; }
        public bool PufferfishPoisoning { get; set; }
        public bool Botulism { get; set; }
        public bool NitritePoisoning { get; set; }
        public bool RhabdomyolysisSyndrome { get; set; }
        public bool ShellfishToxinPoisoning { get; set; }
        public bool Other { get; set; }
        public string OtherInfo { get; set; }
        public string Status { get; set; }
    }
}
