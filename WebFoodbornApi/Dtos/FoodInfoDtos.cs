using System;
using System.ComponentModel.DataAnnotations;

namespace WebFoodbornApi.Dtos
{
    public class FoodInfoOutput
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string FoodName { get; set; }
        public string FoodType { get; set; }
        public string FoodPackaging { get; set; }
        public string FoodBrand { get; set; }
        public string Manufacturer { get; set; }
        public string EatingPlace { get; set; }
        public string PurchasePlace { get; set; }
        public string EatingBorderland { get; set; }
        public string EatingProvinceCityDistrict { get; set; }
        public string EatingAddress { get; set; }
        public string PurchaseBorderland { get; set; }
        public string PurchaseProvinceCityDistrict { get; set; }
        public string PurchaseAddress { get; set; }
        public string EatingCounts { get; set; }
        public DateTime EatingTime { get; set; }
        public string IsOtherPeople { get; set; }
        public string Status { get; set; }
    }

    public class FoodInfoQueryInput
    {
        public int PatientId { get; set; }
    }

    public class FoodInfoCreateInput
    {
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "请输入食品名称")]
        public string FoodName { get; set; }
        [Required(ErrorMessage = "请选择食品分类")]
        public string FoodType { get; set; }
        [Required(ErrorMessage = "请选择食品包装/加工方式")]
        public string FoodPackaging { get; set; }
        public string FoodBrand { get; set; }
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "请选择进食场所")]
        public string EatingPlace { get; set; }
        [Required(ErrorMessage = "请选择购买场所")]
        public string PurchasePlace { get; set; }
        public string EatingBorderland { get; set; }
        public string EatingProvinceCityDistrict { get; set; }
        [Required(ErrorMessage = "请输入进食地址")]
        public string EatingAddress { get; set; }
        public string PurchaseBorderland { get; set; }
        public string PurchaseProvinceCityDistrict { get; set; }
        [Required(ErrorMessage = "请输入购买地址")]
        public string PurchaseAddress { get; set; }
        [Required(ErrorMessage = "请输入进食人数")]
        public string EatingCounts { get; set; }
        [Required(ErrorMessage = "请输入进食时间")]
        public DateTime EatingTime { get; set; }
        public string IsOtherPeople { get; set; }
        public string Status { get; set; }
    }

    public class FoodInfoUpdateInput
    {
        [Required(ErrorMessage = "Id不能为空")]
        public int Id { get; set; }
        [Required(ErrorMessage = "请输入患者Id")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "请输入食品名称")]
        public string FoodName { get; set; }
        [Required(ErrorMessage = "请选择食品分类")]
        public string FoodType { get; set; }
        [Required(ErrorMessage = "请选择食品包装/加工方式")]
        public string FoodPackaging { get; set; }
        public string FoodBrand { get; set; }
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "请选择进食场所")]
        public string EatingPlace { get; set; }
        [Required(ErrorMessage = "请选择购买场所")]
        public string PurchasePlace { get; set; }
        public string EatingBorderland { get; set; }
        public string EatingProvinceCityDistrict { get; set; }
        [Required(ErrorMessage = "请输入进食地址")]
        public string EatingAddress { get; set; }
        public string PurchaseBorderland { get; set; }
        public string PurchaseProvinceCityDistrict { get; set; }
        [Required(ErrorMessage = "请输入购买地址")]
        public string PurchaseAddress { get; set; }
        [Required(ErrorMessage = "请输入进食人数")]
        public string EatingCounts { get; set; }
        [Required(ErrorMessage = "请输入进食时间")]
        public DateTime EatingTime { get; set; }
        public string IsOtherPeople { get; set; }
        public string Status { get; set; }
    }
}
