using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Loja.Api.Models;

public class Modelo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string? IdMarca { get; set; }
    
    [BsonElement("Nome")]
    public string? NomeModelo { get; set; }

}