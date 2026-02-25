

# <p align="center">🚗 Driving License Management System (DVLD)</p>

---

### 📌 **Summary**

The **Driving License Management System (DVLD)** is an enterprise-grade desktop application engineered to digitize and automate the operations of a motor vehicle department. Developed during my software engineering studies at FST Tangier (MIP), this project showcases a strong command of **Object-Oriented Programming (OOP)** and strict adherence to a scalable **3-Tier Architecture**.

The system successfully translates complex, real-world governmental business rules into automated, secure software workflows.

---

### 🏗 **System Architecture & Design Patterns**

The application is engineered with a strict **Separation of Concerns (SoC)**, decoupling the user interface from data access and business logic to ensure high maintainability and code scalability.

| Layer | Technology Stack | Engineering Focus |
| --- | --- | --- |
| **Presentation (UI)** | **C# WinForms** | Built a highly modular interface. Extensively utilized custom **Reusable User Controls** to adhere to the DRY (Don't Repeat Yourself) principle across 20+ complex forms. |
| **Business Logic (BLL)** | **C# Class Library** | Encapsulated all domain logic, implementing strict validation pipelines, sequential state engines for testing, and automated lifecycle management. |
| **Data Access (DAL)** | **ADO.NET + SQL Server** | Engineered secure data pipelines using parameterized **Stored Procedures** to prevent SQL injection and utilized ADO.NET for high-performance data retrieval. |

---

### 🛠 **Technical Achievements & Core Features**

#### 🔒 **Identity Management & Security**

* **Relational Database Design**: Architected a normalized SQL Server database to centralize "People" and "User" records, maintaining strict referential integrity.
* **Role-Based Access Control (RBAC)**: Implemented a secure authentication layer, managing user sessions and system permissions dynamically.

#### ⚙️ **Workflow Automation & Transaction Management**

* **State-Driven Test Engine**: Programmed a sequential validation system that strictly enforces business rules (e.g., candidates must pass Vision ➔ Theory ➔ Practical tests in exact order).
* **Atomic Transactions**: Leveraged SQL and ADO.NET transaction management to ensure data atomicity. Complex operations—like deactivating an old license and issuing a new one—are executed as single, fail-safe transactions.
* **Full Service Lifecycle**: Programmed end-to-end CRUD capabilities for issuing New, Renewed, Damaged, and Replacement licenses.

#### 🪪 **Specialized Domain Modules**

* **Global Permitting**: Engineered automated logic to cross-reference local license validity and generate International Driving Permits.
* **Violation & Detainment Tracking**: Built a comprehensive sub-system to record traffic infractions, calculate fines, and manage the confiscation and release of permits.

---

### 💻 **Skills Demonstrated**

* **Languages & Frameworks:** C#, .NET Framework, WinForms
* **Database & Data Access:** Microsoft SQL Server, T-SQL (Stored Procedures, Views, Joins), ADO.NET
* **Software Engineering Principles:** 3-Tier Architecture, OOP, DRY Principle, Relational Database Design, UI/UX Component Reusability.

---

### 📂 **Project Structure**

```text
DVLD_Solution/
├── DVLD_Presentation/   # UI Layer: Forms and Custom User Controls
├── DVLD_Business/       # BLL Layer: Domain entities and validation rules
├── DVLD_Data/           # DAL Layer: Database connectivity and SQL execution methods
├── SQL_Scripts/         # DDL/DML scripts and Stored Procedures for DB recreation
└── README.md            # Project Documentation

```

---

### 🚀 **Environment Setup & Execution**

1. **Database Initialization**: Open Microsoft SQL Server Management Studio (SSMS) and execute the master script located in the `/SQL_Scripts` directory to generate the schema and stored procedures.
2. **Configure Connection**: Navigate to the `DVLD_Data` project layer and update the ADO.NET connection string to target your local SQL Server instance.
3. **Compile & Run**: Open the solution in **Visual Studio**, set `DVLD_Presentation` as the startup project, and run the application.

---

