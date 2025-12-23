import axiosInstance from './axios.config';
import { ReservarTurnoRequest } from '../Types';

export const turnosApi = {
    reservar: async (data: ReservarTurnoRequest) => {
        const response = await axiosInstance.post('/turnos/reservar', data);
        return response.data;
    },

    cancelar: async (id: number) => {
        const response = await axiosInstance.delete(`/turnos/cancelar/${id}`);
        return response.data;
    },

    obtenerPorEmpleado: async (empleadoId: number) => {
        const response = await axiosInstance.get(`/turnos/empleado/${empleadoId}`);
        return response.data;
    },

    obtenerPorFecha: async (fecha: string) => {
        const response = await axiosInstance.get(`/turnos/fecha/${fecha}`);
        return response.data;
    },
};