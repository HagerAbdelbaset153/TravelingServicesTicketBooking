using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class TaxiDriver : Person
    {
        public int driverID { get; set; }
        public string carDetails { get; set; }
        public List<string> schedule { get; set; } = new List<string>();

        public void UpdateAvailability(string availableTime)
        {
            schedule.Add(availableTime);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Successfully added {availableTime} time. ");
            Console.ResetColor();
        }
        public void AcceptBooking(string customerName, string tripTime)
        {
            if (schedule.Contains(tripTime))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{customerName} booking at {tripTime} is accepted. ");
                Console.ResetColor();
                schedule.Remove(tripTime);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{tripTime} is invalid! ");
            }
        }
    }
}
