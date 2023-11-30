using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Loja.Api.Models;

public class Marca
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("Nome")]
    public string? NomeMarca { get; set; }
}