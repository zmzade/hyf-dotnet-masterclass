public class ExchangeRate
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    private decimal _rate;
    public decimal Rate
    {
        get { return _rate; }
        set
        {
            if (value <= 0) throw new Exception("Rate should be positive");
            _rate = value;
        }
    }
    public ExchangeRate(string fromCurrency, string toCurrency)
    {
        FromCurrency = fromCurrency;
        ToCurrency = toCurrency;
    }

    public decimal Calculate(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount input should be positive.");
        return Rate * amount;
    }
}