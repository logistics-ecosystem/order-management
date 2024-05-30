namespace Logistics.Models
{
    public class ParseOrder
    {
        public string AddressFrom { get; set; } = string.Empty;
        public string AddressTo { get; set; } = string.Empty;
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public string FrachtType { get; set; } = string.Empty;
        public int Distance { get; set; }
        public string TrunkType { get; set; } = string.Empty;
        public double Weight { get; set; }
        public double LoadingMetre { get; set; }
        public double Height { get; set; }
        public string LoadingType { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public double Price { get; set; }
        public string ContactInfo { get; set; } = string.Empty;

        public Boolean IsDataInvalid()
        {
            return AddressFrom == null ||
                AddressTo == null ||
                FrachtType == null ||
                Distance <= 0 ||
                TrunkType == null ||
                Weight <= 0 ||
                LoadingMetre <= 0 ||
                Height <= 0 ||
                LoadingType == null ||
                Price <= 0 ||
                ContactInfo == null;
        }

        public Available ToAvailable()
        {
            return new Available
            {
                AddressFrom = AddressFrom,
                AddressTo = AddressTo,
                DateTimeFrom = DateTimeFrom,
                DateTimeTo = DateTimeTo,
                FrachtType = FrachtType,
                Distance = Distance,
                TrunkType = TrunkType,
                Weight = Weight,
                LoadingMetre = LoadingMetre,
                Height = Height,
                LoadingType = LoadingType,
                Temperature = Temperature,
                Price = Price,
                ContactInfo = ContactInfo
            };
        }
        
        public override string ToString()
        {
            return $"{AddressFrom}, {AddressTo}, {DateTimeFrom.ToString()}, " +
                $"{DateTimeTo.ToString()}, {FrachtType}, {Distance}, {TrunkType}, " +
                $"{Weight}, {LoadingMetre}, {Height}, {LoadingType}, {Temperature}" +
                $"{Price}, {ContactInfo}";            
        }
    }
}
