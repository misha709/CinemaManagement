# Cinema Management System

A simple Cinema Management System built with ASP.NET Core MVC and Entity Framework Core.

## Features

- **Halls Management**: Create, view, edit, and delete cinema halls with configurable rows and seats
- **Movies Management**: Manage movie catalog with title, description, duration, genre, age rating, and release date
- **Screenings Management**: Schedule movie screenings in specific halls with start times and pricing
- **Bookings Management**: Handle customer bookings with seat assignments and status tracking

## Technology Stack

- **Framework**: ASP.NET Core MVC (.NET 10)
- **Database**: Entity Framework Core with SQL Server
- **UI**: Bootstrap 5, Razor Views
- **Language**: C# 12

## Project Structure

```
CinemaManagement.Web/
├── Controllers/          # MVC Controllers (Halls, Movies, Screenings, Bookings)
├── Data/                 # DbContext and database configuration
├── Models/               # Entity models (Hall, Movie, Screening, Booking)
├── Views/                # Razor views for all entities
│   ├── Halls/
│   ├── Movies/
│   ├── Screenings/
│   ├── Bookings/
│   └── Shared/
└── wwwroot/              # Static files (CSS, JS, libraries)
```

## Getting Started

### Prerequisites

- .NET 10 SDK or later
- SQL Server or Azure SQL

### Setup Instructions

1. **Clone or download the project**

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Create the database**
   
   Run the following commands to create the database and apply migrations:
   
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open in browser**
   
   Navigate to `https://localhost:5001` or `http://localhost:5000`

## Database Configuration

The application uses SQL Server LocalDB by default. The connection string is configured in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CinemaManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

To use a different database, update the connection string in `appsettings.json`.

## Usage

### Navigation

The main navigation bar provides links to:
- **Home**: Landing page
- **Halls**: Manage cinema halls
- **Movies**: Manage movie catalog
- **Screenings**: Schedule and manage screenings
- **Bookings**: View and manage customer bookings

### Creating Data

1. **Start with Halls**: Create halls first (e.g., "Hall A" with 10 rows and 15 seats per row)
2. **Add Movies**: Add movies to your catalog
3. **Schedule Screenings**: Create screenings by selecting a movie, hall, date/time, and price
4. **Make Bookings**: Create bookings by selecting a screening and entering customer details and seat information

## Models Overview

### Hall
- Name, Rows, Seats Per Row
- Calculated capacity (Rows × Seats Per Row)

### Movie
- Title, Description, Duration (minutes)
- Genre, Age Rating, Release Date

### Screening
- Movie, Hall, Start Time, Base Price
- Links a movie to a specific hall at a specific time

### Booking
- Screening, Customer Name, Customer Email
- Row Number, Seat Number, Status (Booked/Cancelled)

## Notes

- This is a basic demonstration project without advanced features
- No seat availability validation (seats can be double-booked)
- No authentication or authorization
- No payment processing
- Simple status tracking (Booked/Cancelled)

## Future Enhancements

Potential improvements for a production system:
- User authentication and role-based authorization
- Seat availability checking and visual seat selection
- Payment integration
- Email notifications
- Reporting and analytics
- Multi-language support
- Mobile-responsive design improvements
- API endpoints for third-party integration

## License

This is a demo project for educational purposes.
