# 📸 Application Screenshots

This document showcases the major workflows and user interfaces of the Driving & Vehicle License Department (DVLD) Management System.

---

# Login

The application starts with a secure login screen where authorized users authenticate before accessing the system.

![Login](screenshots/login.png)

---

# Dashboard

After successful authentication, users are presented with the main dashboard providing access to all system modules.

![Dashboard](screenshots/dashboard.png)

---

# People Management

The People module allows administrators to manage all registered people.

### Features

- Add Person
- Update Person
- Delete Person
- Search & Filter
- View Details

![Manage People](screenshots/people/manage-people.png)

---

# Add New Person

Register a new person before creating any driving license application.

![Add Person](screenshots/people/add-person.png)

---

# Local Driving License Application

Create a new local driving license application by selecting the applicant and the desired license class.

![Application](screenshots/applications/new-local-license.png)

---

# Vision Test

Applicants must first pass the vision examination.

![Vision Test](screenshots/tests/vision-test.png)

---

# Written Test

After successfully passing the vision test, applicants become eligible for the written examination.

![Written Test](screenshots/tests/written-test.png)

---

# Street Test

The final examination before a driving license can be issued.

![Street Test](screenshots/tests/street-test.png)

---

# Issue Driving License

Once all tests are passed, the system allows issuing the first driving license.

![Issue License](screenshots/licenses/issue-license.png)

---

# License Information

View complete information about an issued driving license.

![License Information](screenshots/licenses/license-info.png)

---

# License Renewal

Renew expired driving licenses.

![Renew License](screenshots/licenses/renew-license.png)

---

# Replace Lost or Damaged License

Issue replacement licenses when necessary.

![Replacement](screenshots/licenses/replacement-license.png)

---

# Detain License

Detain a license and record the detention information.

![Detain License](screenshots/detention/detain-license.png)

---

# Release Detained License

Release a previously detained license after the required conditions have been met.

![Release License](screenshots/detention/release-license.png)

---

# International Driving License

Issue an international driving license for eligible drivers.

![International License](screenshots/international/international-license.png)

---

# Complete Workflow

The following diagram summarizes the complete business workflow implemented by the system.

```text
Person Registration
        ↓
Local License Application
        ↓
Vision Test
        ↓
Written Test
        ↓
Street Test
        ↓
Issue License
        ↓
Driver Management
        ↓
Renew / Replace / Detain
        ↓
International License
```
