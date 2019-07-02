using System;

namespace ParkingLotWebApi
{
    /// <summary>
    /// The class contain method for taking vehicle from parking lot.
    /// </summary>
    public class VehicleRemover
    {
        /// <summary>
        /// Check whether the vehicle is parked.
        /// </summary>
        public static void TakeVehicle(Vehicle v)
        {
                if (VehicleViewer.IsParked(v.Id))
                {                    
                    v.IsParked = false;
                    Parking.GetInstance().vehicles.Remove(v);
                }
        }
    }
}
