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
                            string.IsNullOrEmpty(patient.InpatientNo) ? null : new XElement("住院号", patient.InpatientNo),
                            new XElement("患者姓名", patient.PatientName),
                            string.IsNullOrEmpty(patient.GuardianName) ? null : new XElement("监护人姓名", patient.GuardianName),
                            new XElement("患者性别", patient.GenderName),
                            new XElement("患者职业", patient.ProfessionName),
                            string.IsNullOrEmpty(patient.IdCard) ? null : new XElement("身份证号", patient.IdCard),
                            new XElement("出生日期", patient.Birthday),
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
                                symptom.FacialFlush ? new XElement("面色潮红", "是") : null,
                                symptom.Pale ? new XElement("面色苍白", "是") : null,
                                symptom.Hairpin ? new XElement("发绀", "是") : null,
                                symptom.Dehydration ? new XElement("脱水", "是") : null,
                                symptom.Thirsty ? new XElement("口渴", "是") : null,
                                symptom.Puffiness ? new XElement("浮肿", "是") : null,
                                symptom.WeightLoss ? new XElement("体重下降", "是") : null,
                                symptom.Chill ? new XElement("寒战", "是") : null,
                                symptom.Weak ? new XElement("乏力", "是") : null,
                                symptom.Anemia ? new XElement("贫血", "是") : null,
                                symptom.Swollen ? new XElement("肿胀", "是") : null,
                                symptom.Insomnia ? new XElement("失眠", "是") : null,
                                symptom.Photophobia ? new XElement("畏光", "是") : null,
                                symptom.Mouthly ? new XElement("口有糊味", "是") : null,
                                symptom.Metallic ? new XElement("金属味", "是") : null,
                                symptom.SoapSalty ? new XElement("肥皂咸味", "是") : null,
                                symptom.ExcessiveSaliva ? new XElement("唾液过多", "是") : null,
                                symptom.FootWristPendant ? new XElement("足腕下垂", "是") : null,
                                symptom.Pigmentation ? new XElement("色素沉着", "是") : null,
                                symptom.Peeling ? new XElement("脱皮", "是") : null,
                                symptom.NailBand ? new XElement("指甲出现白带", "是") : null,
                                symptom.SignsOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.SignsOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "消化系统",
                                symptom.Disgusting ? new XElement("恶心", "是") : null,
                                symptom.Vomiting ? new XElement
                                (
                                    "呕吐",
                                    new XElement("次数", symptom.VomitingCount)
                                ) : null,
                                symptom.StomachAche ? new XElement("腹痛", "是") : null,
                                symptom.Diarrhea ? new XElement
                                (
                                    "腹泻",
                                    new XElement("性状", symptom.DiarrheaTraits),
                                    new XElement("次数", symptom.DiarrheaCount)
                                ) : null,
                                symptom.Constipation ? new XElement("便秘", "是") : null,
                                symptom.HeavyAndHeavy ? new XElement("里急后重", "是") : null,
                                symptom.DigestiveOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.DigestiveOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "呼吸系统",
                                symptom.ShortnessOfBreath ? new XElement("呼吸短促", "是") : null,
                                symptom.Hemoptysis ? new XElement("咯血", "是") : null,
                                symptom.DifficultyBreathing ? new XElement("呼吸困难", "是") : null,
                                symptom.RespiratoryOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.RespiratoryOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "心脑血管系统",
                                symptom.ChestTightness ? new XElement("胸闷", "是") : null,
                                symptom.ChestPain ? new XElement("胸痛", "是") : null,
                                symptom.Palpitations ? new XElement("心悸", "是") : null,
                                symptom.BreathHard ? new XElement("气短", "是") : null,
                                symptom.CardiovascularOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.CardiovascularOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "泌尿系统",
                                symptom.ReducedUrineOutput ? new XElement("尿量减少", "是") : null,
                                symptom.BackKidneyPain ? new XElement("背部肾区疼痛", "是") : null,
                                symptom.BloodInTheUrine ? new XElement("尿中带血", "是") : null,
                                symptom.KidneyStones ? new XElement("肾结石", "是") : null,
                                symptom.UrinaryOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.UrinaryOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "神经系统",
                                symptom.Headache ? new XElement("头痛", "是") : null,
                                symptom.Dizziness ? new XElement("眩晕", "是") : null,
                                symptom.Coma ? new XElement("昏迷", "是") : null,
                                symptom.Convulsion ? new XElement("抽搐", "是") : null,
                                symptom.Horror ? new XElement("惊厥", "是") : null,
                                symptom.Delirium ? new XElement("谵妄", "是") : null,
                                symptom.Paralysis ? new XElement("瘫痪", "是") : null,
                                symptom.DifficultiesInSpeech ? new XElement("言语困难", "是") : null,
                                symptom.HardToSwallow ? new XElement("吞咽困难", "是") : null,
                                symptom.FeelingAbnormal ? new XElement("感觉异常", "是") : null,
                                symptom.MentalDisorder ? new XElement("精神失常", "是") : null,
                                symptom.Diplopia ? new XElement("复视", "是") : null,
                                symptom.BlurredVision ? new XElement("视物模糊", "是") : null,
                                symptom.LimbNumbness ? new XElement("麻木", "是") : null,
                                symptom.PeripheralSensoryDisorder ? new XElement("末梢感觉障碍", "是") : null,
                                symptom.PupilAbnormality ? new XElement
                                (
                                    "瞳孔异常",
                                    new XElement("状态", symptom.PupilStatus)
                                ) : null,
                                symptom.Acupuncture ? new XElement("针刺感", "是") : null,
                                symptom.Nerveother ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.NerveOtherInfo)
                                ) : null
                            ),
                            new XElement
                            (
                                "皮肤和皮下组织",
                                symptom.Itching ? new XElement("瘙痒", "是") : null,
                                symptom.BurningSensation ? new XElement("烧灼感", "是") : null,
                                symptom.Rash ? new XElement("皮疹", "是") : null,
                                symptom.BleedingPoint ? new XElement("出血点", "是") : null,
                                symptom.Jaundice ? new XElement("黄疸", "是") : null,
                                symptom.SkinOther ? new XElement
                                (
                                    "其他",
                                    new XElement("名称", symptom.SkinOtherInfo)
                                ) : null
                            )
                        ),
                        new XElement
                        (
                            "诊断结论",
                            initialDiagnosis.AcuteGastroenteritis ? new XElement("急性胃肠炎", "是") : null,
                            initialDiagnosis.InfectiousDiarrhea ? new XElement("感染性腹泻", "是") : null,
                            initialDiagnosis.PoisonousMushroomPoisoning ? new XElement("毒蘑菇中毒", "是") : null,
                            initialDiagnosis.BeanPoisoning ? new XElement("菜豆中毒", "是") : null,
                            initialDiagnosis.PufferfishPoisoning ? new XElement("河豚中毒", "是") : null,
                            initialDiagnosis.Botulism ? new XElement("肉毒中毒", "是") : null,
                            initialDiagnosis.NitritePoisoning ? new XElement("亚硝酸盐中毒", "是") : null,
                            initialDiagnosis.RhabdomyolysisSyndrome ? new XElement("横纹肌溶解综合征", "是") : null,
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
                            pastMedicalHistory.GeneralGastrointestinalInflammation ? new XElement("一般消化道炎症", "是") : null,
                            pastMedicalHistory.CrohnsDisease ? new XElement("克罗恩病", "是") : null,
                            pastMedicalHistory.GastrointestinalUlcer ? new XElement("消化道溃疡", "是") : null,
                            pastMedicalHistory.GastrointestinalCancer ? new XElement("消化道肿瘤", "是") : null,
                            pastMedicalHistory.IrritableBowelSyndrome ? new XElement("肠易激综合征", "是") : null,
                            pastMedicalHistory.Meningitis ? new XElement("脑膜炎脑肿瘤等", "是") : null,
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
                                string.IsNullOrEmpty(foodInfo.FoodBrand) ? null : new XElement("食品品牌", foodInfo.FoodBrand),
                                string.IsNullOrEmpty(foodInfo.Manufacturer) ? null : new XElement("生产厂家", foodInfo.Manufacturer),
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
