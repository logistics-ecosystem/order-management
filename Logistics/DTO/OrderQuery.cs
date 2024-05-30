namespace Logistics.DTO
{
    public class OrderQuery
    {
        public string? AddressFrom { get; set; } = null;
        public string? AddressTo { get; set; } = null;
        public DateTime? DateTimeFrom { get; set; } = null;
        public DateTime? DateTimeTo { get; set; } = null;
        public string? FrachtType { get; set; } = null;
        public int? Distance { get; set; } = null;
        public string? TrunkType { get; set; } = null;
        public double? Weight { get; set; } = null;
        public double? LoadingMetre { get; set; } = null;
        public double? Height { get; set; } = null;
        public string? LoadingType { get; set; } = null;
        public int? Temperature { get; set; } = null;
        public double? Price { get; set; } = null;
        public string? ContactInfo { get; set; } = null;
    }
}
