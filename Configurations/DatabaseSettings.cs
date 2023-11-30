namespace Loja.Api.Configurations;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public List<string> CollectionsName { get; set; } = default!;
}