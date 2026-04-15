🚗 Driving License Management System (DVLD)
📌 Summary

The Driving License Management System (DVLD) is a desktop application developed as part of my software engineering studies at FST Tangier (MIP).

The system simulates the workflow of a driving license department by managing license applications, driver records, testing procedures, and license issuance.

This project focuses on applying Object-Oriented Programming (OOP) principles and implementing a structured 3-Tier Architecture to separate the user interface, business logic, and data access layers.

The objective of the project was to translate real-world administrative workflows into a structured software system using C#, WinForms, and SQL Server.

🏗 System Architecture

The application follows a 3-Tier Architecture to ensure clear separation between UI, business logic, and database access.

Layer	Technology Stack	Responsibility
Presentation Layer	C# WinForms	Implements the user interface and interaction logic using reusable user controls and forms
Business Logic Layer	C# Class Library	Contains the domain entities, validation rules, and workflow logic
Data Access Layer	ADO.NET + SQL Server	Handles database communication using stored procedures and parameterized queries

This structure improves maintainability, scalability, and separation of concerns.

🛠 Core Features
Identity Management
Management of People, Users, and Drivers
Authentication system for application access
Relational database structure with referential integrity
License Application Workflow
Create and manage driving license applications
Sequential testing process:
Vision Test → Written Test → Practical Driving Test
License issuance after successful completion of all tests
License Services
New license issuance
License renewal
Replacement for damaged or lost licenses
International driving license issuance
License Detainment
Record license detainment cases
Manage release procedures and related records
💻 Skills Demonstrated

Languages & Frameworks

C#
.NET Framework
WinForms

Database & Data Access

Microsoft SQL Server
T-SQL
Stored Procedures
ADO.NET

Software Engineering Concepts

Object-Oriented Programming
3-Tier Architecture
Relational Database Design
UI Component Reusability
Separation of Concerns
📂 Project Structure
DVLD_Solution/
├── DVLD_Presentation/   # UI Layer: Forms and User Controls
├── DVLD_Business/       # Business Logic Layer
├── DVLD_Data/           # Data Access Layer
├── SQL_Scripts/         # Database creation scripts
└── README.md
🚀 Setup
Open SQL Server Management Studio.
Execute the scripts inside /SQL_Scripts to create the database.
Update the connection string in the DVLD_Data project.
Open the solution in Visual Studio.
Set DVLD_Presentation as the startup project and run.
