using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class SystemAdmin : Person
    {
        public int adminID { get; set; }

        public static List<Customer> Customers { get; set; } = new List<Customer>();
        public static List<HotelOwner> HotelOwners { get; set; } = new List<HotelOwner>();
        public static List<TaxiDriver> TaxiDrivers { get; set; } = new List<TaxiDriver>();
        public static List<AirlineAdmin> AirlineAdmins { get; set; } = new List<AirlineAdmin>();
        public static List<Flight> Flights { get; set; } = new List<Flight>();
        public static List<Taxi> Taxis { get; set; } = new List<Taxi>();
        public static List<Room> Rooms { get; set; } = new List<Room>();
        public static void ManageUsers()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("===== Users List =====");
            Console.WriteLine();
            Console.WriteLine("---- Customers ----");
            foreach (var c in Customers)
                Console.WriteLine(value: $"- {c.name} (ID: {c.customerID})");
            Console.WriteLine("------------------------------------");

            Console.WriteLine("---- Hotel Owners ----");
            foreach (var h in HotelOwners)
                Console.WriteLine($"- {h.name} (ID: {h.ownerID})");
            Console.WriteLine();

            Console.WriteLine("---- Booked Rooms ----");
            foreach (var r in Rooms)
                Console.WriteLine($"-RoomID {r.roomID}");
            Console.WriteLine();
            Console.WriteLine("------------------------------------");

            Console.WriteLine("---- Taxi Drivers ----");
            foreach (var d in TaxiDrivers)
                Console.WriteLine($"- {d.name} (ID: {d.driverID})");
            Console.WriteLine();
            Console.WriteLine("---- Booked Taxis ----");
            foreach (var t in Taxis)
                Console.WriteLine($"- Taxi ID: {t.taxiID}");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("---- Airline Admins ----");
            foreach (var a in AirlineAdmins)
                Console.WriteLine($"- {a.name} (ID: {a.adminID})");
            Console.WriteLine();
            Console.WriteLine("---- Booked Flights ----");
            foreach (var f in Flights)
                Console.WriteLine($"- Flight ID: {f.flightID}");
            Console.WriteLine();
            Console.ResetColor();
        }
        public static void GenerateReports()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("===== System Reports =====");
            Console.WriteLine($"Number of Customers: {Customers.Count}");
            Console.WriteLine($"Number of Hotel Owners: {HotelOwners.Count}");
            Console.WriteLine($"Number of Taxi Drivers: {TaxiDrivers.Count}");
            Console.WriteLine($"Number of Airline Admins: {AirlineAdmins.Count}");
            Console.WriteLine($"Number of Booked Rooms: {Rooms.Count}");
            Console.WriteLine($"Number of Booked Flights: {Flights.Count}");
            Console.WriteLine($"Number of Booked Taxis: {Taxis.Count}");
        }
    }
}
