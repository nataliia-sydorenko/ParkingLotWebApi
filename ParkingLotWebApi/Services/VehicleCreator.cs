using System;
using System.Linq;
using System.Threading;

namespace ParkingLotWebApi
{
    /// <summary>
    /// The class contains methods for parking vehicle.
    /// </summary>
    internal class VehicleCreator
    {
        private TimerCallback tm;
        private Timer timer;

        static VehicleCreator()
        {
            TransactionsService.LogTransactions();
        }

        /// <summary>
        /// Park vehicle and turn on meter.
        /// </summary>
        public void AddVehicle(Vehicle v)
        {
            if (Settings.capacity > Parking.GetInstance().vehicles.Count)
            {
                if (!VehicleViewer.IsParked(v.Id))
                {
                    Parking.GetInstance().vehicles.Add(v);
                    this.tm = new TimerCallback(this.GetPaid);
                    this.timer = new Timer(this.tm, v, 0, Settings.period);                    
                }
            }
        }

        /// <summary>
        /// Withdraw money from vehicle account.
        /// </summary>
        /// /// <param name="vehicle">Represents object of vehicle.</param>
        private void GetPaid(object vehicle)
        {
            Vehicle v = (Vehicle)vehicle;
            double sum = Settings.price;
            if (v.IsParked)
            {
                switch (v.Type)
                {
                    case Settings.VehicleType.Car: sum *= 2;
                        break;
                    case Settings.VehicleType.Bus: sum *= 3.5;
                        break;
                    case Settings.VehicleType.Truck: sum *= 5;
                        break;
                }
                if (v.Balance >= v.Rate)
                {
                    v.Balance -= sum;
                    Parking.GetInstance().Balance += sum;
                    Parking.GetInstance().transactions.Add(new Transaction() { Amount = sum, TimeOfTransaction = DateTime.Now, VehicleId = v.Id });
                }
                else
                {
                    sum *= Settings.penalty;
                    v.Balance -= sum;
                    Parking.GetInstance().Balance += sum;
                    Parking.GetInstance().transactions.Add(new Transaction() { Amount = sum, TimeOfTransaction = DateTime.Now, VehicleId = v.Id });
                }
            }
            else
            {
                this.timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }
    }
}
