# 🚀 Installation Guide

This guide explains how to set up and run the Driving & Vehicle License Department (DVLD) application on your local machine.

---

# Prerequisites

Before running the project, make sure the following software is installed:

- Visual Studio 2022 (or newer)
- .NET Framework
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)

---

# Clone the Repository

Clone the repository using Git:

```bash
git clone https://github.com/MONSEF441/Driving-License-Management-App.git
```

Or download it as a ZIP file from GitHub.

---

# Restore the Database

1. Open SQL Server Management Studio.
2. Connect to your SQL Server instance.
3. Create a new database named:

```
DVLD
```

4. Execute the provided SQL script (or restore the backup if included).

After the process is complete, verify that all database tables have been created successfully.

---

# Configure the Connection String

Open the project and locate the database connection string.

Example:

```csharp
public static string ConnectionString =
"Server=.;Database=DVLD;User Id=sa;Password=YOUR_PASSWORD;Encrypt=False;";
```

Update the following values to match your SQL Server configuration:

- Server Name
- Database Name
- Username
- Password

---

# Open the Solution

1. Launch Visual Studio.
2. Open the solution file (`.sln`).
3. Restore NuGet packages if prompted.
4. Build the solution.

---

# Run the Application

Press **F5** or click **Start** in Visual Studio.

If everything is configured correctly, the application will launch and display the login screen.

---

# Troubleshooting

## Cannot connect to SQL Server

- Verify SQL Server is running.
- Verify SQL Authentication is enabled.
- Check the connection string.
- Verify the DVLD database exists.

---

## Login Failed

- Verify the database contains user records.
- Check the configured SQL username and password.

---

## Build Errors

- Restore NuGet packages.
- Clean and rebuild the solution.
- Ensure all project references are loaded.

---

# Project Requirements

| Component | Version |
|-----------|---------|
| Visual Studio | 2022 or newer |
| .NET Framework | Project Target Framework |
| SQL Server | 2019+ Recommended |
| Windows | Windows 10 / 11 |

---

# Repository Structure

```
Driving-License-Management-App/

├── assets/
├── docs/
├── Database/
├── DVLD/
├── README.md
└── LICENSE
```

---

# Getting Started

After completing the setup, you can explore the following modules:

- Login
- People Management
- User Management
- Driver Management
- Local License Applications
- Driving Tests
- License Services
- International Licenses
- License Detention

For more information, visit the documentation inside the `docs` folder.
