using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class HotelOwner : Person
    {
        public int ownerID { get; set; }
        public List<string> hotelList { get; set; } = new List<string>();

        public void RegisterHotel(string hotelName)
        {
            hotelList.Add(hotelName);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Hotel {hotelName} is registered. ");
            Console.ResetColor();
        }
        public void UpdateHotelDetails(int index, string newName)
        {
            if (index >= 0 && index < hotelList.Count)
            {
                hotelList[index] = newName;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Successfully hotel {hotelList[index]} is modified to {newName}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid hotel! ");
                Console.ResetColor();
            }
        }
        public void RemoveHotel(int index)
        {
            if (index >= 0 && index < hotelList.Count)
            {
                hotelList.RemoveAt(index);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Hotel {hotelList[index]} is removed. ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid hotel! ");
                Console.ResetColor();
            }
        }
    }
}
