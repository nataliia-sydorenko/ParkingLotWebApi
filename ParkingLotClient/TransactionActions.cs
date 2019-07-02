using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotClient
{
    internal class TransactionActions
    {
        /// <summary>
        /// Get the list of transactions for last minute.
        /// </summary>
        /// <returns>Returns the list of transaction objects.</returns>
        internal static List<Transaction> GetTransactions()
        {
            List<Transaction> trs = null;
            var responseTask = Program.client.GetAsync("transactions/1");
            responseTask.Wait();
            var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Transaction>>();
                    readTask.Wait();

                    trs = readTask.Result;
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            
            return trs;
        }

        /// <summary>
        /// Get the list of all history of transactions.
        /// </summary>
        /// <returns>Returns the list of transactions strings.</returns>
        internal static List<string> GetAllTransactions()
        {
            List<string> transactions = null;
            using (new HttpClient() { BaseAddress = new Uri(Program.APP_PATH) })
            {
                var responseTask = Program.client.GetAsync("transactions");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<string>>();
                    readTask.Wait();

                    transactions = readTask.Result;
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
            return transactions;
        }

        /// <summary>
        /// Get the last minute income of parking lot.
        /// </summary>
        internal static void GetLastMunuteIncome()
        {
            double income = 0;
            foreach (var transaction in GetTransactions())
            {
                income += transaction.Amount;
            }
            Console.WriteLine($"The income of parking lot for last minute is {income}");
        }

        /// <summary>
        /// Get the balance of parking lot.
        /// </summary>
        internal static void GetBalance()
        {
            using (new HttpClient() { BaseAddress = new Uri(Program.APP_PATH) })
            {
                var responseTask = Program.client.GetAsync("balance");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<double>();
                    readTask.Wait();

                    var balance = readTask.Result;
                    Console.WriteLine($"Parking lot balance is {balance}");                   
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }

            }
        }
    }
}
