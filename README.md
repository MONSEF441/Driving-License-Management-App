

# <p align="center">🚗 Driving License Management System (DVLD)</p>

---

### 📌 **Summary**

The **Driving License Management System (DVLD)** is a desktop application designed to digitize and automate the operations of a motor vehicle department. Developed during my software engineering studies at **FST Tangier (MIP)**, this project demonstrates a solid application of **Object-Oriented Programming (OOP)** and the implementation of a structured **3-Tier Architecture**.

The system models real-world administrative workflows by transforming driving license procedures into automated and organized software processes.

---

### 🏗 **System Architecture**

The application follows a strict **Separation of Concerns (SoC)**, isolating the user interface from business logic and data access layers to improve maintainability and scalability.

| Layer                    | Technology Stack         | Engineering Focus                                                                                                              |
| ------------------------ | ------------------------ | ------------------------------------------------------------------------------------------------------------------------------ |
| **Presentation (UI)**    | **C# WinForms**          | Modular user interface using reusable **User Controls** and structured forms to reduce duplication and improve maintainability |
| **Business Logic (BLL)** | **C# Class Library**     | Encapsulates domain logic, validation rules, and workflow management                                                           |
| **Data Access (DAL)**    | **ADO.NET + SQL Server** | Handles database interaction through parameterized queries and stored procedures                                               |

---

### 🛠 **Core Features**

#### 🔒 **Identity Management**

* **Relational Database Design**: Centralized management of **People**, **Users**, and **Drivers** with referential integrity.
* **Authentication System**: Validates users and manages application access.

#### ⚙️ **License Workflow Management**

* **Sequential Testing Process**: The system enforces the required testing order:

```
Vision Test → Written Test → Practical Driving Test
```

* **Application Management**: Create and track driving license applications linked to individuals.

#### 🪪 **License Services**

* New license issuance
* License renewal
* Replacement for lost or damaged licenses
* International driving license issuance

#### 🚓 **License Detainment Management**

* Record license detainment cases
* Manage release procedures and related records

---

### 💻 **Skills Demonstrated**

* **Languages & Frameworks:** C#, .NET Framework, WinForms
* **Database & Data Access:** Microsoft SQL Server, T-SQL (Stored Procedures, Views, Joins), ADO.NET
* **Software Engineering Principles:** 3-Tier Architecture, Object-Oriented Programming, DRY Principle, Relational Database Design, Separation of Concerns

---

### 📂 **Project Structure**

```text
DVLD_Solution/
├── DVLD_Presentation/   # UI Layer: Forms and Custom User Controls
├── DVLD_Business/       # Business Logic Layer
├── DVLD_Data/           # Data Access Layer
├── SQL_Scripts/         # Database creation scripts
└── README.md
```

---

### 🚀 **Environment Setup & Execution**

1. **Database Initialization**
   Open **SQL Server Management Studio (SSMS)** and execute the scripts in the `/SQL_Scripts` directory to create the database schema.

2. **Configure Connection**
   Update the **ADO.NET connection string** in the `DVLD_Data` project to point to your SQL Server instance.

3. **Compile & Run**
   Open the solution in **Visual Studio**, set `DVLD_Presentation` as the startup project, and run the application.

