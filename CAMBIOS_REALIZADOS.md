# Adaptación del Frontend al Backend - Cambios Realizados

## 📋 Resumen de Cambios

Se ha actualizado completamente el frontend para que sea compatible con la estructura y endpoints del backend ubicado en `https://localhost:7001`.

### 🔧 Cambios en el Frontend

#### 1. **Actualización de Configuración de Axios**
- **Archivo**: `src/API/axios.config.ts`
- Cambio de URL base: `http://localhost:5000` → `https://localhost:7001`
- Agregado soporte para certificados HTTPS autofirmados en desarrollo

#### 2. **Actualización de Interfaces TypeScript**
- **Archivo**: `src/Types/index.ts`
- Cambios en nomenclatura de propiedades para coincidir con camelCase del backend:
  - `id_usuarios` → `id_Usuarios`
  - `nombre_completo` → `Nombre_Completo`
  - `email` → `Email`
  - `rol` → `Rol`
  - `fecha_creacion` → `Fecha_Creacion`
  - `id_servicios` → `Id_Servicios`
  - `duracion_minutos` → `Duracion_Minutos`
  - Y todas las demás propiedades actualizadas

#### 3. **Actualización de Componentes**
- **Navbar.tsx**: Actualizado para usar `Nombre_Completo` en lugar de `nombre_completo`
- **Register.tsx**: Actualizado para enviar datos con propiedades capitalizadas

#### 4. **Actualización del AuthContext**
- **Archivo**: `src/Context/AuthContext.tsx`
- Cambio en verificación de rol: `user?.rol` → `user?.Rol`

#### 5. **Instalación de Dependencias Faltantes**
- Instalado `axios` (necesario para las llamadas API)
- Instalado `react-router-dom` (para enrutamiento)

#### 6. **Archivo de Configuración de Entorno**
- Creado `.env` con:
  ```
  REACT_APP_API_URL=https://localhost:7001
  ```

### 🔧 Cambios en el Backend

#### 1. **Nuevo Controlador de Autenticación**
- **Archivo**: `Controllers/AuthController.cs`
- Implementado:
  - POST `/api/auth/register` - Registro de usuarios
  - POST `/api/auth/login` - Login con JWT
  - Hashing seguro de contraseñas con SHA256
  - Generación de tokens JWT

#### 2. **Nuevo Controlador de Empleados**
- **Archivo**: `Controllers/EmpleadosController.cs`
- Implementado CRUD completo:
  - GET `/api/empleados` - Obtener todos
  - GET `/api/empleados/{id}` - Obtener por ID
  - POST `/api/empleados` - Crear
  - PUT `/api/empleados/{id}` - Actualizar
  - DELETE `/api/empleados/{id}` - Eliminar

#### 3. **Nuevo Controlador de Servicios**
- **Archivo**: `Controllers/ServiciosController.cs`
- Implementado CRUD completo:
  - GET `/api/servicios` - Obtener todos
  - GET `/api/servicios/{id}` - Obtener por ID
  - POST `/api/servicios` - Crear
  - PUT `/api/servicios/{id}` - Actualizar
  - DELETE `/api/servicios/{id}` - Eliminar

#### 4. **Actualización del MappingProfile**
- **Archivo**: `Profiles/MappingProfile.cs`
- Agregados mapeos bidireccionales para Empleado, Servicio
- Mejorados mapeos de Turno con todos los datos necesarios

#### 5. **Configuración de CORS**
- **Archivo**: `Program.cs`
- Agregado middleware de CORS para permitir solicitudes desde el frontend

## 🚀 Cómo Ejecutar

### Backend (.NET 8)
```bash
cd Back-end
dotnet run
# O desde Visual Studio: F5
```
El backend estará disponible en: `https://localhost:7001`

### Frontend (React)
```bash
cd reservas-frontend
npm install
npm start
# O si está instalado: npm start
```
El frontend estará disponible en: `http://localhost:3000`

## ✅ Endpoints Disponibles

### Autenticación
- `POST /api/auth/register` - Registrar nuevo usuario
- `POST /api/auth/login` - Iniciar sesión

### Empleados
- `GET /api/empleados` - Obtener todos
- `GET /api/empleados/{id}` - Obtener por ID
- `POST /api/empleados` - Crear
- `PUT /api/empleados/{id}` - Actualizar
- `DELETE /api/empleados/{id}` - Eliminar

### Servicios
- `GET /api/servicios` - Obtener todos
- `GET /api/servicios/{id}` - Obtener por ID
- `POST /api/servicios` - Crear
- `PUT /api/servicios/{id}` - Actualizar
- `DELETE /api/servicios/{id}` - Eliminar

### Turnos
- `POST /api/turnos/reservar` - Reservar turno
- `DELETE /api/turnos/cancelar/{id}` - Cancelar turno
- `GET /api/turnos/empleado/{id}` - Obtener por empleado
- `GET /api/turnos/fecha/{fecha}` - Obtener por fecha

## 📝 Notas Importantes

1. **Certificado HTTPS**: El backend usa certificado autofirmado. El frontend está configurado para aceptarlo en desarrollo.
2. **Base de datos**: Asegúrate de que SQL Server esté corriendo en `localhost:1433` con las credenciales configuradas en `appsettings.json`
3. **JWT**: Se usa JWT para autenticación. El token se almacena en `localStorage` y se envía en cada solicitud
4. **CORS**: Está habilitado para todas las orígenes (reconfigura según sea necesario en producción)

## 🔐 Seguridad

- Las contraseñas se hashean con SHA256
- Los tokens JWT expiran en 24 horas
- Se valida la autenticación en rutas protegidas
- Se implementó validación en DTOs del backend
