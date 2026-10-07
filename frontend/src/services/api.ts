import axios from 'axios';

// Backend API'mizin temel adresi
const api = axios.create({
    baseURL: 'http://localhost:5151/api',
});

// Her istekten önce çalışıp, eğer varsa Token'ı başlığa (Header) ekler
api.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;