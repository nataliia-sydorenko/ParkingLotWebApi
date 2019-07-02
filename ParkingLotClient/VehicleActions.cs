using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ParkingLotClient
{
    class VehicleActions
    {
        /// <summary>
        /// Park vehicle and turn on meter.
        /// </summary>
        internal static void AddVehicle()
        {
            try
            {
                var vehicle = CreateVehicle();

                var postTask = Program.client.PostAsJsonAsync<Vehicle>("vehicles", vehicle);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{vehicle.Type} with license number {vehicle.Id} was added.");
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
            catch (Exception)
            {

                Console.WriteLine($"Vehicle has already been parked or parking lot is full.");
            }
            
        }

        /// <summary>
        /// Read vehicle info from console.
        /// </summary>
        /// <param name="type">Represents number value of vehicle type.</param>
        /// <param name="balance">Represents double value of vehicle balance.</param>
        /// <param name="licenseNumber">Represents string value of vehicle license number.</param>
        public static void ReadInfo(out int type, out double balance, out string licenseNumber)
        {
            var incorrectInput = "Incorrect input, try again!";
        input1: Console.WriteLine("Choose the type of your vehicle (press a number):\n" +
            "1 - Motorbike\n" +
            "2 - Car\n" +
            "3 - Bus\n" +
            "4 - Truck");
            string num = Console.ReadLine();
            if (!int.TryParse(num, out type) & !Enumerable.Range(1, 4).Contains(type))
            {
                Console.WriteLine(incorrectInput);
                goto input1;
            }

            Console.WriteLine("Enter your license number:");
            licenseNumber = Console.ReadLine();
        input2: Console.WriteLine("Enter the account balance of your vehicle:");
            string bal = Console.ReadLine();
            if (!double.TryParse(bal, out balance))
            {
                Console.WriteLine(incorrectInput);
                goto input2;
            }
        }

        /// <summary>
        /// Create new object of vehicle.
        /// </summary>
        /// <returns>Returns created object of vehicle.</returns>
        internal static Vehicle CreateVehicle()
        {
            var vehicle = new Vehicle();
            ReadInfo(out int type, out double balance, out string licenseNumber);
            vehicle.Id = licenseNumber;
            vehicle.Balance = balance;
            switch (type)
            {
                case 1:
                    vehicle.Type = VehicleType.Motorbyke;
                    break;
                case 2:
                    vehicle.Type = VehicleType.Car;
                    break;
                case 3:
                    vehicle.Type = VehicleType.Bus;
                    break;
                case 4:
                    vehicle.Type = VehicleType.Truck;
                    break;
            }
            vehicle.IsParked = true;

            return vehicle;
        }

        /// <summary>
        /// Get the list of parked vehicles.
        /// </summary>
        /// <returns>Returns list of parked vehicles.</returns>
        internal static List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicles = null;
            using (new HttpClient() { BaseAddress = new Uri(Program.APP_PATH) })
            {
                var responseTask = Program.client.GetAsync("vehicles");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Vehicle>>();
                    readTask.Wait();

                    vehicles = readTask.Result;                   
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
            return vehicles;
        }

        /// <summary>
        /// Remove vehicle from parking lot.
        /// </summary>
        internal static void TakeVehicle()
        {
            try
            {
                var vehicle = GetVehicle();
                if (vehicle.Balance > 0)
                {
                    var deleteTask = Program.client.DeleteAsync("vehicles/" + vehicle.Id);
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Vehicle with license number {vehicle.Id} was taken.");
                    }
                    else
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
                else
                {
                    Console.WriteLine($"You have a debt for parking in amount of { vehicle.Balance}. Please fund your account.");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Vehicle is not found.");
            }
            
        }

        /// <summary>
        /// Get the vehicle by license number.
        /// </summary>
        /// <returns>Returns list of parked vehicles.</returns>
        internal static Vehicle GetVehicle()
        {
            Console.WriteLine("Enter the license number of your vehicle:");
            string id = Console.ReadLine();
            Vehicle vehicle = null;
            try
            {
                var responseTask = Program.client.GetAsync("vehicles/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Vehicle>();
                    readTask.Wait();

                    vehicle = readTask.Result;
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Vehicle is not found.");
            }
            return vehicle;
        }

        /// <summary>
        /// Fund vehicle account.
        /// </summary>
        internal static void PutVehicle()
        {
            Console.WriteLine("Enter the license number of your vehicle:");
            string id = Console.ReadLine();
            Console.WriteLine("Enter the amount of money you want to fund: ");
            try
            {
            input: if (double.TryParse(Console.ReadLine(), out double fund))
                {
                    var responseTask = Program.client.PutAsJsonAsync("vehicles/" + id, fund);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"{fund} was added to your account.");
                    }
                    else
                    {
                        Console.WriteLine(result.StatusCode);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Try again!");
                    goto input;
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Vehicle is not found.");
            }
        
        }
    }
}
