using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelingServicesTicketBooking
{
    class Program
    {
        static void CustomerMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your ID: ");
            int id = int.Parse(Console.ReadLine());

            Customer cust = new Customer { name = name, customerID = id };
            SystemAdmin.Customers.Add(cust);
            Console.WriteLine();
            Console.WriteLine("--- Customer Menu ---");
            Console.WriteLine("1. Search Flights");
            Console.WriteLine("2. Book Flight");
            Console.WriteLine("3. Book Hotel");
            Console.WriteLine("4. Book Taxi");
            Console.Write("Your Choice: ");
            string c = Console.ReadLine();

            switch (c)
            {
                case "1":
                    foreach (var f in Services.Flights)
                    {
                        Console.WriteLine($"{f.flightID} - {f.origin} to {f.destination} - {f.price}$ ({f.availableSeats} seats)");
                    }
                    break;

                case "2":
                    Console.Write("Enter Flight ID to book: ");
                    int fid = int.Parse(Console.ReadLine());
                    Flight flight = Services.Flights.Find(x => x.flightID == fid);
                    if (flight != null && flight.CheckAvailability())
                    {
                        flight.ReserveSeat();
                        Booking.bookingID++;
                        Booking.customer = cust.name;
                        Booking.ConfirmBooking();
                        FlightBooking fb = new FlightBooking { flight = flight.airline };
                        fb.GenerateTicket();
                    }
                    else
                    {
                        Console.WriteLine("Flight not available!");
                    }
                    break;

                case "3": 
                    var availableRooms = Services.Rooms.Where(r => r.isAvailable).ToList();
                    if (availableRooms.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("No available rooms right now.");
                        Console.ResetColor();
                        break;
                    }

                    Console.WriteLine("Available Rooms:");
                    foreach (var r in availableRooms)
                    {
                        Console.WriteLine($"ID: {r.roomID} - {r.roomType} - {r.price}$");
                    }

                    Console.Write("Enter Room ID to book: ");
                    int rid = int.Parse(Console.ReadLine());
                    var roomToBook = Services.Rooms.Find(r => r.roomID == rid);

                    if (roomToBook != null && roomToBook.isAvailable)
                    {
                        Console.Write("Enter Check-in Date (e.g. 2025-09-10): ");
                        string checkIn = Console.ReadLine();
                        Console.Write("Enter Check-out Date (e.g. 2025-09-12): ");
                        string checkOut = Console.ReadLine();

                        roomToBook.isAvailable = false;

                        Booking.bookingID++;
                        Booking.customer = cust.name;
                        Booking.ConfirmBooking();

                        HotelBooking hb = new HotelBooking
                        {
                            room = $"{roomToBook.roomType} (ID {roomToBook.roomID})",
                            checkInDate = checkIn,
                            checkOutDate = checkOut
                        };

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Room booked: {hb.room} from {hb.checkInDate} to {hb.checkOutDate}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Room not available or invalid ID!");
                        Console.ResetColor();
                    }
                    break;

                case "4": 
                    if (Services.Taxis == null || Services.Taxis.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("No taxis available.");
                        Console.ResetColor();
                        break;
                    }

                    Console.WriteLine("Available Taxis:");
                    foreach (var t in Services.Taxis)
                    {
                        Console.WriteLine($"ID: {t.taxiID} - Time: {t.availableTime} - {t.pricePerKm} per Km");
                    }

                    Console.Write("Enter Taxi ID to book: ");
                    int tid = int.Parse(Console.ReadLine());
                    var taxiToBook = Services.Taxis.Find(t => t.taxiID == tid);

                    if (taxiToBook != null)
                    {
                        if (string.IsNullOrWhiteSpace(taxiToBook.driver))
                        {
                            Console.Write("Enter Driver Name: ");
                            taxiToBook.driver = Console.ReadLine();
                        }

                        Console.Write("Enter Pickup Location: ");
                        string pickup = Console.ReadLine();
                        Console.Write("Enter Drop Location: ");
                        string drop = Console.ReadLine();

                        Booking.bookingID++;
                        Booking.customer = cust.name;
                        Booking.ConfirmBooking();

                        TaxiBooking tb = new TaxiBooking
                        {
                            taxi = $"Taxi #{taxiToBook.taxiID}",
                            pickupLocation = pickup,
                            dropLocation = drop
                        };

                        
                        taxiToBook.BookTaxi(cust.name);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Taxi booked: {tb.taxi} from {tb.pickupLocation} to {tb.dropLocation}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Taxi not found!");
                        Console.ResetColor();
                    }
                    break;

                default:
                    Console.WriteLine("Invalid Choice! ");
                    break;
            }
            Console.ResetColor();
        }
        static void AirlineAdminMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter Admin Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Airline Name: ");
            string airline = Console.ReadLine();

            AirlineAdmin admin = new AirlineAdmin { adminID = 1, name = name, airlineName = airline };
            SystemAdmin.AirlineAdmins.Add(admin);

            Console.WriteLine("--- Airline Admin Menu ---");
            Console.WriteLine("1. Add Flight");
            Console.WriteLine("2. Update Flight");
            Console.WriteLine("3. Cancel Flight");
            Console.Write("Your Choice: ");
            string c = Console.ReadLine();

            switch (c)
            {
                case "1":
                    Console.Write("Enter Origin: ");
                    string origin = Console.ReadLine();
                    Console.Write("Enter Destination: ");
                    string dest = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    double price = double.Parse(Console.ReadLine());
                    Console.Write("Enter Seats: ");
                    int seats = int.Parse(Console.ReadLine());

                    Flight newFlight = new Flight
                    {
                        flightID = Services.Flights.Count + 1,
                        airline = airline,
                        origin = origin,
                        destination = dest,
                        price = price,
                        availableSeats = seats,
                        departureTime = DateTime.Now.AddHours(2),
                        arrivalTime = DateTime.Now.AddHours(5)
                    };

                    Services.Flights.Add(newFlight);
                    admin.AddFlight($"{origin}-{dest}");
                    break;

                case "2":
                    Console.Write("Enter Flight index to update: ");
                    int idx = int.Parse(Console.ReadLine());
                    Console.Write("Enter new details: ");
                    string nd = Console.ReadLine();
                    admin.UpdateFlight(idx, nd);
                    break;

                case "3":
                    Console.Write("Enter Flight index to cancel: ");
                    int ci = int.Parse(Console.ReadLine());
                    admin.CancelFlight(ci);
                    break;

                default:
                    Console.WriteLine("Invalid Choice! ");
                    break;
            }
            Console.ResetColor();
        }
        static void HotelOwnerMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter Owner Name: ");
            string name = Console.ReadLine();
            HotelOwner owner = new HotelOwner { ownerID = 1, name = name };
            SystemAdmin.HotelOwners.Add(owner);
            Console.WriteLine();

            Console.WriteLine("--- Hotel Owner Menu ---");
            Console.WriteLine("1. Register Hotel");
            Console.WriteLine("2. Update Hotel");
            Console.WriteLine("3. Remove Hotel");
            Console.WriteLine("4. Add Room");
            Console.Write("Your Choice: ");
            string c = Console.ReadLine();

            switch (c)
            {
                case "1":
                    Console.Write("Enter hotel name: ");
                    string hname = Console.ReadLine();
                    owner.RegisterHotel(hname);
                    break;

                case "2":
                    Console.Write("Enter index: ");
                    int i = int.Parse(Console.ReadLine());
                    Console.Write("Enter new hotel name: ");
                    string newName = Console.ReadLine();
                    owner.UpdateHotelDetails(i, newName);
                    break;

                case "3":
                    Console.Write("Enter index: ");
                    int r = int.Parse(Console.ReadLine());
                    owner.RemoveHotel(r);
                    break;

                case "4":
                    Console.Write("Enter Room ID: ");
                    int rid = int.Parse(Console.ReadLine());
                    Console.Write("Enter Room Type: ");
                    string rtype = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    int rprice = int.Parse(Console.ReadLine());

                    Room room = new Room { roomID = rid, roomType = rtype, price = rprice, isAvailable = true };
                    Services.Rooms.Add(room);
                    Console.WriteLine($"Room {rtype} added!");
                    break;

                default:
                    Console.WriteLine("Invalid Choice! ");
                    break;
            }
            Console.ResetColor();
        }
        static void TaxiDriverMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter Driver Name: ");
            string name = Console.ReadLine();
            TaxiDriver driver = new TaxiDriver { driverID = 1, name = name };
            SystemAdmin.TaxiDrivers.Add(driver);

            Console.WriteLine("--- Taxi Driver Menu ---");
            Console.WriteLine("1. Update Availability");
            Console.WriteLine("2. Accept Booking");
            Console.Write("Your Choice: ");
            string c = Console.ReadLine();

            switch (c)
            {
                case "1":
                    Console.Write("Enter available time: ");
                    string time = Console.ReadLine();
                    driver.UpdateAvailability(time);
                    break;

                case "2":
                    Console.Write("Enter customer name: ");
                    string cust = Console.ReadLine();
                    Console.Write("Enter trip time: ");
                    string t = Console.ReadLine();
                    driver.AcceptBooking(cust, t);
                    break;

                default:
                    Console.WriteLine("Invalid Choice! ");
                    break;
            }
            Console.ResetColor();
        }
        static void SystemAdminMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- System Admin Menu ---");
            Console.WriteLine("1. Manage Users");
            Console.WriteLine("2. Generate Reports");
            Console.Write("Your Choice: ");
            string c = Console.ReadLine();

            switch (c)
            {
                case "1":
                    SystemAdmin.ManageUsers();
                    break;

                case "2":
                    SystemAdmin.GenerateReports();
                    break;

                default:
                    Console.WriteLine("Invalid Choice! ");
                    break;
            }
            Console.ResetColor();
        }
        static void Register()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- Register New User ---");

            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Hotel Owner");
            Console.WriteLine("3. Taxi Driver");
            Console.WriteLine("4. Airline Admin");
            Console.WriteLine("5. System Admin");
            Console.Write("Your Choice: ");
            int choice = int.Parse(Console.ReadLine());
            Console.ResetColor();

            switch (choice)
            {
                case 1: // Customer
                    Customer c = new Customer();
                    Console.Write("Enter Name: ");
                    c.name = Console.ReadLine();
                    Console.Write("Enter Customer ID: ");
                    c.customerID = int.Parse(Console.ReadLine());

                    SystemAdmin.Customers.Add(c);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Customer registered successfully!");
                    Console.ResetColor();
                    break;

                case 2: // Hotel Owner
                    HotelOwner h = new HotelOwner();
                    Console.Write("Enter Name: ");
                    h.name = Console.ReadLine();
                    Console.Write("Enter Owner ID: ");
                    h.ownerID = int.Parse(Console.ReadLine());

                    SystemAdmin.HotelOwners.Add(h);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Hotel Owner registered successfully!");
                    Console.ResetColor();
                    break;

                case 3: // Taxi Driver
                    TaxiDriver d = new TaxiDriver();
                    Console.Write("Enter Name: ");
                    d.name = Console.ReadLine();
                    Console.Write("Enter Driver ID: ");
                    d.driverID = int.Parse(Console.ReadLine());
                    Console.Write("Enter Car Details: ");
                    d.carDetails = Console.ReadLine();

                    SystemAdmin.TaxiDrivers.Add(d);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Taxi Driver registered successfully!");
                    Console.ResetColor();
                    break;

                case 4: // Airline Admin
                    AirlineAdmin a = new AirlineAdmin();
                    Console.Write("Enter Name: ");
                    a.name = Console.ReadLine();
                    Console.Write("Enter Admin ID: ");
                    a.adminID = int.Parse(Console.ReadLine());
                    Console.Write("Enter Airline Name: ");
                    a.airlineName = Console.ReadLine();

                    SystemAdmin.AirlineAdmins.Add(a);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Airline Admin registered successfully!");
                    Console.ResetColor();
                    break;

                case 5: // System Admin
                    SystemAdmin sa = new SystemAdmin();
                    Console.Write("Enter Name: ");
                    sa.name = Console.ReadLine();
                    Console.Write("Enter Admin ID: ");
                    sa.adminID = int.Parse(Console.ReadLine());

                    // ممكن نعمل List للـ SystemAdmins لو محتاجين نحتفظ بيهم
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("System Admin registered successfully!");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice!");
                    Console.ResetColor();
                    break;
            }
        }
        static void Login()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- Login ---");

            Console.WriteLine("1. Customer");
            Console.WriteLine("2. Hotel Owner");
            Console.WriteLine("3. Taxi Driver");
            Console.WriteLine("4. Airline Admin");
            Console.WriteLine("5. System Admin");
            Console.Write("Choose: ");
            Console.ResetColor();

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice)) choice = -1;

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Customer ID: ");
                    int cID = int.Parse(Console.ReadLine());
                    var customer = SystemAdmin.Customers.FirstOrDefault(c => c.customerID == cID);
                    if (customer != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Welcome {customer.name}!");
                        Console.ResetColor();

                        CustomerMenu();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Customer not found!");
                        Console.ResetColor();
                    }
                    break;

                case 2:
                    Console.Write("Enter Owner ID: ");
                    int hID = int.Parse(Console.ReadLine());
                    var owner = SystemAdmin.HotelOwners.FirstOrDefault(h => h.ownerID == hID);
                    if (owner != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Welcome {owner.name}!");
                        Console.ResetColor();

                        HotelOwnerMenu();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Hotel Owner not found!");
                        Console.ResetColor();
                    }
                    break;

                case 3:
                    Console.Write("Enter Driver ID: ");
                    int dID = int.Parse(Console.ReadLine());
                    var driver = SystemAdmin.TaxiDrivers.FirstOrDefault(d => d.driverID == dID);
                    if (driver != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Welcome {driver.name}!");
                        Console.ResetColor();

                        TaxiDriverMenu();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Taxi Driver not found!");
                        Console.ResetColor();
                    }
                    break;

                case 4:
                    Console.Write("Enter Admin ID: ");
                    int aID = int.Parse(Console.ReadLine());
                    var admin = SystemAdmin.AirlineAdmins.FirstOrDefault(a => a.adminID == aID);
                    if (admin != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Welcome {admin.name} ({admin.airlineName})!");
                        Console.ResetColor();

                        AirlineAdminMenu();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Airline Admin not found!");
                        Console.ResetColor();
                    }
                    break;

                case 5:
                    Console.Write("Enter Admin ID: ");
                    int saID = int.Parse(Console.ReadLine());

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Welcome System Admin with ID {saID}!");
                    Console.ResetColor();

                    SystemAdminMenu();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice!");
                    Console.ResetColor();
                    break;
            }
        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===========================================");
            Console.WriteLine("===== Welcome to our Traveling System =====");
            Console.WriteLine("===========================================");
            Console.ResetColor();
            Console.WriteLine();


            bool exit = false;
            while (!exit)
            {
                
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Your Choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice)) choice = -1;

                switch (choice)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        Login();
                        break;
                    case 3:
                        exit = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("See you soon");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice!");
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
       