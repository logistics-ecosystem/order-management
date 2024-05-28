using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Logistics.Models
{
    public class Deadline
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("dateTimeTo")]
        public DateTime DateTimeTo { get; set; }
    }
}
