using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Library.MongoService
{
    public abstract class IModel
    {
        /// <summary>
        /// The document id
        /// </summary>
        [BsonElement("_id")]
        [JsonPropertyName("id")]
        public ObjectId Id { get; set; }

        /// <summary>
        /// The document creation time
        /// </summary>
        [BsonElement("creation_date")]
        [JsonIgnore]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The document last update time
        /// </summary>
        [BsonElement("update_date")]
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; }
    }
}