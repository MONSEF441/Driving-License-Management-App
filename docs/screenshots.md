# 📸 Application Screenshots

This document provides a visual walkthrough of the **Driving & Vehicle License Department (DVLD)** Management System. The screenshots are organized according to the application's modules while reflecting the complete driving license lifecycle.

---

# 🔐 Authentication

## Login

The application starts with a secure login screen where authorized users authenticate before accessing the system.

![Login](screenshots/Login/Login.png)

---

# 📂 Applications

## Manage Local Driving License Applications

View, search, filter, and manage all local driving license applications.

![Manage Local Applications](screenshots/Applications/manage-local-applications.png)

---

## New Local Driving License Application

Create a new local driving license application by selecting an applicant and the desired license class.

![New Local Application](screenshots/Applications/new-local-licensePart1.png)
![New Local Application](screenshots/Applications/new-local-licensePart2.png)

---

## Manage Application Types

Administrators can manage the available application types and update their corresponding service fees without modifying the application's source code.

![Application Types](screenshots/Applications/application-types.png)


---

# 👤 People

## Manage People

Manage all registered people within the system.

Available operations include:

- Add Person
- Update Person
- Delete Person
- Search & Filter
- View Person Details

![Manage People](screenshots/People/manage-people.png)

---

## Add / Edit Person

Register a new person or update existing personal information.

![Add Person](screenshots/People/add-person.png)

---

# 🚗 Drivers

## Manage Drivers

Browse and search all registered drivers together with their license information.

![Manage Drivers](screenshots/Drivers/manage-drivers.png)

---

# 👥 Users

## Manage Users

Administrators can manage users who have access to the system.

Available operations include:

- Add User
- Update User
- Delete User
- Activate / Deactivate User
- Search Users

![Manage Users](screenshots/Users/manage-users.png)

---

## Add / Edit User

Create new system accounts and assign permissions.

![Add User](screenshots/Users/add-user.png)

---

# 📝 Tests

## Driving Test Workflow

The DVLD system supports three driving examinations:

- 👁 Vision Test
- 📚 Written Test
- 🚗 Street Test

Each examination follows the same workflow:

1. Schedule a test appointment.
2. Conduct the examination.
3. Record the result.
4. Update the application status.

The only differences between the tests are the **test type**, **associated fee**, and **business stage**.

### Test Appointment

![Test Appointment](screenshots/Tests/test-appointment.png)

### Take Test

![Take Test](screenshots/Tests/take-test.png)

---

## Manage Test Types

Administrators can configure the available driving test types and update their associated fees.

![Test Types](screenshots/Tests/test-types.png)

---

# 🪪 License Services

## Issue Driving License

A driving license can only be issued after the applicant has successfully completed all required driving tests.

![Issue License](screenshots/Licenses/issue-license.png)

---

## License Information

Display complete information about an issued driving license.

![License Information](screenshots/Licenses/license-info.png)

---

## Renew Driving License

Renew an expired driving license while preserving the driver's history.

![Renew License](screenshots/Licenses/renew-license.png)

---

## Replace Lost / Damaged License

Issue a replacement for a lost driving license while maintaining the license history.
Replace a damaged driving license with a newly issued one.

![Replace Lost License](screenshots/Licenses/replacement-license.png)

---

## Detain License

Detain a driving license and record the detention information, including the applicable fine.

![Detain License](screenshots/Detention/detain-license.png)

---

## Release Detained License

Release a previously detained license after all required conditions have been satisfied.

![Release License](screenshots/Detention/release-license.png)

---

# 🌍 International Driving License

## Issue International Driving License

Drivers who possess a valid local driving license are eligible to apply for an International Driving License.

The system verifies the driver's eligibility before issuing the international license.

![International License](screenshots/International/international-license.png)

---

# 👤 Account

## Current User Information

View the currently authenticated user's profile information.

![Current User](screenshots/Profile/current-user-info.png)

---

## Change Password

Authenticated users can securely change their account password.

![Change Password](screenshots/Profile/change-password.png)

---

# 🔄 Complete License Lifecycle

The DVLD system manages the complete lifecycle of a driving license.

```text
Register Person
        │
        ▼
Create Local Driving License Application
        │
        ▼
Schedule Test Appointment
        │
        ▼
Pass Vision Test
        │
        ▼
Pass Written Test
        │
        ▼
Pass Street Test
        │
        ▼
Issue Local Driving License
        │
        ▼
Driver Record Created
        │
        ▼
License Services
        ├── Renew License
        ├── Replace Lost License
        ├── Replace Damaged License
        ├── Detain License
        └── Release License
        │
        ▼
Issue International Driving License
```

---

# ⭐ Highlights

- Modern desktop application built with **C# WinForms** and **ADO.NET**.
- Three-Tier Architecture separating Presentation, Business Logic, and Data Access layers.
- Microsoft SQL Server relational database.
- Complete driving license lifecycle management.
- Configurable Application Types and Test Types.
- Secure authentication and user management.
- Advanced searching and filtering across system modules.
- Business rules enforced through the Business Logic Layer before any database operation.
