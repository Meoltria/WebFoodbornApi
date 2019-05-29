using System;
using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class SymptomOutput
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public bool Fever { get; set; } //发热
        public string FeverDegree { get; set; }//发热度数
        public bool FacialFlush { get; set; } //面色潮红
        public bool Pale { get; set; }//面色苍白
        public bool Hairpin { get; set; }//发绀
        public bool Dehydration { get; set; }//脱水
        public bool Thirsty { get; set; }//口渴
        public bool Puffiness { get; set; }//浮肿
        public bool WeightLoss { get; set; }//体重下降
        public bool Chill { get; set; }//寒战
        public bool Weak { get; set; }//乏力
        public bool Anemia { get; set; }//贫血
        public bool Swollen { get; set; }//肿胀
        public bool Insomnia { get; set; }//失眠
        public bool Photophobia { get; set; }//畏光
        public bool Mouthly { get; set; }//口有糊味
        public bool Metallic { get; set; }//金属味
        public bool SoapSalty { get; set; }//肥皂/咸味
        public bool ExcessiveSaliva { get; set; }//唾液过多
        public bool FootWristPendant { get; set; }//足/腕下垂
        public bool Pigmentation { get; set; }//色素沉着
        public bool Peeling { get; set; }//脱皮
        public bool NailBand { get; set; }//指甲出现白带
        public bool SignsOther { get; set; }//全身症状与体征其他
        public string SignsOtherInfo { get; set; }//全身症状与体征其他信息
        public bool Disgusting { get; set; }//恶心
        public bool Vomiting { get; set; }//呕吐
        public string VomitingCount { get; set; }//呕吐次数
        public bool StomachAche { get; set; }//腹痛
        public bool Diarrhea { get; set; }//腹泻
        public string DiarrheaTraits { get; set; }//腹泻性状
        public string DiarrheaCount { get; set; }//腹泻次数
        public bool Constipation { get; set; }//便秘
        public bool HeavyAndHeavy { get; set; }//里急后重
        public bool DigestiveOther { get; set; }//消化系统其他
        public string DigestiveOtherInfo { get; set; }//消化系统其他信息
        public bool ShortnessOfBreath { get; set; }//呼吸短促
        public bool Hemoptysis { get; set; }//咯血
        public bool DifficultyBreathing { get; set; }//呼吸困难
        public bool RespiratoryOther { get; set; }//呼吸系统其他
        public string RespiratoryOtherInfo { get; set; }//呼吸系统其他信息
        public bool ChestTightness { get; set; }//胸闷
        public bool ChestPain { get; set; }//胸痛
        public bool Palpitations { get; set; }//心悸
        public bool BreathHard { get; set; }//气短
        public bool CardiovascularOther { get; set; }//心血管系统其他
        public string CardiovascularOtherInfo { get; set; }//心血管系统其他信息
        public bool ReducedUrineOutput { get; set; }//尿量减少
        public bool BackKidneyPain { get; set; }//背部肾区疼痛
        public bool BloodInTheUrine { get; set; }//尿中带血
        public bool KidneyStones { get; set; }//肾结石
        public bool UrinaryOther { get; set; }//泌尿系统其他
        public string UrinaryOtherInfo { get; set; }//泌尿系统其他信息
        public bool Headache { get; set; }//头痛
        public bool Dizziness { get; set; }//眩晕
        public bool Coma { get; set; }//昏迷
        public bool Convulsion { get; set; }//抽搐
        public bool Horror { get; set; }//惊厥
        public bool Delirium { get; set; }//谵妄
        public bool Paralysis { get; set; }//瘫痪
        public bool DifficultiesInSpeech { get; set; }//言语困难
        public bool HardToSwallow { get; set; }//吞咽困难
        public bool FeelingAbnormal { get; set; }//感觉异常
        public bool MentalDisorder { get; set; }//精神失常
        public bool Diplopia { get; set; }//复视
        public bool BlurredVision { get; set; }//视力模糊
        public bool EyelidDrooping { get; set; }//眼睑下垂
        public bool LimbNumbness { get; set; }//肢体麻木
        public bool PeripheralSensoryDisorder { get; set; }//末梢感觉障碍
        public bool PupilAbnormality { get; set; }//瞳孔异常
        public string PupilStatus { get; set; }//瞳孔异常状态
        public bool Acupuncture { get; set; }//针刺感
        public bool Nerveother { get; set; }//神经系统其他
        public string NerveOtherInfo { get; set; }//神经系统其他信息
        public bool Itching { get; set; }//瘙痒
        public bool BurningSensation { get; set; }//烧灼感
        public bool Rash { get; set; }//皮疹
        public bool BleedingPoint { get; set; }//出血点
        public bool Jaundice { get; set; }//黄疸
        public bool SkinOther { get; set; }//皮肤其他
        public string SkinOtherInfo { get; set; }//皮肤其他信息
        public string Status { get; set; }
    }

    public class SymptomQueryInput : IPageAndSortInputDto
    {
        public int PatientId { get; set; }
    }

    public class SymptomCreateInput
    {
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        public bool Fever { get; set; } //发热
        public string FeverDegree { get; set; }//发热度数
        public bool FacialFlush { get; set; } //面色潮红
        public bool Pale { get; set; }//面色苍白
        public bool Hairpin { get; set; }//发绀
        public bool Dehydration { get; set; }//脱水
        public bool Thirsty { get; set; }//口渴
        public bool Puffiness { get; set; }//浮肿
        public bool WeightLoss { get; set; }//体重下降
        public bool Chill { get; set; }//寒战
        public bool Weak { get; set; }//乏力
        public bool Anemia { get; set; }//贫血
        public bool Swollen { get; set; }//肿胀
        public bool Insomnia { get; set; }//失眠
        public bool Photophobia { get; set; }//畏光
        public bool Mouthly { get; set; }//口有糊味
        public bool Metallic { get; set; }//金属味
        public bool SoapSalty { get; set; }//肥皂/咸味
        public bool ExcessiveSaliva { get; set; }//唾液过多
        public bool FootWristPendant { get; set; }//足/腕下垂
        public bool Pigmentation { get; set; }//色素沉着
        public bool Peeling { get; set; }//脱皮
        public bool NailBand { get; set; }//指甲出现白带
        public bool SignsOther { get; set; }//全身症状与体征其他
        public string SignsOtherInfo { get; set; }//全身症状与体征其他信息
        public bool Disgusting { get; set; }//恶心
        public bool Vomiting { get; set; }//呕吐
        public string VomitingCount { get; set; }//呕吐次数
        public bool StomachAche { get; set; }//腹痛
        public bool Diarrhea { get; set; }//腹泻
        public string DiarrheaTraits { get; set; }//腹泻性状
        public string DiarrheaCount { get; set; }//腹泻次数
        public bool Constipation { get; set; }//便秘
        public bool HeavyAndHeavy { get; set; }//里急后重
        public bool DigestiveOther { get; set; }//消化系统其他
        public string DigestiveOtherInfo { get; set; }//消化系统其他信息
        public bool ShortnessOfBreath { get; set; }//呼吸短促
        public bool Hemoptysis { get; set; }//咯血
        public bool DifficultyBreathing { get; set; }//呼吸困难
        public bool RespiratoryOther { get; set; }//呼吸系统其他
        public string RespiratoryOtherInfo { get; set; }//呼吸系统其他信息
        public bool ChestTightness { get; set; }//胸闷
        public bool ChestPain { get; set; }//胸痛
        public bool Palpitations { get; set; }//心悸
        public bool BreathHard { get; set; }//气短
        public bool CardiovascularOther { get; set; }//心血管系统其他
        public string CardiovascularOtherInfo { get; set; }//心血管系统其他信息
        public bool ReducedUrineOutput { get; set; }//尿量减少
        public bool BackKidneyPain { get; set; }//背部肾区疼痛
        public bool BloodInTheUrine { get; set; }//尿中带血
        public bool KidneyStones { get; set; }//肾结石
        public bool UrinaryOther { get; set; }//泌尿系统其他
        public string UrinaryOtherInfo { get; set; }//泌尿系统其他信息
        public bool Headache { get; set; }//头痛
        public bool Dizziness { get; set; }//眩晕
        public bool Coma { get; set; }//昏迷
        public bool Convulsion { get; set; }//抽搐
        public bool Horror { get; set; }//惊厥
        public bool Delirium { get; set; }//谵妄
        public bool Paralysis { get; set; }//瘫痪
        public bool DifficultiesInSpeech { get; set; }//言语困难
        public bool HardToSwallow { get; set; }//吞咽困难
        public bool FeelingAbnormal { get; set; }//感觉异常
        public bool MentalDisorder { get; set; }//精神失常
        public bool Diplopia { get; set; }//复视
        public bool BlurredVision { get; set; }//视力模糊
        public bool EyelidDrooping { get; set; }//眼睑下垂
        public bool LimbNumbness { get; set; }//肢体麻木
        public bool PeripheralSensoryDisorder { get; set; }//末梢感觉障碍
        public bool PupilAbnormality { get; set; }//瞳孔异常
        public string PupilStatus { get; set; }//瞳孔异常状态
        public bool Acupuncture { get; set; }//针刺感
        public bool Nerveother { get; set; }//神经系统其他
        public string NerveOtherInfo { get; set; }//神经系统其他信息
        public bool Itching { get; set; }//瘙痒
        public bool BurningSensation { get; set; }//烧灼感
        public bool Rash { get; set; }//皮疹
        public bool BleedingPoint { get; set; }//出血点
        public bool Jaundice { get; set; }//黄疸
        public bool SkinOther { get; set; }//皮肤其他
        public string SkinOtherInfo { get; set; }//皮肤其他信息
        public string Status { get; set; }
    }

    public class SymptomUpdateInput
    {
        [Required(ErrorMessage = "请输入症状体征Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        public bool Fever { get; set; } //发热
        public string FeverDegree { get; set; }//发热度数
        public bool FacialFlush { get; set; } //面色潮红
        public bool Pale { get; set; }//面色苍白
        public bool Hairpin { get; set; }//发绀
        public bool Dehydration { get; set; }//脱水
        public bool Thirsty { get; set; }//口渴
        public bool Puffiness { get; set; }//浮肿
        public bool WeightLoss { get; set; }//体重下降
        public bool Chill { get; set; }//寒战
        public bool Weak { get; set; }//乏力
        public bool Anemia { get; set; }//贫血
        public bool Swollen { get; set; }//肿胀
        public bool Insomnia { get; set; }//失眠
        public bool Photophobia { get; set; }//畏光
        public bool Mouthly { get; set; }//口有糊味
        public bool Metallic { get; set; }//金属味
        public bool SoapSalty { get; set; }//肥皂/咸味
        public bool ExcessiveSaliva { get; set; }//唾液过多
        public bool FootWristPendant { get; set; }//足/腕下垂
        public bool Pigmentation { get; set; }//色素沉着
        public bool Peeling { get; set; }//脱皮
        public bool NailBand { get; set; }//指甲出现白带
        public bool SignsOther { get; set; }//全身症状与体征其他
        public string SignsOtherInfo { get; set; }//全身症状与体征其他信息
        public bool Disgusting { get; set; }//恶心
        public bool Vomiting { get; set; }//呕吐
        public string VomitingCount { get; set; }//呕吐次数
        public bool StomachAche { get; set; }//腹痛
        public bool Diarrhea { get; set; }//腹泻
        public string DiarrheaTraits { get; set; }//腹泻性状
        public string DiarrheaCount { get; set; }//腹泻次数
        public bool Constipation { get; set; }//便秘
        public bool HeavyAndHeavy { get; set; }//里急后重
        public bool DigestiveOther { get; set; }//消化系统其他
        public string DigestiveOtherInfo { get; set; }//消化系统其他信息
        public bool ShortnessOfBreath { get; set; }//呼吸短促
        public bool Hemoptysis { get; set; }//咯血
        public bool DifficultyBreathing { get; set; }//呼吸困难
        public bool RespiratoryOther { get; set; }//呼吸系统其他
        public string RespiratoryOtherInfo { get; set; }//呼吸系统其他信息
        public bool ChestTightness { get; set; }//胸闷
        public bool ChestPain { get; set; }//胸痛
        public bool Palpitations { get; set; }//心悸
        public bool BreathHard { get; set; }//气短
        public bool CardiovascularOther { get; set; }//心血管系统其他
        public string CardiovascularOtherInfo { get; set; }//心血管系统其他信息
        public bool ReducedUrineOutput { get; set; }//尿量减少
        public bool BackKidneyPain { get; set; }//背部肾区疼痛
        public bool BloodInTheUrine { get; set; }//尿中带血
        public bool KidneyStones { get; set; }//肾结石
        public bool UrinaryOther { get; set; }//泌尿系统其他
        public string UrinaryOtherInfo { get; set; }//泌尿系统其他信息
        public bool Headache { get; set; }//头痛
        public bool Dizziness { get; set; }//眩晕
        public bool Coma { get; set; }//昏迷
        public bool Convulsion { get; set; }//抽搐
        public bool Horror { get; set; }//惊厥
        public bool Delirium { get; set; }//谵妄
        public bool Paralysis { get; set; }//瘫痪
        public bool DifficultiesInSpeech { get; set; }//言语困难
        public bool HardToSwallow { get; set; }//吞咽困难
        public bool FeelingAbnormal { get; set; }//感觉异常
        public bool MentalDisorder { get; set; }//精神失常
        public bool Diplopia { get; set; }//复视
        public bool BlurredVision { get; set; }//视力模糊
        public bool EyelidDrooping { get; set; }//眼睑下垂
        public bool LimbNumbness { get; set; }//肢体麻木
        public bool PeripheralSensoryDisorder { get; set; }//末梢感觉障碍
        public bool PupilAbnormality { get; set; }//瞳孔异常
        public string PupilStatus { get; set; }//瞳孔异常状态
        public bool Acupuncture { get; set; }//针刺感
        public bool Nerveother { get; set; }//神经系统其他
        public string NerveOtherInfo { get; set; }//神经系统其他信息
        public bool Itching { get; set; }//瘙痒
        public bool BurningSensation { get; set; }//烧灼感
        public bool Rash { get; set; }//皮疹
        public bool BleedingPoint { get; set; }//出血点
        public bool Jaundice { get; set; }//黄疸
        public bool SkinOther { get; set; }//皮肤其他
        public string SkinOtherInfo { get; set; }//皮肤其他信息
        public string Status { get; set; }
    }
}
