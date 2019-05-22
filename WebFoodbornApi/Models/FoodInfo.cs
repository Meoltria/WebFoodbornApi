using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFoodbornApi.Models
{
    public class FoodInfo
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

        public Patient Patient { get; set; }
    }
}
