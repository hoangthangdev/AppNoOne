import axios from 'axios';
import { overlay, token } from '/ManageApp/components/constants';

// Khởi tạo một instance Axios
const axiosInstance = axios.create({
    baseURL: 'https://localhost:5001', // Thiết lập baseURL
    headers: {
        'Authorization': 'Bearer ' + token() // Thiết lập headers
    }
});

// Đăng ký interceptor trước khi gửi yêu cầu
axiosInstance.interceptors.request.use((config) => {
    overlay.value = true;
    return config;
}, (error) => {
    overlay.value = false;
    return Promise.reject(error);
});

// Đăng ký interceptor sau khi nhận phản hồi
axiosInstance.interceptors.response.use((response) => {
    overlay.value = false;
    return response;
}, (error) => {
    overlay.value = false;
    return Promise.reject(error);
});

export default axiosInstance