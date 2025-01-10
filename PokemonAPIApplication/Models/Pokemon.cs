using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonAPIApplication.Models
{
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string? Name { get; set; }
        [BsonElement("Type")]
        public string? Type { get; set; }
        [BsonElement("Ability")]
        public string? Ability { get; set; }
        [BsonElement("Level")]
        public int? Level { get; set; }
    }
}
