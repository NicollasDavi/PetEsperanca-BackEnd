import axios from "axios";

const instance = axios.create({
    baseURL: 'http://localhost:',
    timeout: 15000
});

instance.interceptors.request.use(async config => {
    return config;
}, error => {
    return Promise.reject(error)
});

export default instance