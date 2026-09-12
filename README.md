# SmartSpend - Sistema de Gestión de Gastos Personales

SmartSpend es una aplicación web desarrollada en C# con .NET 8 para el control de finanzas personales. Permite registrar transacciones diarias, categorizar gastos, definir límites presupuestarios mensuales con alertas visuales en tiempo real y exportar/importar datos en formato CSV.

El proyecto está diseñado bajo una arquitectura limpia en capas (Clean Architecture) e incluye una suite de pruebas automatizadas que abarca pruebas unitarias en el backend y pruebas End-to-End (E2E) sobre la interfaz de usuario con Selenium WebDriver.

## Características Principales

- **Autenticación y Seguridad:** Registro e inicio de sesión protegido mediante tokens JWT y cifrado de contraseñas con BCrypt.
- **Gestión de Gastos (CRUD):** Registro de transacciones con monto, fecha, categoría y método de pago.
- **Categorías y Métodos de Pago:** Creación de catálogos personalizados por usuario.
- **Control de Presupuestos:** Definición de límites mensuales por categoría con indicadores de consumo en tiempo real (barras de progreso y alertas por sobregiro).
- **Dashboard e Historial:** Visualización gráfica de distribución de gastos (Chart.js) y tabla de historial con filtros por fecha y búsqueda por texto.
- **Portabilidad de Datos:** Exportación e importación masiva de gastos mediante archivos CSV.
- **Pruebas Automatizadas:** Cobertura de lógica de negocio y automatización de flujos de UI.

## Tecnologías Utilizadas

### Backend
- **Lenguaje / Framework:** C# | .NET 8.0 (ASP.NET Core Web API)
- **Base de Datos / ORM:** SQL Server | Entity Framework Core 8.0 (Code-First)
- **Seguridad:** JWT (JSON Web Tokens) | BCrypt.Net

### Frontend
- **Interfaz:** HTML5, CSS3, Bootstrap 5
- **Lógica:** JavaScript nativo (ES6+, Fetch API)
- **Gráficos e Iconos:** Chart.js | FontAwesome

### Testing y QA
- **Pruebas Unitarias:** xUnit | Moq
- **Pruebas E2E (UI):** Selenium WebDriver | ChromeDriver

## Arquitectura del Proyecto

La solución sigue una estructura limpia en capas para mantener la separación de responsabilidades:

```text
SmartSpend/
├── SistemaDeGastosPersonales.Domain/        # Entidades principales e interfaces core
├── SistemaDeGastosPersonales.Application/   # Servicios de negocio, DTOs e interfaces
├── SistemaDeGastosPersonales.Infrastructure/# DbContext, repositorios y persistencia SQL Server
├── SistemaDeGastosPersonales.API/          # Controladores REST, middleware JWT y vistas HTML/JS
└── SistemaDeGastosPersonales.Tests/        # Pruebas unitarias (xUnit/Moq) y E2E (Selenium)
```

## Requisitos Previos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Microsoft SQL Server](https://www.microsoft.com/sql-server/) (LocalDB o SQL Server Express)
- [Google Chrome](https://www.google.com/chrome/) (necesario para la ejecución de pruebas Selenium)

## Configuración y Ejecución

### 1. Clonar el repositorio
```bash
git clone https://github.com/StaisyFortt/SmartSpend.git
cd SmartSpend
```

### 2. Configurar la base de datos
Asegúrate de que la cadena de conexión en `SistemaDeGastosPersonales.API/appsettings.json` apunte a tu instancia local de SQL Server:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=GastosDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Al iniciar la aplicación por primera vez, Entity Framework Core creará automáticamente la base de datos `GastosDB` e insertará los datos iniciales de prueba (seeding).

### 3. Ejecutar la API
```bash
dotnet run --project SistemaDeGastosPersonales.API
```
La API estará disponible en `http://localhost:5104` y la documentación interactiva en `http://localhost:5104/swagger`.

### 4. Acceder al sistema
Abre tu navegador e ingresa a `http://localhost:5104/index.html`. Puedes registrarte o ingresar con la cuenta de prueba predeterminada:
- **Correo:** `wanda@prueba.com`
- **Contraseña:** `MiPasswordSeguro123`

## Ejecución de Pruebas Automatizadas

El proyecto incluye 9 casos de prueba automatizados.

### Pruebas Unitarias (Backend)
Validan la lógica del servicio `GastoService` aislando la base de datos mediante mocks de los repositorios:
```bash
dotnet test --filter "FullyQualifiedName~GastoServiceTests"
```

### Pruebas de Interfaz (Selenium E2E)
Automatizan la navegación sobre Google Chrome evaluando escenarios felices, negativos y de límites para Login y CRUD de gastos:

1. Inicia la API en una terminal (`dotnet run --project SistemaDeGastosPersonales.API`).
2. En otra terminal, ejecuta:
```bash
dotnet test --filter "FullyQualifiedName~SeleniumTests"
```

> **Nota:** Las pruebas de Selenium están configuradas por defecto en modo oculto (`--headless`). Para visualizar la ventana del navegador interactuando durante la ejecución, comenta la línea `options.AddArgument("--headless");` en `SistemaDeGastosPersonales.Tests/SeleniumTests.cs`.

## Autor

Desarrollado por **Staisyfort** como proyecto final para la asignatura de Programación 3.
