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
        public bool AcuteGastroenteritis { get; set; } //急性胃肠炎
        public bool InfectiousDiarrhea { get; set; } //感染性腹泻
        public bool PoisonousMushroomPoisoning { get; set; } //毒蘑菇中毒
        public bool BeanPoisoning { get; set; } //菜豆中毒
        public bool PufferfishPoisoning { get; set; } //河豚中毒
        public bool Botulism { get; set; } //肉毒中毒
        public bool NitritePoisoning { get; set; } //亚硝酸盐中毒
        public bool RhabdomyolysisSyndrome { get; set; } //横纹肌溶解综合征
        public bool ShellfishToxinPoisoning { get; set; } //贝类毒素中毒
        public bool Other { get; set; } //其他
        public string OtherInfo { get; set; }
        public string Status { get; set; }

        public Patient Patient { get; set; }
    }
}
