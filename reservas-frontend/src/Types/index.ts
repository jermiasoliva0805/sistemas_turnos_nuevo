export interface Usuario {
    id_usuarios: number;
    nombre_completo: string;
    email: string;
    rol: 'admin' | 'cliente';
    fecha_creacion: string;
}

export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    nombreCompleto: string;
    email: string;
    password: string;
    rol?: 'admin' | 'cliente';
}

export interface Servicio {
    id_servicios: number;
    nombre: string;
    descripcion: string;
    duracion_minutos: number;
    precio: number;
}

export interface Empleado {
    id_empleados: number;
    nombreCompleto: string;
    email: string;
    telefono: string;
}

export interface Turno {
    id_turno: number;
    estado: string;
    fecha: string;
    horaInicio: string;
    horaFin: string;
    fechaCreacion: string;
    servicioNombre: string;
    servicioPrecio: number;
    servicioDuracion: number;
    empleadoNombre: string;
    empleadoEmail: string;
    usuarioNombre: string;
    usuarioEmail: string;
}

export interface ReservarTurnoRequest {
    usuarioId: number;
    empleadoId: number;
    servicioId: number;
    fecha: string;
    horaInicio: string;
    horaFin: string;
}