# 🏗️ Arquitectura del Proyecto

Este sistema está construido siguiendo una arquitectura de microservicios contenerizada, separando claramente las responsabilidades entre la autenticación, la lógica de negocio y la presentación.

## 🧩 Visión General de Componentes

| Servicio       | Tecnología  | Puerto (Docker) | Descripción                                       |
| -------------- | ----------- | --------------- | ------------------------------------------------- |
| **Frontend**   | Angular 17+ | `4200`          | Interfaz de usuario modular con Angular Material. |
| **Auth API**   | .NET 9      | `5081`          | Gestión de usuarios, roles y autenticación JWT.   |
| **Orders API** | .NET 9      | `5080`          | Gestión de clientes, pedidos y dashboard.         |
| **Database**   | SQL Server  | `1433`          | Base de datos relacional compartida.              |

---

## 🔐 1. Auth API (`Clientes_pedidos_API`)

Servicio encargado de la seguridad y gestión de identidad. Sigue una **Arquitectura Hexagonal**.

### 📂 Estructura de Directorios

```
Clientes_pedidos_API/
├── Application/        # Casos de uso y reglas de negocio
│   ├── DTOs/           # Objetos de transferencia de datos
│   ├── Features/       # Lógica por funcionalidad (Usuarios, Roles)
│   ├── Interfaces/     # Contratos de servicios y repositorios
│   └── Mappings/       # Configuraciones de AutoMapper
├── Domain/             # Entidades y lógica de dominio puro
│   ├── Entities/       # Modelos de base de datos (Usuario, Rol)
│   └── Base/           # Entidades base y auditoría
├── Infrastructure/     # Implementación técnica
│   ├── Context/        # DbContext de Entity Framework
│   ├── Repositories/   # Acceso a datos
│   └── Services/       # Servicios externos (JWT, Cifrado)
└── Plant_HexArquitecture_API/ # Entry point (Controllers)
```

### 🔑 Funcionalidades Clave

- Autenticación mediante **JWT (JSON Web Tokens)**.
- Gestión de usuarios y roles.
- Middleware de manejo de excepciones.

---

## 📦 2. Orders API (`Servicio_ClientesPedidos`)

Servicio principal que maneja la lógica de negocio de la tienda. También implementa **Clean Architecture**.

### 📂 Estructura de Directorios

```
Servicio_ClientesPedidos/
├── API/                # Controladores y configuración (Program.cs)
├── Application/        # Lógica de aplicación
│   ├── DTOs/           # PedidoDto, ClienteDto, DashboardDto
│   ├── Features/       # Operaciones CQRS simplificadas
│   │   ├── Clientes/   # Lógica gestión clientes
│   │   ├── Pedidos/    # Lógica gestión pedidos
│   │   └── Dashboard/  # Estadísticas y métricas
│   └── Interfaces/     # Abstracciones
├── Domain/             # Núcleo del negocio
│   ├── Entities/       # Cliente, Pedido, DetallePedido, Producto
│   └── Repository/     # Interfaces de repositorios genéricos
└── Infrastructure/     # Persistencia
    ├── Context/        # SqlDbContext
    └── Services/       # ExternalAuthService (comunicación entre APIs)
```

### 🚀 Características

- **Entity Framework Core** Code-First con migraciones.
- Comunicación síncrona con Auth API para registro de usuarios.
- Patrón **Repository Genérico**.
- Endpoints optimizados para Dashboard y Reportes.

---

## 🎨 3. Frontend (`Front`)

Aplicación Single Page Application (SPA) construida con Angular, utilizando un diseño modular y moderno.

### 📂 Estructura de Directorios (`src/app/`)

```
src/app/
├── core/               # Singleton services y guardias
│   ├── guards/         # AuthGuard, AdminGuard
│   ├── interceptors/   # JWT y manejo de errores
│   └── services/       # AuthService, OrdersService
├── features/           # Módulos funcionales (Lazy Loading)
│   ├── auth/           # Login, Registro
│   ├── dashboard/      # Gráficos y estadísticas
│   ├── clients/        # Gestión de clientes
│   ├── orders/         # Gestión de pedidos
│   └── products/       # Catálogo de productos
├── shared/             # Componentes reutilizables
│   ├── components/     # ConfirmDialog, Loader
│   └── models/         # Interfaces TypeScript (Order, Client)
└── app.module.ts       # Módulo raíz
```

### ✨ Stack Tecnológico

- **Angular Material**: Componentes UI (Tablas, Diálogos, Cards).
- **Chart.js**: Visualización de datos en el Dashboard.
- **RxJS**: Manejo de flujos de datos asíncronos.
- **Nginx**: Servidor web para producción en Docker.

---

## 🐳 Orquestación (Docker Compose)

El archivo `docker-compose.yml` integra todos los servicios:

1. **Proxy Inverso (Nginx en Frontend)**:
   - Redirige `/api/*` -> `orders-api:8080`
   - Redirige `/auth/*` -> `auth-api:8081`
   - Sirve estáticos en `/`

2. **Red Interna**: Todos los servicios se comunican en una red bridge `backend-network`.

3. **Variables de Entorno**: Gestionadas centralmente en `.env` para secretos y configuración.

---

_Documentación generada automáticamente para el proyecto de Prueba Técnica._
