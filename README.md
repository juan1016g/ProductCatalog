# Product Catalog API

Una API RESTful construida con .NET y C# aplicando los principios de **Clean Architecture**, **CQRS** (mediante MediatR) y **Domain-Driven Design (DDD)**. Este proyecto gestiona el catálogo y el inventario de productos, asegurando el cumplimiento estricto de las reglas de negocio, con una alta cobertura de pruebas unitarias.

## 🚀 Entorno en Vivo

La aplicación se encuentra desplegada y la documentación interactiva de la API está disponible públicamente:
**[Ver Swagger UI en Render](https://productcatalog-t2pk.onrender.com/swagger/index.html)**

---

## 🛠 Arquitectura y Tecnologías

* **Framework:** .NET (C#)
* **Arquitectura:** Clean Architecture (Domain, Application, Infrastructure, WebAPI)
* **Patrones:** CQRS (Command Query Responsibility Segregation), Repository Pattern
* **Librerías principales:** MediatR, FluentValidation, Entity Framework Core
* **Base de Datos:** Azure SqlServer
* **Pruebas:** xUnit, Moq, FluentValidation.TestHelper
* **Contenedores:** Compatible con Docker y Podman

---

## 🏗 Estructura del Proyecto (Clean Architecture)

El proyecto sigue una estricta separación de responsabilidades a través de las siguientes capas:

```text
ProductCatalog/
│
├── Domain/                   # Entidades del núcleo (Product, AuditableEntity), Excepciones y Contratos.
├── Application/              # Casos de uso (CQRS con MediatR), DTOs, Validaciones y Comportamientos.
├── Infrastructure/           # Implementación de Repositorios, DbContext de EF Core y acceso a BD.
├── Delamujer.ProductCatalog/ # Proyecto API principal: Controladores, Middleware y Swagger.
│
├── Domain.Tests/             # Pruebas unitarias de las reglas de negocio en las entidades (xUnit).
└── Application.Tests/        # Pruebas unitarias para Handlers y Validadores utilizando Moq.
```
**Flujo de Dependencias:** `Delamujer.ProductCatalog(WebAPI)` -> `Infrastructure` -> `Application` -> `Domain`

## 📋 Endpoints y Reglas de Negocio

La API expone los siguientes contratos HTTP, garantizando la consistencia del dominio:

| Método | Endpoint | Descripción y Reglas de Negocio |
| :--- | :--- | :--- |
| **POST** | `/api/products` | Crea un producto nuevo. Requiere `name`, `description`, `price` (mayor a 0) e `initialStock` (no negativo). |
| **GET** | `/api/products/{id}` | Consulta el detalle completo de un producto específico mediante su identificador único (GUID). |
| **GET** | `/api/products` | Consulta el catálogo completo. Implementa **paginación desde el servidor** (`pageNumber`, `pageSize`) para optimizar la transferencia de datos. |
| **PATCH** | `/api/products/{id}/stock` | Ajusta el inventario (suma o resta). Retorna `HTTP 400 Bad Request` si la operación intenta dejar el stock en un número negativo. |

---

## 💻 Ejecución Local

El proyecto está diseñado para ser completamente replicable. Puedes ejecutarlo utilizando el CLI de .NET o mediante contenedores.

### 1. Variables de Entorno y Configuración
Antes de iniciar, asegúrate de configurar la cadena de conexión a la base de datos SQL Server. Modifica el archivo `appsettings.Development.json` en el proyecto WebAPI, o configura la siguiente variable de entorno:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:tu-servidor.database.windows.net,1433;Initial Catalog=ProductCatalogDb;Persist Security Info=False;User ID=tu_usuario;Password=tu_contraseña;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

### 2. Opción A: Ejecución con .NET CLI
Asegúrate de tener un servidor SqlServer en ejecución.

```bash
# 1. Navegar a la carpeta de la API
cd src/WebAPI

# 2. Aplicar las migraciones a la base de datos
dotnet ef database update

# 3. Ejecutar el proyecto
dotnet run
```

La API estará disponible en `http://localhost:5000/swagger`.

### 3. Opción B: Ejecución con Docker / Podman
Si prefieres un entorno virtualizado, utiliza el archivo `docker-compose.yml` incluido en la raíz del proyecto. Asegúrate de que las variables de entorno de la base de datos apunten a tu instancia de Azure SQL.

```bash
# Construir y levantar los contenedores en segundo plano
docker-compose up -d --build

# (Si utilizas Podman: podman-compose up -d --build)
```

---
## ⚡ Ejemplos de Uso (cURL)

Puedes importar los endpoints a Postman o utilizar los siguientes comandos cURL para probar la API localmente (reemplaza `localhost:5000` por la URL de Render si pruebas en producción):

**1. Crear un producto (POST)**
```bash
curl -X 'POST' \
  'http://localhost:5000/api/products' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Teclado Mecánico EPOMAKER",
  "description": "Switch Red, distribución ISO español",
  "price": 350000,
  "initialStock": 15
}'
```

**2. Consultar el catálogo con paginación (GET)**
```bash
curl -X 'GET' \
  'http://localhost:5000/api/products?PageNumber=1&PageSize=10' \
  -H 'accept: application/json'
```

**3. Consultar un producto por ID (GET)**
```bash
curl -X 'GET' \
  'http://localhost:5000/api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6' \
  -H 'accept: application/json'
```

**4. Ajustar el inventario (PATCH)**
*Nota: Enviar un número positivo suma al stock, un número negativo resta.*
```bash
curl -X 'PATCH' \
  'http://localhost:5000/api/products/3fa85f64-5717-4562-b3fc-2c963f66afa6/stock' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "quantity": -5
}'
```

---

## 🧪 Pruebas Unitarias
El proyecto cuenta con una suite exhaustiva de pruebas unitarias garantizando la integridad de la lógica central.
Para ejecutar la suite de pruebas y generar el reporte de cobertura:

```bash
dotnet test
```

***

Desarrollado por **Juan Pablo Garcia Blanco**.
