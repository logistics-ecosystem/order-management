using LogisticTask.Models;
using System.ComponentModel.DataAnnotations;

namespace LogisticTask.DTO
{
    public class OrderDTO
    {
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string FrachtType { get; set; } // LTL, FTL, Multifracht

        //[Range(0, int.MaxValue)]
        public int Distance { get; set; } // в метрах
        public string TrunkType { get; set; }

        //[Range(0, int.MaxValue)]
        public double Weight { get; set; } // в тоннах

        //[Range(0, int.MaxValue)]
        public double LoadingMetre { get; set; } // LDM

        //[Range(0, int.MaxValue)]
        public double Height { get; set; } // в метрах
        public string LoadingType { get; set; }
        public int Temperature { get; set; }

        //[Range(0, int.MaxValue)]
        public float Price { get; set; }
        public string ContactInfo { get; set; }
    }
}
