

## 📌 Overview  
The **Travel Booking System** is a C# console-based application that simulates a simple platform for booking:  

- ✈️ **Flights**  
- 🚖 **Taxis**  
- 🏨 **Hotel Rooms**  

The system supports different types of users (**Customers, Admins, and Service Providers**) and allows **registration, login, and personalized menus** for each role.  

---

# 🛠️ Features  
- 🔑 **Register & Login** for multiple user roles:  
  - **System Admin**  
  - **Airline Admin**  
  - **Hotel Admin**  
  - **Taxi Admin**  
  - **Customer**  

- 👤 **Customer Functionalities**  
  - Search for flights  
  - Book flights (with ticket generation)  
  - Book hotel rooms  
  - Book taxis  

- 🏨 **Hotel Admin Functionalities**  
  - Add rooms  
  - Update rooms  
  - Check available rooms  

- 🚖 **Taxi Admin Functionalities**  
  - Add taxis  
  - Assign drivers  
  - Manage taxi bookings  

- ✈️ **Airline Admin Functionalities**  
  - Add flights  
  - Update flights  
  - Manage flight availability  

- ⚙️ **System Admin Functionalities**  
  - Manage all users (register, list, etc.)  

---

# 🏗️ Project Structure  
📂 TravelBookingSystem
┣ 📜 Program.cs // Main entry point + Menus
┣ 📜 Person.cs // Base class for all users
┣ 📜 Customer.cs // Customer class
┣ 📜 Booking.cs // Booking base + FlightBooking, TaxiBooking, HotelBooking
┣ 📜 Flight.cs // Flight management
┣ 📜 Room.cs // Hotel room management
┣ 📜 Taxi.cs // Taxi management
┣ 📜 Services.cs // Stores system-wide lists (Flights, Rooms, Taxis)
┣ 📜 SystemAdmin.cs // Manages users
┗ 📜 README.md

yaml
نسخ الكود
