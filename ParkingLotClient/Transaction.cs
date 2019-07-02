﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotClient
{
    internal class Transaction
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
