using System.ComponentModel.DataAnnotations;

namespace Logistics.Models
{
    public class Order
    {
        [Key]
        public Guid UniqueId { get; set; }
        public string AddressFrom { get; set; } = string.Empty;
        public string AddressTo { get; set; } = string.Empty;
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string FrachtType { get; set; } = string.Empty; // LTL, FTL, Multifracht
        public int Distance { get; set; } // meters
        public string TrunkType { get; set; } = string.Empty;
        public double Weight { get; set; } // tons
        public double LoadingMetre { get; set; } // LDM
        public double Height { get; set; } // meters
        public string LoadingType { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public float Price { get; set; }
        public string ContactInfo { get; set; } = string.Empty;        
    }
}
