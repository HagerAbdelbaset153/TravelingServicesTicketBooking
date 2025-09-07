using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class AirlineAdmin : Person
    {
        public int adminID { get; set; }
        public string airlineName { get; set; }
        public List<string> Flights { get; set; } = new List<string>();

        public void AddFlight(string flightDetails)
        {
            Flights.Add(flightDetails);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Flight {flightDetails} has added.");
            Console.ResetColor();
        }
        public void UpdateFlight(int index, string newFlight)
        {
            if (index >= 0 && index < Flights.Count)
            {
                Flights[index] = newFlight;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Flight {Flights[index]} has modified to {newFlight}. ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Flight!");
                Console.ResetColor();
            }
        }
        public void CancelFlight(int index)
        {
            if (index >= 0 && index < Flights.Count)
            {
                Flights.Remove(Flights[index]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Flight {Flights[index]} has removed.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Flight!");
            }
        }
    }
}
