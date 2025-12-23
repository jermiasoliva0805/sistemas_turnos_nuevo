import axiosInstance from './axios.config';

export const empleadosApi = {
    obtenerTodos: async () => {
        const response = await axiosInstance.get('/empleados');
        return response.data;
    },

    obtenerPorId: async (id: number) => {
        const response = await axiosInstance.get(`/empleados/${id}`);
        return response.data;
    },

    crear: async (empleado: any) => {
        const response = await axiosInstance.post('/empleados', empleado);
        return response.data;
    },

    actualizar: async (id: number, empleado: any) => {
        const response = await axiosInstance.put(`/empleados/${id}`, empleado);
        return response.data;
    },

    eliminar: async (id: number) => {
        const response = await axiosInstance.delete(`/empleados/${id}`);
        return response.data;
    },
};