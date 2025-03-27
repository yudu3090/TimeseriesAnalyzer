namespace TimeseriesAnalyzer.Data;

public class MarketData
{
    public string Symbol { get; set; }
    public decimal LastPrice { get; set; }
    public decimal PriceChangePercent { get; set; }
}