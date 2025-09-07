using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class Services
    {
        public static List<Flight> Flights { get; set; } = new List<Flight>();
        public static List<Taxi> Taxis { get; set; } = new List<Taxi>();
        public static List<Room> Rooms { get; set; } = new List<Room>();
    }
    class Flight
    {
        public int flightID { get; set; }
        public string airline { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arrivalTime { get; set; }
        public double price { get; set; }
        public int availableSeats { get; set; }

        public bool CheckAvailability()
        {
            return availableSeats > 0;
        }
        public void ReserveSeat()
        {
            if (availableSeats > 0)
            {
                availableSeats--;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully seat reserved. ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No seat is available! ");
                Console.ResetColor();

            }
        }

    }
    class Taxi
    {
        public int taxiID { get; set; }
        public string driver { get; set; }
        public string availableTime { get; set; }
        public int pricePerKm { get; set; }
        public bool isBooked { get; set; } = false;

        public void BookTaxi(string customerName)
        {
            if (! isBooked)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Taxi {taxiID} has booked by {customerName}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This taxi is not available!");
                Console.ResetColor();
            }
        }
        public void CancelTaxi(string customerName)
        {
            if (isBooked)
            {
                isBooked = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{customerName} cancelled booking for Taxi {taxiID}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Taxi {taxiID} was not booked!");
                Console.ResetColor();
            }
        }
       
    }
    class Room
    {
        public int roomID { get; set; }
        public string roomType { get; set; }
        public int price { get; set; }
        public bool isAvailable { get; set; } = true;

        public void BookRoom(string customerName)
        {
            if (isAvailable)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Room {roomID} ({roomType}) has booked by {customerName} ");
                Console.ResetColor();
                isAvailable = false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not available Room! ");
                Console.ResetColor();
            }
        }
        public void FreeRoom()
        {
            isAvailable = true;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Room {roomID} is available now. ");
            Console.ResetColor();
        }
    }
}
