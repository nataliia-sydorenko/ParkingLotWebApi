using System;

namespace ParkingLotWebApi
{
    public class Transaction
    {
        /// <value>Gets/sets the value of transaction data.</value>
        public DateTime TimeOfTransaction { get; set; }

        /// <value>Gets/sets the string value of vehicle license number.</value>
        public string VehicleId { get; set; }

        /// <value>Gets/sets the value of tranaction sum.</value>
        public double Amount { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.TimeOfTransaction + " " + this.VehicleId + " " + this.Amount;
        }
    }
}
