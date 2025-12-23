export interface Usuario {
    id_Usuarios: number;
    Nombre_Completo: string;
    Email: string;
    Rol: 'admin' | 'cliente';
    Fecha_Creacion: string;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    NombreCompleto: string;
    Email: string;
    Password: string;
    Rol?: 'admin' | 'cliente';
}

export interface Servicio {
    Id_Servicios: number;
    Nombre: string;
    Descripcion: string;
    Duracion_Minutos: number;
    Precio: number;
}

export interface Empleado {
    Id_Empleados: number;
    NombreCompleto: string;
    Email: string;
    Telefono: string;
}

export interface Turno {
    Id_Turno: number;
    Estado: string;
    Fecha: string;
    HoraInicio: string;
    HoraFin: string;
    FechaCreacion: string;
    ServicioNombre: string;
    ServicioPrecio: number;
    ServicioDuracion: number;
    EmpleadoNombre: string;
    EmpleadoEmail: string;
    UsuarioNombre: string;
    UsuarioEmail: string;
}

export interface ReservarTurnoRequest {
    UsuarioId: number;
    EmpleadoId: number;
    ServicioId: number;
    Fecha: string;
    HoraInicio: string;
    HoraFin: string;
}