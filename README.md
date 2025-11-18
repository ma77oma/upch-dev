# 📚 UPCH Bookstore Serverless API (.NET 8 & SQL Server)

API RESTful para la gestión de libros, construida usando .NET 8 (Minimal APIs/Controladores) y Entity Framework Core, desplegada como una función Serverless en AWS Lambda.

---

## ⚙️ Dependencias y Pre-requisitos

Para ejecutar este proyecto localmente o desplegarlo, necesitas las siguientes herramientas instaladas:

* **SDK de .NET 8:** https://dotnet.microsoft.com/download/dotnet/8.0
* **SQL Server:** Una instancia local o remota de SQL Server (Express, LocalDB o RDS).
* **Visual Studio 2022** (Opcional, se puede usar VS Code).
* **AWS CLI & Lambda Tools:**
    * **AWS CLI:** Configuradas con credenciales de acceso.
    * **Amazon.Lambda.Tools:** Herramienta global de .NET para el despliegue Serverless.
        ```bash
        dotnet tool install -g Amazon.Lambda.Tools
        ```

---

## 🚀 Ejecución Local de la Solución

### Paso 1: Configuración de la Base de Datos

1.  **Crear la Base de Datos:** Asegúrate de que la base de datos `upchbookdev` exista en tu instancia de SQL Server.
2.  **Configurar Cadena de Conexión Local:**
    * Abre el archivo `UPCH.Bookstore.Api/appsettings.json`.
    * Reemplaza la clave `ConnectionStrings:DefaultConnection` con tu cadena de conexión local.

    > **Ejemplo:**
    > ```json
    > "ConnectionStrings": {
    >   "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=upchbookdev;Trusted_Connection=True;MultipleActiveResultSets=true"
    > }
    > ```

### Paso 2: Ejecutar Migraciones de Entity Framework

Navega a la carpeta del proyecto que contiene la configuración de Entity Framework (generalmente la capa de `Infrastructure`) y aplica las migraciones para crear las tablas (`Libro`, `Autor`, `Categoria`, etc.):

```bash
# Navega al proyecto que contiene el DbContext
cd UPCH.Bookstore.Infrastructure
dotnet ef database update --startup-project ../UPCH.Bookstore.Api
