// Services/ProductService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Ottiene tutti i prodotti
    public async Task<List<Product>> GetProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Product>>("products");
    }

    // Ottiene il prodotto per ID
    public async Task<Product> GetProductAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Product>($"products/{id}");
    }

    // Crea un nuovo prodotto
    public async Task<Product> CreateProductAsync(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync("products", product);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    // Aggiorna un prodotto
    public async Task<Product> UpdateProductAsync(Product product)
    {
        var response = await _httpClient.PutAsJsonAsync($"products/{product.Id}", product);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    // Elimina un prodotto
    public async Task DeleteProductAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"products/{id}");
        response.EnsureSuccessStatusCode();
    }
}
