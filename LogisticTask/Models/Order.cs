using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LogisticTask.Models
{


    public class Order
    {
        [Key]
        public Guid UniqueId { get; set; } // Генерация UUID
        public string AddressFrom { get; set; } = string.Empty;
        public string AddressTo { get; set; } = string.Empty;
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string FrachtType { get; set; } = string.Empty; // LTL, FTL, Multifracht
        public int Distance { get; set; } // в метрах
        public string TrunkType { get; set; } = string.Empty;
        public double Weight { get; set; } // в тоннах
        public double LoadingMetre { get; set; } // LDM
        public double Height { get; set; } // в метрах
        public string LoadingType { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public float Price { get; set; }
        public string ContactInfo { get; set; } = string.Empty;

        
    }

}
