using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class Booking
    {
        public static int bookingID { get; set; }
        public static string customer { get; set; }
        public DateTime date { get; set; }
        public static string status { get; set; } 

        public static void ConfirmBooking()
        {
            status = "confirmed";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{bookingID} booking has confirmed to {customer}");
            Console.ResetColor();
        }
        public static void CancelBooking()
        {
            status = "cancelled";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{bookingID} booking has cancelled to {customer}");
            Console.ResetColor();
        }
    }
    class FlightBooking : Booking
    {
        public string flight { get; set; }

        public void GenerateTicket()
        {
            if (status == "confirmed")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("======================");
                Console.WriteLine("=== A plane Ticket ===");
                Console.WriteLine("======================");
                Console.WriteLine($"Customer Name: {customer} \nFlight: {flight} \nBooking Number: {bookingID}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You must confirm booking to get ticket! ");
                Console.ResetColor();
            }
        }
    }
    class TaxiBooking : Booking
    {
        public string taxi;
        public string pickupLocation;
        public string dropLocation;

    }
    class HotelBooking : Booking
    {
        public string room;
        public string checkInDate;
        public string checkOutDate;
    }
}
