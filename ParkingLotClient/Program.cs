using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotClient
{
    class Program
    {
        private static string Menu { get; } = "\nChoose an action (press a number):\n" +
                       "1 - Show the balance of ParkingLot\n" +
                       "2 - Show the amount of money earned for last minute\n" +
                       "3 - Show a number of spare place\n" +
                       "4 - Show all transactions of ParkingLot for last minute\n" +
                       "5 - Show all history of transactions\n" +
                       "6 - Show all parked vehicles\n" +
                       "7 - Park a vehicle\n" +
                       "8 - Take a vehicle\n" +
                       "9 - Fund the account of the vehicle\n" +
                       "For exit press ESC\n";
        internal const string APP_PATH = "https://localhost:44391/api/";
        internal static HttpClient client = new HttpClient() { BaseAddress = new Uri(APP_PATH)};
        

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine(Menu);
                string choise = ReadInput();
                if (int.TryParse(choise, out int num) && Enumerable.Range(1, 9).Contains(num))
                {
                    switch (num)
                    {
                        case 1:
                            TransactionActions.GetBalance();
                            break;
                        case 2:
                            TransactionActions.GetLastMunuteIncome();
                            break;
                        case 3:
                            Console.WriteLine($"The parking lot has {10-VehicleActions.GetVehicles().Count} spare place(s)."); ;
                            break;
                        case 4:
                            foreach (var tr in TransactionActions.GetTransactions())
                            {
                                Console.WriteLine(tr);
                            }
                            break;
                        case 5:
                            foreach (var tr in TransactionActions.GetAllTransactions())
                            {
                                Console.WriteLine(tr);
                            }
                            break;
                        case 6:
                            foreach (var vehicle in VehicleActions.GetVehicles())
                            {
                                Console.WriteLine(vehicle);
                            }
                            break;
                        case 7:
                            VehicleActions.AddVehicle();
                            break;
                        case 8:
                            VehicleActions.TakeVehicle();
                            break;
                        case 9:
                            VehicleActions.PutVehicle();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Try again!");
                }
            }
        }



        /// <summary>
        /// Read data from console.
        /// </summary>
        private static string ReadInput()
        {
            string input = null;

            StringBuilder builder = new StringBuilder();

            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                builder.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }

            if (info.Key == ConsoleKey.Escape)
            {
                System.Environment.Exit(0);
            }
            else if (info.Key == ConsoleKey.Enter)
            {
                input = builder.ToString();
            }

            return input;
        }
    }
}

