public class StockBroker
{
    public string BrokerName { get; set; }
    // The broker holds a list of Stocks (though not strictly needed in this lab)
    public List<Stock> stocks = new List<Stock>();
    // We'll write to "Lab1_output.txt" in the same folder as the .exe
    readonly string destPath =
    System.IO.Path.Combine(___________________________________, "Lab1_Output.txt");
    // We’ll print this header in both console & file
    public string titles =
    "Broker".PadRight(10) +
    "Stock".PadRight(15) +
    "Value".PadRight(10) +
    "Changes".PadRight(10) +
    "Date and Time";

    public StockBroker(string brokerName)
    {
        BrokerName = brokerName;
        // Print the header to console
        Console.WriteLine(titles);
        // Overwrite (false) the file with this same header once
        using (StreamWriter outputFile = new StreamWriter(destPath, false))
        {
            _______________________________________
        }
    }
    
    public void AddStock(Stock stock)
    {
        stocks.Add(stock);
        // Subscribe to the stock’s event using our event handler
        stock.______________ += ___________;
    }

    private _______ EventHandler(__________________________________)
    { if (sender is not null)
        // The second parameter needs to be cast to StockNotification
        _____ Helper(sender, (StockNotification)e);
    }
    
    public ____________ Helper(object sender, StockNotification e)
    {
        // We could cast the sender back to Stock if we needed more info
        Stock newStock = (Stock)sender;
        // Construct the output line
        string message =
        $"{BrokerName.PadRight(10)}" +
        $"{e.StockName.PadRight(15)}" +
        $"{e.CurrentValue.ToString().PadRight(10)}" +
        $"{e.NumChanges.ToString().PadRight(10)}" +
        $"{DateTime.Now}";
        try
{
// Append this line to the output file
using (StreamWriter outputFile = new StreamWriter(destPath, true))
{
________________________________________________________
}
// Also write to console
_____________________________________
}
catch (IOException ex)
{
// Handle or log any file I/O exceptions if needed
}
}
}
}