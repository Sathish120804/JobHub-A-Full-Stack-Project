import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:5032/api"
});

api.interceptors.request.use(
    (config) => {

        const token = localStorage.getItem("token");

        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }

        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

export default api;
//we have one central Axios instance.
//useEffect is a React Hook used to perform side effects in functional components, 
// such as API calls, subscriptions, timers, and interacting with external systems.