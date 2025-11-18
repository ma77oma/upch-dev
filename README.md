# 📚 UPCH Bookstore Serverless API (.NET 8 & SQL Server)

API RESTful para la gestión de libros, construida usando **.NET 8** (Minimal APIs/Controladores) y **Entity Framework Core**, desplegada como una función Serverless en **AWS Lambda** y **API Gateway**.

---

## ⚙️ Dependencias y Pre-requisitos

Para ejecutar este proyecto localmente o desplegarlo en AWS, necesitas las siguientes herramientas instaladas y configuradas:

* **SDK de .NET 8:** https://dotnet.microsoft.com/download/dotnet/8.0
* **SQL Server:** Una instancia local o remota de SQL Server (Express, LocalDB o RDS).
* **Visual Studio 2022** (Opcional, se puede usar VS Code).
* **Herramientas Globales de .NET:**
    * **Entity Framework Core CLI (`dotnet-ef`):** Necesario para ejecutar las migraciones localmente.
        ```bash
        dotnet tool install --global dotnet-ef
        ```
    * **Amazon.Lambda.Tools:** Herramienta global de .NET para el despliegue Serverless.
        ```bash
        dotnet tool install --global Amazon.Lambda.Tools
        ```
* **AWS CLI:** Configurado con credenciales de acceso.

---

## 🚀 Ejecución Local de la Solución

### Paso 1: Configuración de la Cadena de Conexión

1.  Abre el archivo `UPCH.Bookstore.Api/appsettings.json`.
2.  Reemplaza el valor de `ConnectionStrings:DefaultConnection` para que apunte a tu base de datos local de SQL Server.

> **⚠️ NOTA CRÍTICA (Error SSL/Certificado):**
> Si tu instancia de SQL Server es local (como SQLEXPRESS) y recibes un error de certificado SSL durante la conexión, debes incluir el parámetro **`;TrustServerCertificate=True`** en la cadena de conexión.

> **Ejemplo de Configuración Local:**
> ```json
> "ConnectionStrings": {
>   "DefaultConnection": "Server=DESKTOP-0U4OQF0\\SQLEXPRESS;Database=upchbookde2;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
> }
> ```

### Paso 2: Ejecutar Migraciones de Entity Framework

Navega a la carpeta del proyecto de infraestructura que contiene el `DbContext` y aplica las migraciones.

1.  Navega a la carpeta del proyecto de infraestructura:
    ```bash
    cd UPCH.Bookstore.Infrastructure
    ```
2.  Aplica las migraciones para crear o actualizar las tablas en la base de datos (se usará la cadena de `appsettings.json`):
    ```bash
    dotnet ef database update --startup-project ../UPCH.Bookstore.Api
    ```
*(Si necesitas generar un nuevo archivo de migración, usa: `dotnet ef migrations add NombreDeLaMigracion --startup-project ../UPCH.Bookstore.Api`.)*

### Paso 3: Iniciar la API Localmente

Navega a la carpeta principal de la API (`UPCH.Bookstore.Api`) y ejecuta:

```bash
cd ../UPCH.Bookstore.Api
dotnet run
