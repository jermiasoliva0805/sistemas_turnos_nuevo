import axiosInstance from './axios.config';

export const serviciosApi = {
    obtenerTodos: async () => {
        const response = await axiosInstance.get('/servicios');
        return response.data;
    },

    obtenerPorId: async (id: number) => {
        const response = await axiosInstance.get(`/servicios/${id}`);
        return response.data;
    },

    crear: async (data: any) => {
        const response = await axiosInstance.post('/servicios', data);
        return response.data;
    },

    actualizar: async (id: number, data: any) => {
        const response = await axiosInstance.put(`/servicios/${id}`, data);
        return response.data;
    },

    eliminar: async (id: number) => {
        const response = await axiosInstance.delete(`/servicios/${id}`);
        return response.data;
    },
};