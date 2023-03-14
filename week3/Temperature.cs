class Temperature
{
    private decimal _celsius;
    public decimal Celsius
    {
        get { return _celsius; }
        private set
        {
            if (value < 273.15m)
                throw new ArgumentException("celsius degree can not be less than 273.15 celsius.");
            _celsius = value;
        }
    }

    public decimal Fahrenheit { get => (Celsius * 9 / 5) + 32; }
    public decimal Kelvin { get => Celsius + 273.15m; }
    public Temperature(decimal celsius)
    {
        _celsius = celsius;
    }
}
