using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Logistics.Models
{
    public class Available
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid UniqueId { get; set; } = Guid.NewGuid();
        [BsonElement("addressFrom")]
        public string AddressFrom { get; set; }
        [BsonElement("addressTo")]
        public string AddressTo { get; set; }
        [BsonElement("dateTimeFrom")]
        public DateTime DateTimeFrom { get; set; }
        [BsonElement("dateTimeTo")]
        public DateTime DateTimeTo { get; set; }

        [BsonElement("frachtType")]
        public string FrachtType { get; set; }

        [BsonElement("distance")]
        public int Distance { get; set; }

        [BsonElement("trunkType")]
        public string TrunkType { get; set; }

        [BsonElement("weight")]
        public double Weight { get; set; }

        [BsonElement("loadingMetre")]
        public double LoadingMetre { get; set; }

        [BsonElement("height")]
        public double Height { get; set; }

        [BsonElement("loadingType")]
        public string LoadingType { get; set; }

        [BsonElement("temperature")]
        public int Temperature { get; set; }

        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("contactInfo")]
        public string ContactInfo { get; set; }
    }
}
