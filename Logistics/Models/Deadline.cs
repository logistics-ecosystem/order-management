using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Logistics.Models
{
    public class Deadline
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("dateTimeTo")]
        public DateTime DateTimeTo { get; set; }
    }
}
