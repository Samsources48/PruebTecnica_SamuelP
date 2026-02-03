# Guía de Despliegue con Docker

Este proyecto consta de 3 servicios orquestados con Docker Compose:
1. **Frontend**: Angular 17 (puerto 4200)
2. **Orders API**: .NET 9 (puerto 5080)
3. **Auth API**: .NET 9 (puerto 5081)
4. **Base de Datos**: SQL Server 2022 (puerto 1433)

## Prerrequisitos

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado y ejecutándose.

## Cómo Ejecutar

1. **Configurar Variables de Entorno**
   Verifica el archivo `.env` en la raíz. Puedes cambiar las contraseñas si lo deseas.
   ```
   SA_PASSWORD=YourStrong!Password123
   JWT_SECRET_KEY=YourSuperSecretKeyForJwtTokens123!
   ```

2. **Construir e Iniciar Contenedores**
   Ejecuta el siguiente comando en la terminal desde la carpeta `MyProyects`:
   ```bash
   docker-compose up --build
   ```
   *La primera vez tomará unos minutos mientras descarga las imágenes y compila los proyectos.*

3. **Verificar Servicios**
   - **Frontend**: Abre [http://localhost:4200](http://localhost:4200)
   - **Orders API Swagger**: [http://localhost:5080/swagger](http://localhost:5080/swagger)
   - **Auth API Swagger**: [http://localhost:5081/swagger](http://localhost:5081/swagger)

## Comandos Útiles

- **Detener todo**:
  ```bash
  docker-compose down
  ```

- **Ver logs en tiempo real**:
  ```bash
  docker-compose logs -f
  ```

- **Reconstruir un servicio específico** (ej. frontend):
  ```bash
  docker-compose up -d --build frontend
  ```

## Notas Importantes

- **Datos Persistentes**: La base de datos guarda sus datos en un volumen de Docker (`sqlserver_data`), por lo que no perderás información al reiniciar los contenedores.
- **Conexión SQL**: Si necesitas conectar SSMS (SQL Server Management Studio) localmente, usa:
  - Servidor: `localhost,1433`
  - Usuario: `sa`
  - Contraseña: La definida en `.env`
