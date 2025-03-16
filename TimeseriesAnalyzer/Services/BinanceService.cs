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

    public async Task<decimal> GetBitcoinPriceAsync()
    {
        var response = await _httpClient.GetStringAsync("https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT");
        var priceData = JsonConvert.DeserializeObject<BinancePrice>(response);
        return decimal.Parse(priceData.Price);
    }
}
