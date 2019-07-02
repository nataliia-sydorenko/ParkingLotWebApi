using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ParkingLotWebApi
{
    internal class Parking
    {
        /// <value>Gets/sets the value of parking balance.</value>
        public double Balance { get; set; } = Settings.startBalance;
        public List<Vehicle> vehicles;
        public List<Transaction> transactions;

        private static Parking instance;

        private Parking()
        {
            this.vehicles = new List<Vehicle>();
            this.transactions = new List<Transaction>();
        }

        /// <summary>
        /// Get the instance of parking.
        /// </summary>
        /// <returns>Returns single instance of parking.</returns>
        public static Parking GetInstance()
        {
            if (instance == null)
            {
                instance = new Parking();
            }

            return instance;
        }
    }
}
