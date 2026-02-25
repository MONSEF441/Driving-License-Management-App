<p align="center">🚗 Driving License Management System (DVLD)</p>
<p align="center">
  
<img src="[https://img.shields.io/badge/C%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white](https://www.google.com/search?q=https://img.shields.io/badge/C%2523-%2523239120.svg%3Fstyle%3Dfor-the-badge%26logo%3Dc-sharp%26logoColor%3Dwhite)" alt="C#">
<img src="[https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white](https://www.google.com/search?q=https://img.shields.io/badge/SQL%2520Server-CC2927%3Fstyle%3Dfor-the-badge%26logo%3Dmicrosoft-sql-server%26logoColor%3Dwhite)" alt="SQL Server">
<img src="[https://img.shields.io/badge/Architecture-3--Tier-red?style=for-the-badge](https://www.google.com/search?q=https://img.shields.io/badge/Architecture-3--Tier-red%3Fstyle%3Dfor-the-badge)" alt="3-Tier">
<img src="[https://img.shields.io/badge/Role-Tech%20Lead-blue?style=for-the-badge](https://www.google.com/search?q=https://img.shields.io/badge/Role-Tech%2520Lead-blue%3Fstyle%3Dfor-the-badge)" alt="Tech Lead">
</p>

---

## 📌 Project Overview

**DVLD** is a high-performance administrative platform designed to manage the full lifecycle of driving permits. Developed during my software development studies in the **MIP section at FST Tangier**, this system marks a significant transition from procedural logic to a scalable **3-Tier Enterprise Architecture**.

---

## 🏗 System Architecture

The application follows a strict **3-Tier Design**, ensuring a clean separation of concerns and high maintainability:

| **Layer** | **Technology** | **Responsibilities** |
| --- | --- | --- |
| **Presentation (UI)** | **C# WinForms** | A modular interface featuring **Reusable User Controls** to manage 20+ screens efficiently. |
| **Business Logic (BLL)** | **C# Class Library** | Enforces strict validation, sequential test rules, and automated permit workflows. |
| **Data Access (DAL)** | **ADO.NET + SQL** | Handles database communication, stored procedures, and ensures transactional integrity. |

---

## 🛠 Key Features

### 👤 Identity & Security

* **Centralized Database**: A robust SQL Server schema managing "People" and "Users" with unified records.
* **RBAC (Role-Based Access Control)**: Secure login authentication and permission management.

### 📝 Automated Workflows

* **Full CRUD Services**: End-to-end management for **New Licenses**, **Renewals**, and **Replacements** (Lost/Damaged).
* **Sequential Test Engine**: Logic-driven evaluation requiring passing **Vision ➔ Written ➔ Street** tests in order.
* **Atomic Transactions**: Powered by ADO.NET to ensure data remains consistent during complex operations.

### 🪪 Specialized Modules

* **International Permits**: Automated generation based on active local license credentials.
* **Detained Licenses**: Full tracking for traffic violations, fine payments, and license releases.

---

## 📂 Project Structure

```text
DVLD/
├── DVLD_Presentation/   # Modular WinForms UI & Reusable UserControls
├── DVLD_Business/       # Core Business Logic Layer (BLL)
├── DVLD_Data/           # ADO.NET & SQL Server Data Access Layer (DAL)
├── SQL_Scripts/         # Database schemas & Stored Procedures
└── README.md            # Project Documentation

```

---

## 🚀 How to Run

1. **Database**: Restore the provided SQL Server database from the `/SQL_Scripts` directory.
2. **Configuration**: Update the connection string in the `DVLD_Data` layer to match your local SQL Server instance.
3. **Launch**: Open the solution in **Visual Studio** and set `DVLD_Presentation` as the startup project.

---

### 💡 Tech Lead Next Steps

Since you mentioned using **Visual Studio** to create the repository, would you like me to show you how to add a **License** file (like MIT or Apache) to your repo to make it look even more official for recruiters?
