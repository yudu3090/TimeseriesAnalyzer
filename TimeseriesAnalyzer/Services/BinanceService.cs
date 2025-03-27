using System.Net.Http.Json;
using Newtonsoft.Json;
using TimeseriesAnalyzer.Data;

namespace TimeseriesAnalyzer.Services;

public class BinanceService
{
    private readonly HttpClient _httpClient;

    public BinanceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetBitcoinPriceAsync(string symbol)
    {
        var response = await _httpClient.GetStringAsync($"https://api.binance.com/api/v3/ticker/price?symbol={symbol}");
        var priceData = JsonConvert.DeserializeObject<BinancePrice>(response);
        return decimal.Parse(priceData.Price);
    }
    
    public async Task<List<decimal>> GetBitcoinHistoricalPricesAsync(string symbol, string interval, int limit)
    {
        var url = $"https://api.binance.com/api/v3/klines?symbol={symbol}&interval={interval}&limit={limit.ToString()}";
        var response = await _httpClient.GetFromJsonAsync<List<List<object>>>(url);

        if (response == null) return new List<decimal>();
        try
        {

            return response.Select(candle => Convert.ToDecimal(candle[4].ToString())).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return response.Select(candle => Convert.ToDecimal(candle[4].ToString())).ToList();
    }
    
    public async Task<List<Candlestick>> GetBitcoinPriceTimeseriesAsync(string symbol, string interval, int limit)
    {
        var candlesticks = new List<Candlestick>();
        var response = await _httpClient.GetStringAsync($"https://api.binance.com/api/v3/klines?symbol={symbol}&interval={interval}&limit={limit.ToString()}");
        var data = JsonConvert.DeserializeObject<List<List<object>>>(response);

        if (data == null) return candlesticks;

        foreach (var item in data)
        {
            candlesticks.Add(new Candlestick
            {
                OpenTime = DateTimeOffset.FromUnixTimeMilliseconds((long)item[0]).DateTime,
                Open = decimal.Parse(item[1].ToString()),
                High = decimal.Parse(item[2].ToString()),
                Low = decimal.Parse(item[3].ToString()),
                Close = decimal.Parse(item[4].ToString()),
                Volume = decimal.Parse(item[5].ToString())
            });
        }

        return candlesticks;
    }
}
