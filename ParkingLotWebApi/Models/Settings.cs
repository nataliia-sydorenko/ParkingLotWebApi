namespace ParkingLotWebApi
{
    internal static class Settings
    {
        public static double startBalance = 0;
        public static int capacity = 10;
        public static int period = 5000;
        public static double price = 1;
        public static double penalty = 2.5;

        public enum VehicleType
        {
            Motorbyke,
            Car,
            Bus,
            Truck
        }
    }
}
