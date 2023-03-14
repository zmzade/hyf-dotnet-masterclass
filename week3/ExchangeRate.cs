public class ExchangeRate
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }
    public decimal Rate { get; set; }
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