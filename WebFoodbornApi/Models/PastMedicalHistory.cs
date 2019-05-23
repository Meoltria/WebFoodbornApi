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
        public bool No { get; set; } //无
        public bool GeneralGastrointestinalInflammation { get; set; } //一般消化道炎症
        public bool CrohnsDisease { get; set; } //克罗恩病
        public bool GastrointestinalUlcer { get; set; } //消化道溃疡
        public bool GastrointestinalCancer { get; set; } //消化道肿瘤
        public bool IrritableBowelSyndrome { get; set; } //肠易激综合征
        public bool Meningitis { get; set; } //脑膜炎
        public bool BrainTumor { get; set; } //脑肿瘤
        public bool Other { get; set; } //其他
        public string OtherInfo { get; set; }
        public string Status { get; set; }

        public Patient Patient { get; set; }
    }
}
