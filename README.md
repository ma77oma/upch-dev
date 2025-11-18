📚 UPCH Bookstore Serverless API (.NET 8 & SQL Server)
API RESTful para la gestión de libros, construida usando .NET 8 (Minimal APIs/Controladores) y Entity Framework Core, desplegada como una función Serverless en AWS Lambda y API Gateway.

⚙️ Dependencias y Pre-requisitos
Para ejecutar este proyecto localmente o desplegarlo en AWS, necesitas las siguientes herramientas instaladas y configuradas:

SDK de .NET 8: https://dotnet.microsoft.com/download/dotnet/8.0

SQL Server: Una instancia local o remota de SQL Server (Express, LocalDB, o RDS).

Visual Studio 2022 (Opcional, se puede usar VS Code).

AWS CLI & Lambda Tools:

AWS CLI: Configurado con credenciales de acceso.

Amazon.Lambda.Tools: Herramienta global de .NET para el despliegue Serverless.

Bash

dotnet tool install -g Amazon.Lambda.Tools
🚀 Ejecución Local de la Solución
Paso 1: Configuración de la Cadena de Conexión
Abre el archivo UPCH.Bookstore.Api/appsettings.json.

Reemplaza el valor de ConnectionStrings:DefaultConnection para que apunte a tu base de datos local de SQL Server.

Ejemplo de Configuración Local:

JSON

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=upchbookdev;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Paso 2: Inicializar la Base de Datos y Migraciones
Si has incluido un script de inicialización (init-db.sh o similar) en la raíz del repositorio, úsalo. Este script debe encargarse de crear la base de datos y aplicar las migraciones de Entity Framework.

Usuarios de Windows (PowerShell):

Bash

.\init-db.ps1
Usuarios de Linux/macOS:

Bash

chmod +x init-db.sh 
./init-db.sh
Alternativa (Ejecución Manual de Migraciones):

Bash

# Navega al proyecto que contiene el DbContext (ej: Infrastructure)
cd UPCH.Bookstore.Infrastructure
dotnet ef database update --startup-project ../UPCH.Bookstore.Api
Paso 3: Iniciar la API Localmente
Navega a la carpeta principal de la API (UPCH.Bookstore.Api) y ejecuta:

Bash

cd ../UPCH.Bookstore.Api
dotnet run
La API estará disponible en https://localhost:7256/ (o el puerto configurado).

☁️ Despliegue Serverless en AWS
El despliegue en AWS Lambda se realiza mediante un Stack de CloudFormation/SAM definido en el serverless.template.

Paso 1: Configurar AWS Secrets Manager
La cadena de conexión a la base de datos RDS se gestiona de forma segura a través de AWS Secrets Manager.

Verifica que el secreto upch/dev/sqlserver exista en tu cuenta de AWS.

Formato del Secreto: El secreto debe contener las claves separadas (username, password, host, port, etc.). La lógica en Startup.cs utiliza el SDK de AWS para leer estas claves y construir la cadena de conexión en tiempo de ejecución.

Paso 2: Comando de Despliegue
Ejecuta el comando principal para construir el paquete de despliegue y actualizar la pila de CloudFormation:

Bash

dotnet lambda deploy-serverless
Parámetros: El comando solicitará el nombre de la pila (Stack Name) y el bucket S3 para el despliegue.

Resultado: Una vez completado, el CLI mostrará la URL del ApiGatewayUrl (ej: https://...execute-api.us-east-1.amazonaws.com/Prod/api/libros).

Requisitos Críticos de la Infraestructura (VPC)
Para que la función Lambda (BookstoreApiFunction) pueda acceder a la base de datos RDS y a Secrets Manager, se requiere una configuración de red específica:

VPC Endpoint (Secrets Manager): Debe existir un VPC Endpoint de tipo Interface para com.amazonaws.us-east-1.secretsmanager en la VPC de la Lambda.

Security Groups:

El Security Group del RDS (upch-rds-sg) debe permitir tráfico de MS SQL (1433) desde el Security Group de la Lambda.

El Security Group del VPC Endpoint de Secrets Manager debe permitir tráfico HTTPS (443) desde el Security Group de la Lambda.
