// stockbroker.cs implementation
// Author: Matthew Bennett

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks; // added necessary statements

namespace Stock { // proper namespace
    public class StockBroker
    {
        public string BrokerName { get; set; }
        // The broker holds a list of Stocks (though not strictly needed in this lab)
        public List<Stock> stocks = new List<Stock>();
        // We'll write to "Lab1_output.txt" in the same folder as the .exe
        readonly string destPath =
        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab1_Output.txt"); // super long name
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
               outputFile.WriteLine(titles); // output to life
            }
        }

        public void AddStock(Stock stock)
        {
            stocks.Add(stock);
            // Subscribe to the stock’s event using our event handler
            stock.StockEvent += EventHandler;   // subscription
        }

        private async void EventHandler(object sender, EventArgs e) // async void for implementation
        { if (sender is not null)
                // The second parameter needs to be cast to StockNotification
                await Helper(sender, (StockNotification) e);
        }

        public async Task Helper(object sender, StockNotification e) // method needs to run asynchronously
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
                    await outputFile.WriteAsync(message);   // keeping it async
                }
                // Also write to console
                Console.WriteLine(message);
    }
            catch (IOException ex)
            {
                // Handle or log any file I/O exceptions if needed
                Console.WriteLine($"ERROR WRITING");
            }
        }
    }
}
