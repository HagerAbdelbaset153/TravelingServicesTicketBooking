using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class Person
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public bool LogIn(string user , string pass)
        {
            if (user == userName && pass == password)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully logged in. ");
                Console.ResetColor();
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Username or password is incorrect!");
                Console.ResetColor();
                return false;
            }
        }
        public void LogOut()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Successfully logged out. ");
            Console.ResetColor();
        }
    }
}
