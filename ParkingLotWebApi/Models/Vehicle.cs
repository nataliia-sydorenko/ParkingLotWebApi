using Newtonsoft.Json;

namespace ParkingLotWebApi
{
    public class Vehicle
    {
        /// <value>Gets/sets the string value of vehicle license number.</value>
        [JsonProperty("id")]
        internal string Id { get; set; }

        /// <value>Gets/sets the start value of vehicle balance.</value>
        [JsonProperty("balance")]
        internal double Balance { get; set; }

        /// <value>Gets/sets the value of rate for parking.</value>
        [JsonProperty("rate")]
        internal virtual double Rate { get; set; }

        /// <value>Gets/sets the true value if vehicle is parked.</value>
        [JsonProperty("isparked")]
        internal bool IsParked { get; set; }

        /// <value>Gets/sets the type of vehicle.</value>
        [JsonProperty("type")]
        internal Settings.VehicleType Type { get; set; }

    }
}
