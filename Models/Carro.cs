using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Loja.Api.Models;

public class Carro
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string? IdModelo { get; set; }

    [BsonElement("Nome")]
    public string? NomeCarro { get; set; }
    
    public string? Renavam { get; set; }

    public string? Placa { get; set; }

    public string? Valor { get; set; }

    public string? Ano { get; set; }
}