namespace PortfolioValueCalculator;

static class Program
{

    public static void Main(string[] args)
    {
        var basePath = Path.Combine(AppContext.BaseDirectory, "data");
        var investments = new StreamReader(Path.Combine(basePath, "investments.csv"));
        var transactions = new StreamReader(Path.Combine(basePath, "transactions.csv"));
        var quotes = new StreamReader(Path.Combine(basePath, "quotes.csv"));
        var portfolioValueCalculator = new PortfolioValueCalculator(investments, transactions, quotes);
        var line = Console.ReadLine();
        while (!string.IsNullOrWhiteSpace(line))
        {
            var input = line.Split(";");
            var date = DateTime.Parse(input[0]);
            var investorId = input[1];
            var value = portfolioValueCalculator.Calculate(investorId, date);
            Console.WriteLine($"porfolio value: {value}");
            line = Console.ReadLine();
        }
    }
}