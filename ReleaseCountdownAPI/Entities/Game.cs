using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReleaseCountdownAPI.Entities
{
    public class Game
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Name { get; set; }
    }
}
