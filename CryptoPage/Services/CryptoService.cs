using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using CryptoPage.Models;
using System.Text.Json.Serialization;
using RestSharp;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class CryptoService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public CryptoService(HttpClient httpClient, AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<List<CryptoCurrency>> GetCryptoPricesAsync()
    {
        var options = new RestClientOptions("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&x_cg_demo_api_key=CG-9HeVx6DZJNo3NSEetuGndomj");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        var response = await client.GetAsync(request);


        var coinList = JsonSerializer.Deserialize<List<CryptoCurrency>>(response.Content);

        return coinList;
    }
    public async Task<Dictionary<string,CryptoDetails>> GetCryptoByIdAsync(string id)
    {
        var options = new RestClientOptions($"https://api.coingecko.com/api/v3/simple/price?vs_currencies=usd&ids={id}&x_cg_demo_api_key=CG-9HeVx6DZJNo3NSEetuGndomj");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        var response = await client.GetAsync(request);

        if (string.IsNullOrWhiteSpace(response.Content))
            return null;

        var data = JsonSerializer.Deserialize<Dictionary<string,CryptoDetails>>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if(data != null)
        {
            foreach (var key in data.Keys.ToList())
                data[key].Name = key;
        }

        return data;
    }

    public async Task SaveOrUpdateCryptoTable(List<CryptoCurrency> cryptosList)
    {
        if (cryptosList == null || cryptosList.Count == 0) return;

        foreach(var coin in cryptosList)
        {
            var existiCoin = await _context.Cryptos.FindAsync(coin.Id);
            if (existiCoin == null)
            {
                _context.Cryptos.Add(coin);
            }
            else
            {
                existiCoin.Name = coin.Name;
                existiCoin.Symbol = coin.Symbol;
            }
        }
        await _context.SaveChangesAsync();

    }
    public async Task<List<CryptoCurrency>> GetCryptoFromDatabaseAsync()
    {
        return await _context.Cryptos.ToListAsync();
    }

}

