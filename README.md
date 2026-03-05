# BankBranches API 🏦

API robusta para la gestión de sucursales bancarias, construida con los más altos estándares de calidad de software. Implementa una arquitectura profesional lista para producción basada en principios de diseño moderno.

## 🚀 Tecnologías y Arquitectura

- **.NET 8 Web API**
- **Arquitectura Limpia (Clean Architecture):** Separación de responsabilidades en 4 capas (Domain, Application, Infrastructure, Api).
- **Domain-Driven Design (DDD):** Modelos de dominio con lógica de negocio encapsulada.
- **Dapper:** Micro-ORM de alto rendimiento para el acceso a datos.
- **SQL Server:** Base de datos relacional para persistencia.
- **JWT (JSON Web Tokens):** Seguridad basada en roles (Admin/User).
- **Consumo de API Externa:** Integración con `api-colombia.com` para obtener regiones geográficas en tiempo real.

## 📋 Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server/) (LocalDB o Express recomendado)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) o Azure Data Studio.

## ⚙️ Configuración y Configuración

### 1. Preparar la Base de Datos
Ejecuta el script de inicialización ubicado en la raíz del proyecto:
`scripts/InitDatabase.sql`

Este script creará:
- La base de datos `BankBranchesDb`.
- Las tablas necesarias (`Users`, `Cities`, `Branches`).
- 11 Stored Procedures para operaciones optimizadas.
- Datos semilla (34 ciudades de Colombia y un usuario Administrador).

### 2. Configurar la Cadena de Conexión
Abre el archivo `BankBranches.Api/appsettings.json` y ajusta la sección `ConnectionStrings` según tu instancia de SQL Server:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=BankBranchesDb;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

## 🛠️ Ejecución

Desde la terminal en la raíz del proyecto:

```bash
# Restaurar dependencias
dotnet restore

# Compilar la solución
dotnet build

# Ejecutar la API
dotnet run --project BankBranches.Api
```

Una vez ejecutada, la API abrirá automáticamente **Swagger UI** en:
`https://localhost:PORT/swagger`

## 🔐 Autenticación y Prueba

La API cuenta con dos niveles de acceso:

- **Administrador:** Puede realizar CRUD completo de sucursales.
- **Usuario:** Solo puede consultar sucursales y regiones.

### Credenciales de prueba:
- **Usuario Admin:** `admin@bankbranches.com` / `Admin123!`

### Pasos para probar:
1. Usa el endpoint `POST /api/Auth/Login` con las credenciales anteriores.
2. Copia el `token` devuelto.
3. En Swagger, pulsa el botón **Authorize**, escribe `Bearer TU_TOKEN` y pulsa Authorize.
4. Ya puedes probar los endpoints protegidos.

---
Desarrollado Por Jorge Quintero.
