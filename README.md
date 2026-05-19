# 🛒 Amazon.API

A full-featured **e-commerce REST API** built with **ASP.NET Core 6** following clean architecture principles. It supports product browsing, user authentication, shopping baskets (Redis), order management, and Stripe payment processing.

---

## 📐 Architecture

The solution is split into four projects following a clean, layered architecture:

```
Amazon.API`s/
├── Amazon.API`s          # Presentation layer — Controllers, DTOs, Middlewares, Extensions
├── Amazon.core           # Domain layer — Entities, Interfaces, Specifications
├── Amazon.repository     # Data layer — EF Core, Migrations, Generic Repository, Unit of Work
└── Amazon.service        # Business logic layer — Order, Payment, Token services
```

### Design Patterns Used

- **Generic Repository Pattern** — `GenericRepository<T>` provides type-safe CRUD operations over any `BaseEntity`
- **Unit of Work Pattern** — `UnitOfWork` manages all repositories under a single `DbContext`, ensuring atomic transactions
- **Specification Pattern** — Encapsulates query logic (filtering, sorting, pagination, eager loading) into reusable specification classes
- **Repository Pattern** — `IBasketRepository` for Redis-backed basket operations

---

## ✨ Features

| Module | Description |
|---|---|
| 🔐 **Authentication** | ASP.NET Core Identity + JWT Bearer tokens with role-based claims |
| 🛍️ **Products** | Browse products with filtering by brand/type, sorting, and pagination |
| 🧺 **Basket** | Redis-backed shopping basket with CRUD operations |
| 📦 **Orders** | Full order lifecycle with delivery method selection and order history |
| 💳 **Payments** | Stripe PaymentIntent integration with webhook support |
| 👤 **Accounts** | User registration, login, profile management, address CRUD |
| ⚠️ **Error Handling** | Global exception middleware with standardized `ApiResponse` error model |
| 🗂️ **Data Seeding** | Automatic seeding of products, brands, types, and a default user on startup |

---

## 🛠️ Tech Stack

- **Framework:** ASP.NET Core 6 (.NET 6)
- **ORM:** Entity Framework Core 8 (SQL Server)
- **Authentication:** ASP.NET Core Identity + JWT Bearer
- **Cache / Basket Store:** Redis (via StackExchange.Redis)
- **Payments:** Stripe .NET SDK
- **Object Mapping:** AutoMapper
- **API Docs:** Swagger / OpenAPI
- **Database:** SQL Server (two databases: Store + Identity)

---

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Redis](https://redis.io/download/) (or Docker: `docker run -p 6379:6379 redis`)
- A [Stripe account](https://stripe.com) for payment keys

### Installation

```bash
git clone https://github.com/Souldeer0020/Amazon.API.git
cd Amazon.API
```


### Run

```bash
dotnet run --project Amazon.API`s/Amazon.API
```

Migrations are applied and data is seeded **automatically on startup**.

Once running, navigate to `https://localhost:7258/swagger` to explore the API.

---

## 📡 API Endpoints

### Products
| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/products` | ❌ | Get all products (supports filtering, sorting, pagination) |
| `GET` | `/api/products/{id}` | ❌ | Get product by ID |
| `GET` | `/api/products/brands` | ❌ | Get all brands |
| `GET` | `/api/products/types` | ❌ | Get all product types |

### Accounts
| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/accounts/register` | ❌ | Register a new user |
| `POST` | `/api/accounts/login` | ❌ | Login and receive JWT |
| `GET` | `/api/accounts` | ✅ | Get current authenticated user |
| `GET` | `/api/accounts/address` | ✅ | Get user address |
| `PUT` | `/api/accounts/address` | ✅ | Update user address |

### Basket
| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `GET` | `/api/baskets/{id}` | ❌ | Get basket by ID |
| `POST` | `/api/baskets` | ❌ | Create or update basket |
| `DELETE` | `/api/baskets` | ❌ | Delete basket |

### Orders
| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/orders` | ✅ | Create a new order |
| `GET` | `/api/orders` | ✅ | Get all orders for logged-in user |
| `GET` | `/api/orders/{id}` | ✅ | Get order by ID for logged-in user |
| `GET` | `/api/orders/deliveryMethods` | ✅ | Get available delivery methods |

### Payments
| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/payments` | ✅ | Create or update Stripe PaymentIntent |
| `POST` | `/api/payments/webhook` | ❌ | Stripe webhook handler |

---

## 🗃️ Data Models

```
Product ──┬── ProductBrand
          └── ProductType

Order ────┬── OrderItem ── ProductItemOrdered
          ├── DeliveryMethod
          └── Address

AppUser ──── Address (Identity)

CustomerBasket ── List<BasketItem>
```

---

## 📁 Project Structure

```
Amazon.API`s/
│
├── Controllers/
│   ├── AccountsController.cs
│   ├── basketsController.cs
│   ├── OrdersController.cs
│   ├── PaymentsController.cs
│   └── ProductsController.cs
│
├── DTOs/                        # Request/Response models
├── Errors/                      # Standardized error response classes
├── Extensions/                  # Service & middleware registration extensions
├── Helpers/                     # AutoMapper profiles, Pagination wrapper
└── Middlewares/                 # Global exception handling middleware

Amazon.core/
├── Entities/
│   ├── Order Aggregate/         # Order, OrderItem, DeliveryMethod, Address
│   ├── Identity/                # AppUser, Address
│   ├── Product.cs
│   └── BaseEntity.cs
├── Repositories/                # IGenericRepository, IBasketRepository
├── Services/                    # IOrderService, IPaymentService, ITokenService
├── Specifications/              # ISpecification, BaseSpecification, concrete specs
└── IUnitOfWork.cs

Amazon.repository/
├── Data/
│   ├── Config/                  # EF Fluent API configurations
│   ├── DataSeed/                # JSON seed files
│   ├── Identity/                # AppIdentityDbContext, seed
│   └── StoreContext.cs
├── GenericRepository.cs
├── SpecificationEvaluator.cs
├── UnitOfWork.cs
└── BasketRepository.cs

Amazon.service/
├── OrderService.cs
├── PaymentService.cs
└── TokenService.cs
```

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).
