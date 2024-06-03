using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShoppingWebUI.Models
{
    public class UserMongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("PasswordSalt")]
        [BsonRepresentation(BsonType.Binary)]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("PasswordHash")]
        [BsonRepresentation(BsonType.Binary)]
        public byte[] PasswordHash { get; set; }

        [BsonElement("Status")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool Status { get; set; }

        [BsonElement("Balance")]
        [BsonRepresentation(BsonType.Double)]
        public double Balance { get; set; }

        //played bets 
        [BsonElement("PlayedBets")]
        public List<string> PlayedBets { get; set; } 
        [BsonElement("ComplatedBets")]
        public List<string> ComplatedBets { get; set; } 
    }
}
