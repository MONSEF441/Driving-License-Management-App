

# 🚗 Driving License Management System (DVLD)

## 📌 Project Overview

The **Driving License Management System (DVLD)** is a high-performance administrative platform designed to manage the full lifecycle of driving permits. Developed during my studies at **FST Tangier** (MIP section), this project demonstrates a transition from simple procedural code to a scalable **3-Tier Enterprise Architecture**.

---

## 🏗 Enterprise Architecture (3-Tier)

The system is architected to ensure strict separation of concerns, utilizing the Microsoft technology stack:

* **Presentation Layer (C# WinForms)**: A modular UI featuring **Reusable User Controls** for handling complex interactions across 20+ screens efficiently.
* **Business Logic Layer (BLL)**: A dedicated C# library that enforces strict validation, sequential test rules, and license status management.
* **Data Access Layer (DAL & ADO.NET)**: Leverages **ADO.NET** to communicate with **SQL Server**, utilizing stored procedures and optimized queries for maximum data integrity.

---

## 🛠 Key Features

### 👤 Identity & Security

* **Centralized Database**: A robust SQL Server schema managing "People" and "Users" with a 1:1 relationship.
* **Role-Based Access**: Secure login systems developed with C# to control permissions based on user status.

### 📝 Automated Workflow

* **Service Management**: Full CRUD operations for New Licenses, Renewals, and Replacements (Lost/Damaged).
* **Test Engine**: Logic-driven evaluation system requiring successful completion of **Vision ➔ Written ➔ Street** tests.
* **Transaction Integrity**: ADO.NET-managed transactions ensuring that license deactivations and new issuances happen atomically.

### 🪪 Specialized Modules

* **International Permits**: Automated generation of global licenses based on active local records.
* **Detained Licenses**: Management module for handling violations, fines, and the release of confiscated permits.

---

## 🚀 Technical Proficiencies

* **Language**: C#.
* **Database**: Microsoft SQL Server.
* **Data Access**: ADO.NET.
* **Methodology**: Object-Oriented Programming (OOP) following the **ProgrammingAdvices.com** roadmap.

---

