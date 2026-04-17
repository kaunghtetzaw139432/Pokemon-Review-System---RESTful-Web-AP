# 🐉 Pokemon Review System - Professional REST API
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white) ![Entity Framework](https://img.shields.io/badge/EF%20Core-512BD4?style=for-the-badge&logo=dotnet) ![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)![Pokemon](https://img.shields.io/badge/Pokemon-System-red?style=for-the-badge&logo=pokemon)


A comprehensive backend solution built with **ASP.NET Core Web API** to manage a multi-entity ecosystem. This project simulates a real-world review platform, handling intricate database relationships and high-quality data flow patterns.

---

## 🎯 Project Overview

This API serves as a centralized management system where users can explore Pokemons, track their owners across different countries, and manage a community-driven review system. It's built with a focus on **Scalability**, **Separation of Concerns**, and **Clean Code**.

<table style="width: 100%; border-collapse: collapse;">
  <tr>
    <td style="width: 50%; text-align: center; vertical-align: top; padding: 10px;">
      <b>📊 Database ER Diagram</b><br><br>
      <img src="https://github.com/user-attachments/assets/ec5da2ad-a6a7-4384-9a5c-03c7a3bf52de" width="100%" alt="Database ER Diagram">
      <p><i>Relational design showing Many-to-Many & One-to-Many mapping.</i></p>
    </td>
    <td style="width: 50%; text-align: center; vertical-align: top; padding: 10px;">
      <b>🏗 Repository Pattern Architecture</b><br><br>
      <img src="https://github.com/user-attachments/assets/392f79e9-fe2e-48cc-bca4-a29d64998091" width="100%" alt="Repository Pattern Architecture">
      <p><i>Layered architecture ensuring separation of concerns.</i></p>
    </td>
  </tr>
</table>

---

## 🛠 Tech Stack & Architecture

* **Framework:** .NET 10.0 (ASP.NET Core Web API)
* **Database:** MS SQL Server
* **ORM:** Entity Framework Core (Code First Approach)
* **Architecture:** Repository Pattern
* **Mapping:** AutoMapper (to keep DTOs and Entities separate)
* **Documentation:** Swagger UI (OpenAPI)

## 🏗 Database Design & Relationships

The system architecture handles complex relational data:
* **Many-to-Many:** Pokemon & Owners, Pokemon & Categories.
* **One-to-Many:** Country & Owners, Pokemon & Reviews, Reviewer & Reviews.
* **Seed Data:** Includes a pre-configured `Seed.cs` to populate the database with initial Pokemon, Owners, and Reviews for immediate testing.

## 🚀 Key Features & Functionalities

### 1. Advanced CRUD Operations
* Full Create, Read, Update, and Delete support for all major entities.
* Integrated Validation using `ModelState`.

### 2. Specialized Repository Logic
* **Bulk Delete:** Capability to delete all reviews of a reviewer in a single transaction.
* **Relational Queries:** Fetching data through join tables (e.g., Get Pokemons by Category).

### 3. Data Transfer Objects (DTOs)
* Shielding the internal database schema from the client.
* Optimized API responses by returning only necessary data fields.

## 🔗 Main API Endpoints

| Controller | Available Endpoints |
| :--- | :--- |
| **Pokemon** | `GET /api/Pokemon`, `GET /api/Pokemon/{id}`, `POST /api/Pokemon`, `PUT /api/Pokemon/{id}`, `DELETE /api/Pokemon/{id}` |
| **Category** | `GET /api/Category`, `GET /api/Category/{id}`, `GET /api/Category/pokemon/{categoryId}` |
| **Country** | `GET /api/Country`, `GET /api/Country/{id}`, `GET /api/Country/owners/{ownerId}` |
| **Owner** | `GET /api/Owner`, `GET /api/Owner/{id}`, `GET /api/Owner/{ownerId}/pokemon` |
| **Review** | `GET /api/Review`, `GET /api/Review/{id}`, `GET /api/Review/pokemon/{pokeId}`, `DELETE /api/Review/{id}` |
| **Reviewer** | `GET /api/Reviewer`, `GET /api/Reviewer/{id}`, `GET /api/Reviewer/{reviewerId}/reviews`, `DELETE /api/Reviewer/{id}` |

## 🛠 Installation & Setup

1.  **Clone the Project:**
    ```bash
    git clone [https://github.com/kaunghtetzaw139432/Pokemon-Review-System---RESTful-Web-AP.git](https://github.com/kaunghtetzaw139432/Pokemon-Review-System---RESTful-Web-AP.git)
    ```
2.  **Database Configuration:**
    Update the `ConnectionStrings` in `appsettings.json`.
3.  **Apply Migrations & Seed Data:**
    ```powershell
    Update-Database
    ```
    *(The application will automatically seed data on the first run if configured in Program.cs)*
4.  **Run the App:**
    ```bash
    dotnet run
    ```

---

## 👨‍💻 Developed By

**Kaung Htet Zaw**
* **Education:** 5th Year Software Engineering Student (6-Year Academic Program)
* **Core Interests:** Backend Systems, C# .NET Core, Java Spring Boot, and DevOps.
* **Connect:** [LinkedIn](https://www.linkedin.com/in/kaung-htet-zaw-backend) | [GitHub](https://github.com/kaunghtetzaw139432)

---
*This project was developed as part of a deep dive into ASP.NET Core Web API patterns and professional backend development practices.*
