import axios from "axios";
import { ApiError } from "next/dist/server/api-utils";

const API_BASE_URL = process.env.NEXT_PUBLIC_API_BASE_URL || "https://localhost:5002";

const axiApi = axios.create({ baseURL: API_BASE_URL });

axiApi.interceptors.response.use(
    response => response,
    error => {
        if (!axios.isAxiosError(error)) throw error;

        const status = error.response?.status || 500;
        const data = error.response?.data;
        const message =
            typeof data === "string" && data.trim() ? data :
                typeof data?.message === "string" ? data.message :
                    typeof data?.title === "string" ? data.title :
                        typeof data?.detail === "string" ? data.detail :
                            error.message;

        throw new ApiError(status, message);
    }
);

export default axiApi;