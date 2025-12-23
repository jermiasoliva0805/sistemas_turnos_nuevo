import axiosInstance from './axios.config';
import { LoginRequest, RegisterRequest } from '../Types';

export const authApi = {
    login: async (credentials: LoginRequest) => {
        const response = await axiosInstance.post('/auth/login', credentials);
        return response.data;
    },

    register: async (data: RegisterRequest) => {
        const response = await axiosInstance.post('/auth/register', data);
        return response.data;
    },

    logout: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
    },
};