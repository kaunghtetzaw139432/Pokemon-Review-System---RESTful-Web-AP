# 🐉 Pokemon Review System - Professional REST API

A comprehensive backend solution built with **ASP.NET Core Web API** to manage a multi-entity ecosystem. This project simulates a real-world review platform, handling intricate database relationships and high-quality data flow patterns.

---

## 🎯 Project Overview

This API serves as a centralized management system where users can explore Pokemons, track their owners across different countries, and manage a community-driven review system. It's built with a focus on **Scalability**, **Separation of Concerns**, and **Clean Code**.

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
* **Data Integrity:** Implemented custom logic for bulk deletes and relational cleanup.

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

| Controller | Method | Endpoint | Description |
| :--- | :--- | :--- | :--- |
| **Pokemon** | GET | `/api/Pokemon` | Get all pokemons |
| **Pokemon** | POST | `/api/Pokemon` | Create a new pokemon entry |
| **Owner** | GET | `/api/Owner` | Get all registered owners |
| **Review** | DELETE | `/api/Review/{id}` | Delete a specific review |
| **Reviewer** | DELETE | `/api/Reviewer/{id}` | Delete reviewer and all their reviews |

## 🛠 Installation & Setup

1.  **Clone the Project:**
    ```bash
    git clone [https://github.com/kaunghtetzaw139432/Pokemon-Review-System---RESTful-Web-AP.git](https://github.com/kaunghtetzaw139432/Pokemon-Review-System---RESTful-Web-AP.git)
    ```
2.  **Database Configuration:**
    Update the `ConnectionStrings` in `appsettings.json` to point to your local SQL Server.
3.  **Apply Migrations:**
    ```powershell
    Update-Database
    ```
4.  **Run the App:**
    ```bash
    dotnet run
    ```

---

## 👨‍💻 Developed By

**Kaung Htet Zaw**
* **Education:** 5th Year Software Engineering Student (6-Year Academic Program)
* **Specialization:** Backend Engineering (C# .NET, Java Spring Boot)
* **Connect:** [LinkedIn](https://www.linkedin.com/in/kaung-htet-zaw-backend) | [GitHub](https://github.com/kaunghtetzaw139432)

---
*This project was developed as part of a deep dive into ASP.NET Core Web API patterns and professional backend development practices.*
