namespace Logistics.DTO
{
    public class OrderDTO
    {
        public string AddressFrom { get; set; } = string.Empty;
        public string AddressTo { get; set; } = string.Empty;
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string FrachtType { get; set; } = string.Empty; // LTL, FTL, Multifracht

        //[Range(0, int.MaxValue)]
        public int Distance { get; set; }
        public string TrunkType { get; set; } = string.Empty;

        //[Range(0, int.MaxValue)]
        public double Weight { get; set; }

        //[Range(0, int.MaxValue)]
        public double LoadingMetre { get; set; }

        //[Range(0, int.MaxValue)]
        public double Height { get; set; }
        public string LoadingType { get; set; } = string.Empty;
        public int Temperature { get; set; }

        //[Range(0, int.MaxValue)]
        public float Price { get; set; }
        public string ContactInfo { get; set; } = string.Empty;
    }
}
