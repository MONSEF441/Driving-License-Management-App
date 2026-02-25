🚗 Driving License Management System (DVLD)






📌 Overview

DVLD is a professional Driving License Management System designed to handle the complete lifecycle of driving permits. Developed at FST Tangier (MIP section), it demonstrates a transition from procedural programming to a robust 3-Tier Enterprise Architecture, making it ideal for enterprise-level learning and prototypes.

🏗 Architecture (3-Tier)
Layer	Technology	Responsibilities
Presentation (UI)	C# WinForms	Modular interface with Reusable User Controls for 20+ screens.
Business Logic (BLL)	C# Library	Validation, license rules, test progression, workflow automation.
Data Access (DAL)	ADO.NET + SQL Server	Database communication, stored procedures, transactional integrity.
🛠 Key Features
👤 User & Security Management

Centralized SQL Server database for People and Users.

Role-Based Access Control with secure login authentication.

📝 Automated License Workflow

Full CRUD for New Licenses, Renewals, and Replacements.

Sequential Test Engine: Vision ➔ Written ➔ Street tests.

Atomic transactions ensure data integrity.

🪪 Specialized Modules

International Permits: Auto-generation based on active local licenses.

Detained Licenses: Track violations, fines, and permit releases.

🚀 Tech Stack

Language: C# (OOP Principles)

Database: Microsoft SQL Server

Data Access: ADO.NET

UI/UX: Modular WinForms with reusable components

Architecture: Enterprise 3-Tier

📂 Project Structure
DVLD/
│
├─ DVLD_PresentationAccess/   # WinForms UI & UserControls
├─ DVLD_BusinessAccess/       # Business Logic Layer
├─ DVLD_DataAccess/           # ADO.NET & SQL Server Integration
├─ SQL/                       # Database scripts & stored procedures
└─ README.md

⚡ Highlights

Clean modular design for maintainability.

Implements real-world license administration rules.

Professional enterprise-level architecture ready for learning or prototyping.

🛠 How to Run

Restore the SQL Server database from /SQL.

Update the connection string in DVLD_DataAccess to match your environment.

Open the solution in Visual Studio and run DVLD_PresentationAccess.
