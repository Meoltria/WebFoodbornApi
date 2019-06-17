using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebFoodbornApi.Models;

namespace WebFoodbornApi.Common
{
    public class XmlHelper
    {
        public string ConvertToXml(int operationType, FoodBornApiOptions apiOptions, Patient patient, InitialDiagnosis initialDiagnosis, PastMedicalHistory pastMedicalHistory, Symptom symptom, List<FoodInfo> foodInfos)
        {
            XDocument xDoc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement
                (
                    "接口",
                    new XElement("令牌", apiOptions.SecretKey),
                    new XElement("数据类型", 3),
                    new XElement("操作类型", operationType),
                    new XElement("操作单位", apiOptions.HospitalName),
                    new XElement("操作用户", apiOptions.UserName),
                    new XElement
                    (
                        "病例",
                        new XAttribute("Guid", patient.Guid),
                        new XElement
                        (
                            "填报信息",
                            new XElement("填表人", patient.FillUser),
                            new XElement("接诊医生", patient.ReceivingDoctor),
                            new XElement("填表日期", patient.FillTime.ToString("yyyy-MM-dd HH:mm:ss")),
                            new XElement("医疗机构", apiOptions.HospitalName)
                        ),
                        new XElement
                        (
                            "病例基本信息",
                            new XElement("发病时间", patient.IllnessTime.ToString("yyyy-MM-dd HH:mm:ss")),
                            new XElement("就诊时间", patient.TreatmentTime.ToString("yyyy-MM-dd HH:mm:ss")),
                            new XElement("门诊号", patient.OutpatientNo),
                            new XElement("是否复诊", patient.IsReviewName),
                            new XElement("是否住院", patient.IsHospitalizationName),
                            patient.IsHospitalizationCode.Equals("1") ? new XElement("住院号", patient.InpatientNo) : null,
                            new XElement("患者姓名", patient.PatientName),
                            string.IsNullOrEmpty(patient.GuardianName) ? null : new XElement("监护人姓名", patient.GuardianName),
                            new XElement("患者性别", patient.GenderName),
                            new XElement("患者职业", patient.ProfessionName),
                            string.IsNullOrEmpty(patient.IdCard) ? null : new XElement("身份证号", patient.IdCard),
                            new XElement("出生日期", string.IsNullOrEmpty(patient.Birthday) ? "" : patient.Birthday),
                            new XElement("联系电话", patient.Phone),
                            new XElement("患者属于", "本县区"),
                            new XElement
                            (
                                "现在住址",
                                 new XElement("省市县", patient.ProvinceCityDistrict),
                                 new XElement("详细地址", patient.Address)
                            )
                        ),
                        new XElement
                        (
                            "主要症状与体征",
                            new XElement
                            (
                                "全身症状与体征",
                                symptom.Fever ? new XElement
                                (
                                    "发热",
                                    new XElement("度数", symptom.FeverDegree)
                                ) : null,
                                new XElement("面色潮红", symptom.FacialFlush ? "是" : "否"),
                                new XElement("面色苍白", symptom.Pale ? "是" : "否"),
                                new XElement("发绀", symptom.Hairpin ? "是" : "否"),
                                new XElement("脱水", symptom.Dehydration ? "是" : "否"),
                                new XElement("口渴", symptom.Thirsty ? "是" : "否"),
                                new XElement("浮肿", symptom.Puffiness ? "是" : "否"),
                                new XElement("体重下降", symptom.WeightLoss ? "是" : "否"),
                                new XElement("寒战", symptom.Chill ? "是" : "否"),
                                new XElement("乏力", symptom.Weak ? "是" : "否"),
                                new XElement("贫血", symptom.Anemia ? "是" : "否"),
                                new XElement("肿胀", symptom.Swollen ? "是" : "否"),
                                new XElement("失眠", symptom.Insomnia ? "是" : "否"),
                                new XElement("畏光", symptom.Photophobia ? "是" : "否"),
                                new XElement("口有糊味", symptom.Mouthly ? "是" : "否"),
                                new XElement("金属味", symptom.Metallic ? "是" : "否"),
                                new XElement("肥皂咸味", symptom.SoapSalty ? "是" : "否"),
                                new XElement("唾液过多", symptom.ExcessiveSaliva ? "是" : "否"),
                                new XElement("足腕下垂", symptom.FootWristPendant ? "是" : "否"),
                                new XElement("色素沉着", symptom.Pigmentation ? "是" : "否"),
                                new XElement("脱皮", symptom.Peeling ? "是" : "否"),
                                new XElement("指甲出现白带", symptom.NailBand ? "是" : "否"),
                                symptom.SignsOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.SignsOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "消化系统",
                                new XElement("恶心", symptom.Disgusting ? "是" : "否"),
                                symptom.Vomiting ? new XElement
                                (
                                    "呕吐",
                                    new XElement("次数", symptom.VomitingCount)
                                ) : null,
                                new XElement("腹痛", symptom.StomachAche ? "是" : "否"),
                                symptom.Diarrhea ? new XElement
                                (
                                    "腹泻性状",
                                    new XElement("性状", symptom.DiarrheaTraits),
                                    new XElement("次数", symptom.DiarrheaCount)
                                ) : new XElement("腹泻性状"),
                                new XElement("便秘", symptom.Constipation ? "是" : "否"),
                                new XElement("里急后重", symptom.HeavyAndHeavy ? "是" : "否"),
                                symptom.DigestiveOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.DigestiveOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "呼吸系统",
                                new XElement("呼吸短促", symptom.ShortnessOfBreath ? "是" : "否"),
                                new XElement("咯血", symptom.Hemoptysis ? "是" : "否"),
                                new XElement("呼吸困难", symptom.DifficultyBreathing ? "是" : "否"),
                                symptom.RespiratoryOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.RespiratoryOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "心脑血管系统",
                                new XElement("胸闷", symptom.ChestTightness ? "是" : "否"),
                                new XElement("胸痛", symptom.ChestPain ? "是" : "否"),
                                new XElement("心悸", symptom.Palpitations ? "是" : "否"),
                                new XElement("气短", symptom.BreathHard ? "是" : "否"),
                                symptom.CardiovascularOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.CardiovascularOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "泌尿系统",
                                new XElement("尿量减少", symptom.ReducedUrineOutput ? "是" : "否"),
                                new XElement("背部肾区疼痛", symptom.BackKidneyPain ? "是" : "否"),
                                new XElement("尿中带血", symptom.BloodInTheUrine ? "是" : "否"),
                                new XElement("肾结石", symptom.KidneyStones ? "是" : "否"),
                                symptom.UrinaryOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.UrinaryOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "神经系统",
                                new XElement("头痛", symptom.Headache ? "是" : "否"),
                                new XElement("眩晕", symptom.Dizziness ? "是" : "否"),
                                new XElement("昏迷", symptom.Coma ? "是" : "否"),
                                new XElement("抽搐", symptom.Convulsion ? "是" : "否"),
                                new XElement("惊厥", symptom.Horror ? "是" : "否"),
                                new XElement("谵妄", symptom.Delirium ? "是" : "否"),
                                new XElement("瘫痪", symptom.Paralysis ? "是" : "否"),
                                new XElement("言语困难", symptom.DifficultiesInSpeech ? "是" : "否"),
                                new XElement("吞咽困难", symptom.HardToSwallow ? "是" : "否"),
                                new XElement("感觉异常", symptom.FeelingAbnormal ? "是" : "否"),
                                new XElement("精神失常", symptom.MentalDisorder ? "是" : "否"),
                                new XElement("复视", symptom.Diplopia ? "是" : "否"),
                                new XElement("视力模糊", symptom.BlurredVision ? "是" : "否"),
                                new XElement("肢体麻木", symptom.LimbNumbness ? "是" : "否"),
                                new XElement("末梢感觉障碍", symptom.PeripheralSensoryDisorder ? "是" : "否"),
                                symptom.PupilAbnormality ? new XElement
                                (
                                    "瞳孔异常",
                                    new XElement("状态", symptom.PupilStatus)
                                ) : null,
                                new XElement("针刺感", symptom.Acupuncture ? "是" : "否"),
                                symptom.Nerveother ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.NerveOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "皮肤和皮下组织",
                                new XElement("瘙痒", symptom.Itching ? "是" : "否"),
                                new XElement("烧灼感", symptom.BurningSensation ? "是" : "否"),
                                new XElement("皮疹", symptom.Rash ? "是" : "否"),
                                new XElement("出血点", symptom.BleedingPoint ? "是" : "否"),
                                new XElement("黄疸", symptom.Jaundice ? "是" : "否"),
                                symptom.SkinOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.SkinOtherInfo)
                                ) : null
                            )
                        ),
                        new XElement
                        (
                            "初步诊断",
                            new XElement("急性胃肠炎", initialDiagnosis.AcuteGastroenteritis ? "是" : "否"),
                            new XElement("感染性腹泻", initialDiagnosis.InfectiousDiarrhea ? "是" : "否"),
                            new XElement("毒蘑菇中毒", initialDiagnosis.PoisonousMushroomPoisoning ? "是" : "否"),
                            new XElement("菜豆中毒", initialDiagnosis.BeanPoisoning ? "是" : "否"),
                            new XElement("河豚中毒", initialDiagnosis.PufferfishPoisoning ? "是" : "否"),
                            new XElement("肉毒中毒", initialDiagnosis.Botulism ? "是" : "否"),
                            new XElement("亚硝酸盐中毒", initialDiagnosis.NitritePoisoning ? "是" : "否"),
                            new XElement("横纹肌溶解综合征", initialDiagnosis.RhabdomyolysisSyndrome ? "是" : "否"),
                            //new XElement("贝类毒素中毒", initialDiagnosis.ShellfishToxinPoisoning ? "是" : "否"),
                            initialDiagnosis.Other ? new XElement
                            (
                                "其他",
                                new XElement("名称", initialDiagnosis.OtherInfo)
                            ) : null
                        ),
                        new XElement
                        (
                            "抗生素",
                            new XElement("是否使用抗生素", patient.IsAntibioticName),
                            patient.IsAntibioticName.Equals("是") ? new XElement("抗生素名称", patient.AntibioticName) : null
                        ),
                        new XElement
                        (
                            "既往病史",
                            new XElement("一般消化道炎症", pastMedicalHistory.GeneralGastrointestinalInflammation ? "是" : "否"),
                            new XElement("克罗恩病", pastMedicalHistory.CrohnsDisease ? "是" : "否"),
                            new XElement("消化道溃疡", pastMedicalHistory.GastrointestinalUlcer ? "是" : "否"),
                            new XElement("消化道肿瘤", pastMedicalHistory.GastrointestinalCancer ? "是" : "否"),
                            new XElement("肠易激综合征", pastMedicalHistory.IrritableBowelSyndrome ? "是" : "否"),
                            new XElement("脑膜炎脑肿瘤等", pastMedicalHistory.Meningitis ? "是" : "否"),
                            pastMedicalHistory.Other ? new XElement
                            (
                                "其他",
                                new XElement("名称", pastMedicalHistory.OtherInfo)
                            ) : null
                        ),
                        new XElement
                        (
                            "暴露信息",
                            from foodInfo in foodInfos
                            select new XElement
                            (
                                "暴露信息条目",
                                new XElement("食品名称", foodInfo.FoodName),
                                new XElement("食品分类", foodInfo.FoodType),
                                new XElement("加工或包装方式", foodInfo.FoodPackaging),
                                new XElement("食品品牌", foodInfo.FoodBrand),
                                new XElement("生产厂家", foodInfo.Manufacturer),
                                new XElement("进食场所", foodInfo.EatingPlace),
                                new XElement("购买场所", foodInfo.PurchasePlace),
                                new XElement
                                (
                                    "进食地点",
                                    new XElement("境内境外", foodInfo.EatingBorderland),
                                    new XElement("省市县", foodInfo.EatingProvinceCityDistrict),
                                    new XElement("详细地址", foodInfo.EatingAddress)
                                ),
                                new XElement
                                (
                                    "购买地点",
                                    new XElement("境内境外", foodInfo.PurchaseBorderland),
                                    new XElement("省市县", foodInfo.PurchaseProvinceCityDistrict),
                                    new XElement("详细地址", foodInfo.PurchaseAddress)
                                ),
                                new XElement("进食人数", foodInfo.EatingCounts),
                                new XElement("进食时间", foodInfo.EatingTime.ToString("yyyy-MM-dd HH:mm:ss")),
                                new XElement("他人是否发病", foodInfo.IsOtherPeople)
                            )
                        )
                    )
                )
            );

            return xDoc.ToString();
        }

        public string ConvertToXml(int operationType, FoodBornApiOptions apiOptions, Patient patient)
        {
            XDocument xDoc = new XDocument
            (
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement
                (
                    "接口",
                    new XElement("令牌", apiOptions.SecretKey),
                    new XElement("数据类型", 3),
                    new XElement("操作类型", operationType),
                    new XElement("操作单位", apiOptions.HospitalName),
                    new XElement("操作用户", apiOptions.UserName),
                    new XElement("病例", patient.Guid)
                )
             );

            return xDoc.ToString();
        }
    }
}
