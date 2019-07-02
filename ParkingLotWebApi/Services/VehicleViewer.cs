using System;
using System.Collections.Generic;

namespace ParkingLotWebApi
{
    /// <summary>
    /// The class contains methods for vehicle manupulation.
    /// </summary>
    internal class VehicleViewer
    {
        /// <summary>
        /// The list of parked vehicles.
        /// </summary>
        private static readonly List<Vehicle> Vehs = Parking.GetInstance().vehicles;

        /// <summary>
        /// Check whether the vehicle is parked.
        /// </summary>
        /// <returns>Returns true value if the vehicle is parked.</returns>
        /// <param name="id">Represents string value of vehicle license number.</param>
        public static bool IsParked(string id) => Vehs.Exists(x => x.Id.Contains(id));

        /// <summary>
        /// Find the vehicle by license number at parking lot.
        /// </summary>
        /// <returns>Returns parked Vehicle object.</returns>
        /// <param name="id">Represents string value of vehicle license number.</param>
        public static Vehicle FindVehicle(string id) => Vehs.Find(x => x.Id.Contains(id));

        
    }
}
