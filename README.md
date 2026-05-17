# SmartWorkspace

SmartWorkspace is a Windows Forms desktop application for managing coworking spaces using .NET Framework, SQL Server, and ADO.NET.

## Features

| Module | Description |
|---|---|
| Members | Add and view coworking members |
| Workspaces | Add and manage workspaces |
| Reservations | Reserve available workspaces |
| Inquiries | Run analytical SQL queries |

---

## Technologies Used

- C# Windows Forms
- .NET Framework 4.7.2
- SQL Server
- ADO.NET

---

## Prerequisites

- Visual Studio 2019 or 2022
- SQL Server Express
- .NET Framework 4.7.2 or later

---

## Setup Instructions

1. Open SQL Server Management Studio.
2. Run the `CreateDB.sql` script.
3. Open `DB.cs` and confirm the SQL Server connection string.
4. Open `SmartWorkspace.sln` in Visual Studio.
5. Build the solution.
6. Run the application.

---

## Folder Structure

```text
SmartWorkspace/
│
├── DBMigration.sql
├── README.md
├── SmartWorkspace.sln
├── .gitignore
│
└── SmartWorkspace/
    ├── DB.cs
    ├── Program.cs
    ├── Form1.cs
    ├── Form1.Designer.cs
    ├── MembersForm.cs
    ├── MembersForm.Designer.cs
    ├── WorkspaceForm.cs
    ├── WorkspaceForm.Designer.cs
    ├── ReservationForm.cs
    ├── ReservationForm.Designer.cs
    ├── InquiryForm.cs
    ├── InquiryForm.Designer.cs
    └── SmartWorkspace.csproj

---


