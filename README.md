

# <p align="center"><h1>🚗 Driving License Management System (DVLD)</h1></p>

<p align="center">
<img src="[https://img.shields.io/badge/C%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white](https://img.shields.io/badge/C%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)" alt="C#">
<img src="[https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)" alt="SQL Server">
<img src="[https://img.shields.io/badge/Architecture-3--Tier-red?style=for-the-badge](https://img.shields.io/badge/Architecture-3--Tier-red?style=for-the-badge)" alt="3-Tier">
<img src="[https://img.shields.io/badge/Role-Tech%20Lead-blue?style=for-the-badge](https://img.shields.io/badge/Role-Tech%20Lead-blue?style=for-the-badge)" alt="Tech Lead">
</p>

---

### 📌 **Project Overview**

**DVLD** is a high-performance administrative platform designed to manage the full lifecycle of driving permits. Developed during my software development studies in the **MIP section at FST Tangier**, this system demonstrates a professional transition from procedural logic to a scalable **3-Tier Enterprise Architecture**.

---

### 🏗 **Enterprise Architecture (3-Tier)**

The system is architected to ensure strict separation of concerns, utilizing a professional Microsoft technology stack:

> **1. Presentation Layer (UI)**
> * **Technology:** C# WinForms.
> * **Responsibility:** A modular interface featuring **Reusable User Controls** to manage 20+ screens efficiently.
> 
> 

> **2. Business Logic Layer (BLL)**
> * **Technology:** C# Class Library.
> * **Responsibility:** Enforces strict validation, sequential test rules, and automated permit workflows.
> 
> 

> **3. Data Access Layer (DAL)**
> * **Technology:** ADO.NET + Microsoft SQL Server.
> * **Responsibility:** Handles database communication via stored procedures and ensures transactional integrity.
> 
> 

---

### 🛠 **Key Features**

#### 👤 **Identity & Security**

* **Centralized Database**: A robust SQL Server schema managing "People" and "Users" with a 1:1 relationship.
* **RBAC**: Secure login authentication and role-based permission management.

#### 📝 **Automated Workflows**

* **Full CRUD Services**: End-to-end management for **New Licenses**, **Renewals**, and **Replacements** (Lost/Damaged).
* **Sequential Test Engine**: Logic-driven evaluation requiring passing **Vision ➔ Written ➔ Street** tests in order.
* **Atomic Transactions**: Powered by ADO.NET to ensure data remains consistent during complex operations.

#### **Specialized Modules**

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

