using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using CryptoPage.Models;
using CryptoPage.Views.ViewModels;
using System.Security.Cryptography.X509Certificates;

public class CryptoController : Controller
{
    private readonly CryptoService _cryptoService;

    public CryptoController(CryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    public async Task<IActionResult> Index( string search_CryptoName)
    {
        //List<BinanceTicker> data = await _cryptoService.getBinanceCoins(); //to get new coinlist from binance
        List<BinanceTicker> cryptos_binance = await _cryptoService.GetCryptoFromDatabaseAsync_binance();
       // await _cryptoService.SaveOrUpdateCryptoTable_binance(cryptos_binance); //to update data with new coin
        //List<CryptoCurrency> cryptos = await _cryptoService.GetCryptoPricesAsync(); //to get new coinlist from coingecko
        List<CryptoCurrency> cryptos = await _cryptoService.GetCryptoFromDatabaseAsync();
        //await _cryptoService.SaveOrUpdateCryptoTable(cryptos); //to update data with new coins
        var viewModel = new CryptoViewModels
        {
            Coin = cryptos,
            search_CryptoName = search_CryptoName
        };
        
        return View(viewModel);
    }
    public async Task<IActionResult> Filter(string search_CryptoName)
    {
        List<CryptoCurrency> cryptos = await _cryptoService.GetCryptoFromDatabaseAsync();


        if (!string.IsNullOrEmpty(search_CryptoName))
        {
            cryptos = cryptos
                .Where(c => c.Name.Contains(search_CryptoName, StringComparison.OrdinalIgnoreCase)
                ||  c.Symbol.Contains(search_CryptoName,StringComparison.OrdinalIgnoreCase)
                ||  c.Id.Contains(search_CryptoName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        var viewModel = new CryptoViewModels
        {
            Coin = cryptos,
            search_CryptoName = search_CryptoName
        };

        return PartialView("_CryptoTable", viewModel);
    }
    public async Task<IActionResult> Prices(string[] selectedIds)
    {
        
        System.Diagnostics.Debug.WriteLine("ajdis" + string.Join(", ",selectedIds));

        if (string.IsNullOrEmpty(selectedIds.ToString())) { 
            //return RedirectToAction("Index");
            }
        List<CryptoCurrency> cryptos = await _cryptoService.GetCryptoPricesAsync();
        
        var crypto = await _cryptoService.GetCryptoByIdAsync(string.Join(",", selectedIds));

        CryptoViewModels viewModel = new CryptoViewModels();

        viewModel.Coin = cryptos;
        viewModel.Price = crypto;

        return View(viewModel);
        
    }

    public async Task<IActionResult> Crypto_SideBySide()
    {
        List<BinanceTicker> cryptos_binance = await _cryptoService.GetCryptoFromDatabaseAsync_binance();
        List<CryptoCurrency> cryptos = await _cryptoService.GetCryptoFromDatabaseAsync();
        CryptoViewModels viewModel = new CryptoViewModels();
        
        viewModel.Coin = cryptos;
        viewModel.BinanceCoin = cryptos_binance; 

        return View("Crypto_SideBySide",viewModel);
    }
    
}