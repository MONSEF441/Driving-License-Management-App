
# 🚗 Driving & Vehicle License Department (DVLD)

<p align="center">
  <img src="assets/banner.png" width="100%">
</p>

<div align="center">

### A Desktop Management System for Driving License Services

![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=for-the-badge&logo=csharp)
![WinForms](https://img.shields.io/badge/WinForms-Desktop-blue?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red?style=for-the-badge&logo=microsoftsqlserver)
![ADO.NET](https://img.shields.io/badge/ADO.NET-Data%20Access-0078D4?style=for-the-badge)
![Architecture](https://img.shields.io/badge/Architecture-3--Tier-success?style=for-the-badge)
![License](https://img.shields.io/badge/Status-Completed-brightgreen?style=for-the-badge)

*A complete Driving & Vehicle License Department desktop management system built using C#, WinForms, SQL Server, and ADO.NET following a Three-Tier Architecture.*

---

### ⭐ Key Highlights

✔ Three-Tier Architecture  
✔ SQL Server Database  
✔ User Authentication & Authorization  
✔ Local & International Driving Licenses  
✔ Driving Tests Management  
✔ License Renewal & Replacement  
✔ License Detention & Release  
✔ Modular User Controls  
✔ Clean Dark UI

</div>

---

# 📖 Overview

The **Driving & Vehicle License Department (DVLD)** is a desktop application that digitizes and automates the workflow of a driving license department.

The system manages applicants from the moment they are registered until they receive their driving license while enforcing the complete business workflow required by the licensing process.

The application was developed as a practical software engineering project focusing on:

- Object-Oriented Programming
- Layered Architecture
- Database Design
- Desktop Application Development
- ADO.NET
- SQL Server
- Clean Code Principles

---

# 📚 Documentation

| Document | Description |
|----------|-------------|
| 📖 [Features](docs/features.md) | Complete list of implemented features |
| 🏗 [Architecture](docs/architecture.md) | Three-Tier architecture and project structure |
| 🗄 [Database](docs/database.md) | Database schema and tables |
| 📷 [Screenshots](docs/screenshots.md) | Complete application walkthrough |
| ⚙ [Installation](docs/installation.md) | Installation and setup guide |

---

# ✨ Features

## 👤 People Management

- Register new people
- Update personal information
- Delete records
- Search & filter
- View complete profile

---

## 👥 User Management

- User authentication
- User authorization
- Create new users
- Edit existing users
- Permission management

---

## 🚘 Local Driving License Applications

- Create applications
- Select license class
- Application tracking
- Application history

---

## 📝 Driving Tests

The system supports the complete testing workflow:

- 👁 Vision Test
- 📚 Written Test
- 🚗 Street Test

Applicants must successfully pass each stage before moving to the next one.

---

## 🪪 License Services

- Issue First Driving License
- Renew Driving License
- Replace Lost License
- Replace Damaged License

---

## 🌍 International License

- Issue International Driving License
- Search International Licenses
- View License Information

---

## 🚔 License Detention

- Detain License
- Release License
- View Detained Licenses

---

## 📊 Driver Management

- Driver Records
- Driver History
- License History

---

# 🏗 System Architecture

The project follows the **Three-Tier Architecture**.

```
                Presentation Layer
                  (WinForms UI)
                       │
                       ▼
             Business Logic Layer
          (Business Rules & Validation)
                       │
                       ▼
               Data Access Layer
                  (ADO.NET)
                       │
                       ▼
                  SQL Server
```

---

## Presentation Layer

Responsible for:

- User Interface
- Forms
- User Controls
- Input Validation
- User Experience

---

## Business Layer

Responsible for:

- Business Rules
- Validation
- Application Logic
- Entity Management

---

## Data Access Layer

Responsible for:

- SQL Queries
- CRUD Operations
- Database Communication
- Stored Procedures (if applicable)

---

# 💾 Database

The project uses **Microsoft SQL Server**.

Main entities include:

- Countries
- People
- Users
- Drivers
- Applications
- Application Types
- License Classes
- Licenses
- International Licenses
- Local Driving License Applications
- Test Types
- Tests
- Test Appointments
- Detained Licenses

---

# 🛠 Technologies Used

| Technology | Purpose |
|------------|---------|
| C# | Programming Language |
| .NET Framework | Application Framework |
| WinForms | Desktop UI |
| SQL Server | Database |
| ADO.NET | Database Access |
| Guna UI | Modern UI Components |
| Visual Studio | IDE |
| Git | Version Control |
| GitHub | Repository Hosting |

---

# 📂 Project Structure

```
DVLD/

├── DVLD.Presentation/
│
├── DVLD.Business/
│
├── DVLD.DataAccess/
│
├── Database/
│
├── docs/
│   ├── screenshots/
│   ├── architecture.md
│   ├── database.md
│   └── features.md
│
└── README.md
```

---

# 📸 Application Screenshots

## Login

```text
docs/screenshots/login/login.png
```

![Login](docs/screenshots/login/login.png)

---

## Dashboard

```text
docs/screenshots/dashboard/dashboard.png
```

![Dashboard](docs/screenshots/dashboard/dashboard.png)

---

## People Management

```text
docs/screenshots/people/manage-people.png
```

![People](docs/screenshots/people/manage-people.png)

---

## Add New Person

```text
docs/screenshots/people/add-person.png
```

![Add Person](docs/screenshots/people/add-person.png)

---

## Local License Application

```text
docs/screenshots/applications/new-local-license.png
```

![Application](docs/screenshots/applications/new-local-license.png)

---

## Vision Test

```text
docs/screenshots/tests/vision-test.png
```

![Vision](docs/screenshots/tests/vision-test.png)

---

## Written Test

```text
docs/screenshots/tests/written-test.png
```

![Written](docs/screenshots/tests/written-test.png)

---

## Street Test

```text
docs/screenshots/tests/street-test.png
```

![Street](docs/screenshots/tests/street-test.png)

---

## Issue Driving License

```text
docs/screenshots/licenses/issue-license.png
```

![Issue](docs/screenshots/licenses/issue-license.png)

---

## License Information

```text
docs/screenshots/licenses/license-info.png
```

![License](docs/screenshots/licenses/license-info.png)

---

## International License

```text
docs/screenshots/international/international-license.png
```

![International](docs/screenshots/international/international-license.png)

---

## Detain License

```text
docs/screenshots/detention/detain-license.png
```

![Detain](docs/screenshots/detention/detain-license.png)

---

# 🚀 Installation

## Requirements

- Visual Studio 2022
- .NET Framework
- SQL Server
- SQL Server Management Studio

---

## Clone Repository

```bash
git clone https://github.com/YourUsername/DVLD.git
```

---

## Restore Database

1. Open SQL Server Management Studio.
2. Restore or execute the provided database script.
3. Verify that the **DVLD** database has been created successfully.

---

## Configure Connection String

Update the connection string inside the project:

```csharp
public static string ConnectionString =
"Server=.;Database=DVLD;User Id=sa;Password=YOUR_PASSWORD;Encrypt=False;";
```

---

## Run the Application

1. Open the solution in Visual Studio.
2. Build the solution.
3. Start the application.

---

# 🎯 Learning Outcomes

Through this project I strengthened my knowledge of:

- Object-Oriented Programming
- SOLID Principles
- Three-Tier Architecture
- Desktop Application Development
- SQL Server
- Database Design
- ADO.NET
- UI Design
- Software Engineering Practices

---

# 🔮 Future Improvements

- Export reports to PDF
- Dashboard analytics
- Email notifications
- Audit logging
- Role-based permissions expansion
- Entity Framework version
- REST API integration
- WPF migration

---

# 👨‍💻 Author

**Monssef Bougaidan**

Computer Science Student

- 💻 C#
- 🖥 WinForms
- 🗄 SQL Server
- ⚙ ADO.NET
- 📚 Software Engineering

---

<div align="center">

### ⭐ If you found this project interesting, consider giving it a star!

</div>
