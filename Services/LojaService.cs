using Loja.Api.Configurations;
using Loja.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Loja.Api.Services;

public class LojaService
{
    private readonly IMongoCollection<Carro> _carroCollection;
    private readonly IMongoCollection<Modelo> _modeloCollection;
    private readonly IMongoCollection<Marca> _marcaCollection;

    public LojaService(IOptions<DatabaseSettings> databaseSettings)
    {
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _carroCollection = mongoDb.GetCollection<Carro>(databaseSettings.Value.CollectionsName[0]);
        _modeloCollection = mongoDb.GetCollection<Modelo>(databaseSettings.Value.CollectionsName[1]);
        _marcaCollection = mongoDb.GetCollection<Marca>(databaseSettings.Value.CollectionsName[2]);
    }
    
    // // // MARCAS

    public async Task<List<Marca>> GetAllMarcasAsync() =>
        await _marcaCollection.Find(m => true).ToListAsync();

    public async Task<Marca> GetMarcaByIdAsync(string id) =>
        await _marcaCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task<bool> CreateMarcaAsync(Marca marca)
    {
        await _marcaCollection.InsertOneAsync(marca);
        if (await _marcaCollection.Find(m => m.Id == marca.Id).FirstOrDefaultAsync() != default)
        {
            return true;
        }

        return false;
    }
    
    public async Task<bool> UpdateMarcaAsync(Marca marca)
    {
        await _marcaCollection.ReplaceOneAsync(m => m.Id == marca.Id, marca);
        var marcaFromDb = await _marcaCollection.Find(m => m.Id == marca.Id).FirstOrDefaultAsync();
        if (marcaFromDb.Id == marca.Id && marcaFromDb.NomeMarca == marca.NomeMarca)
        {
            return true;
        }

        return false;
    }
    
    public async Task<bool> DeleteMarcaAsync(string id)
    {
        await _marcaCollection.DeleteOneAsync(m => m.Id == id);
        if (await _marcaCollection.Find(m => m.Id == id).FirstOrDefaultAsync() == default)
        {
            return true;
        }
        return false;
    }
    
    // // // MODELO
    
    public async Task<List<Modelo>> GetAllModelosAsync() =>
        await _modeloCollection.Find(m => true).ToListAsync();

    public async Task<Modelo> GetModeloByIdAsync(string id) =>
        await _modeloCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task<bool> CreateModeloAsync(Modelo modelo)
    {
        await _modeloCollection.InsertOneAsync(modelo);
        if (await _modeloCollection.Find(m => m.Id == modelo.Id).FirstOrDefaultAsync() != default)
        {
            return true;
        }

        return false;
    }
    
    public async Task<bool> UpdateModeloAsync(Modelo modelo)
    {
        await _modeloCollection.ReplaceOneAsync(m => m.Id == modelo.Id, modelo);
        var modeloFromDb = await _modeloCollection.Find(m => m.Id == modelo.Id).FirstOrDefaultAsync();
        if (modeloFromDb.Id == modelo.Id && modeloFromDb.NomeModelo == modelo.NomeModelo)
        {
            return true;
        }

        return false;
    }
    
    public async Task<bool> DeleteModeloAsync(string id)
    {
        await _modeloCollection.DeleteOneAsync(m => m.Id == id);
        if (await _modeloCollection.Find(m => m.Id == id).FirstOrDefaultAsync() == default)
        {
            return true;
        }
        return false;
    }
    
    // // // CARRO
    
    public async Task<List<Carro>> GetAllCarrosAsync() =>
        await _carroCollection.Find(m => true).ToListAsync();

    public async Task<Carro> GetCarroByIdAsync(string id) =>
        await _carroCollection.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task<CarroCompleto> GetCarroCompleto(string id)
    {
        Carro carroFromDb = await GetCarroByIdAsync(id);
        Modelo modeloFromDb = default!;
        Marca marcaFromDb = default!;


        if (carroFromDb == default!)
        {
            return default!;
        }

        if (carroFromDb.IdModelo != default)
        {
            modeloFromDb = await GetModeloByIdAsync(carroFromDb.IdModelo);
        }
        
        if (modeloFromDb.IdMarca != default)
        {
            marcaFromDb = await GetMarcaByIdAsync(modeloFromDb.IdMarca);
        }

        return new CarroCompleto()
        {
            Carro = carroFromDb,
            Modelo = modeloFromDb,
            Marca = marcaFromDb
        };
    }

    public async Task<bool> CreateCarroAsync(Carro carro)
    {
        await _carroCollection.InsertOneAsync(carro);
        if (await _carroCollection.Find(m => m.Id == carro.Id).FirstOrDefaultAsync() != default)
        {
            return true;
        }

        return false;
    }
    
    public async Task<bool> UpdateCarroAsync(Carro carro)
    {
        await _carroCollection.ReplaceOneAsync(m => m.Id == carro.Id, carro);
        var carroFromDb = await _carroCollection.Find(m => m.Id == carro.Id).FirstOrDefaultAsync();
        if (carroFromDb.Id == carro.Id && carroFromDb.NomeCarro == carro.NomeCarro)
        {
            return true;
        }

        return false;
    }
    
    public async Task<bool> DeleteCarroAsync(string id)
    {
        await _carroCollection.DeleteOneAsync(m => m.Id == id);
        if (await _carroCollection.Find(m => m.Id == id).FirstOrDefaultAsync() == default)
        {
            return true;
        }
        return false;
    }

    



}