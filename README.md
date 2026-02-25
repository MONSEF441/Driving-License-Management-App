Understood—we'll keep it clean, text-focused, and high-contrast so it looks sharp on your GitHub profile. This version uses Markdown "Blockquotes" and bold headers to create a professional structure without relying on external images.

<p align="center">🚗 Driving License Management System (DVLD)</p>
📌 Project Overview
DVLD is a high-performance administrative platform designed to manage the full lifecycle of driving permits. Developed during my software development studies in the MIP section at FST Tangier, this system marks a significant transition from procedural logic to a scalable 3-Tier Enterprise Architecture.

🏗 Enterprise Architecture (3-Tier)
The system is architected to ensure strict separation of concerns, utilizing a professional Microsoft technology stack:

1. Presentation Layer (UI) > * Technology: C# WinForms.

Responsibility: A modular interface featuring Reusable User Controls to manage 20+ screens efficiently.

2. Business Logic Layer (BLL) > * Technology: C# Class Library.

Responsibility: Enforces strict validation, sequential test rules, and automated permit workflows.

3. Data Access Layer (DAL) > * Technology: ADO.NET + Microsoft SQL Server.

Responsibility: Handles database communication via stored procedures and ensures transactional integrity.

🛠 Key Features
👤 Identity & Security
Centralized Database: A robust SQL Server schema managing "People" and "Users" with a 1:1 relationship.

RBAC: Secure login authentication and role-based permission management.

📝 Automated Workflows
Full CRUD Services: End-to-end management for New Licenses, Renewals, and Replacements (Lost/Damaged).

Sequential Test Engine: Logic-driven evaluation requiring passing Vision ➔ Written ➔ Street tests in order.

Atomic Transactions: Powered by ADO.NET to ensure data remains consistent during complex operations.

🪪 Specialized Modules
International Permits: Automated generation based on active local license credentials.

Detained Licenses: Full tracking for traffic violations, fine payments, and license releases.

📂 Project Structure
Plaintext
DVLD/
├── DVLD_Presentation/   # Modular WinForms UI & Reusable UserControls
├── DVLD_Business/       # Core Business Logic Layer (BLL)
├── DVLD_Data/           # ADO.NET & SQL Server Data Access Layer (DAL)
├── SQL_Scripts/         # Database schemas & Stored Procedures
└── README.md            # Project Documentation
🚀 How to Run
Database: Restore the provided SQL Server database from the /SQL_Scripts directory.

Configuration: Update the connection string in the DVLD_Data layer to match your local SQL Server instance.

Launch: Open the solution in Visual Studio and set DVLD_Presentation as the startup project.
