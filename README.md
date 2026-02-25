
# <p align="center"><h1>🚗 Driving License Management System (DVLD)</h1></p>



---

### 📌 **Project Overview**

**DVLD** is a high-performance administrative platform designed to manage the full lifecycle of driving permits. Developed during my software development studies in the **MIP section at FST Tangier**, this system demonstrates a professional transition from procedural logic to a scalable **3-Tier Enterprise Architecture**.

---

### 🏗 **System Architecture**

The application is structured into three distinct layers to ensure high maintainability and scalability:

| Layer | Technology Stack | Responsibilities |
| --- | --- | --- |
| **Presentation (UI)** | **C# WinForms** | A modular interface featuring **Reusable User Controls** to manage 20+ screens efficiently. |
| **Business Logic (BLL)** | **C# Class Library** | Enforces strict validation, sequential test rules, and automated permit workflows. |
| **Data Access (DAL)** | **ADO.NET + SQL** | Handles database communication via stored procedures and ensures transactional integrity. |

---

### 🛠 **Key Features**

#### 👤 **Identity & Security**

* **Centralized Database**: A robust SQL Server schema managing "People" and "Users" with a 1:1 relationship.
* **RBAC (Role-Based Access Control)**: Secure login authentication and role-based permission management.

#### 📝 **Automated Workflows**

* **Full CRUD Services**: End-to-end management for **New Licenses**, **Renewals**, and **Replacements** (Lost/Damaged).
* **Sequential Test Engine**: Logic-driven evaluation requiring passing **Vision ➔ Written ➔ Street** tests in order.
* **Atomic Transactions**: Powered by ADO.NET to ensure data remains consistent during complex operations.

#### 🪪 **Specialized Modules**

* **International Permits**: Automated generation based on active local license credentials.
* **Detained Licenses**: Full tracking for traffic violations, fine payments, and license releases.

---

### 📂 **Project Structure**

```text
DVLD/
├── DVLD_Presentation/   # Modular WinForms UI & Reusable UserControls
├── DVLD_Business/       # Core Business Logic Layer (BLL)
├── DVLD_Data/           # ADO.NET & SQL Server Data Access Layer (DAL)
├── SQL_Scripts/         # Database schemas & Stored Procedures
└── README.md            # Project Documentation

```

---

### 🚀 **How to Run**

1. **Database**: Restore the provided SQL Server database from the `/SQL_Scripts` directory.
2. **Configuration**: Update the connection string in the `DVLD_Data` layer to match your local SQL Server instance.
3. **Launch**: Open the solution in **Visual Studio** and set `DVLD_Presentation` as the startup project.

---

