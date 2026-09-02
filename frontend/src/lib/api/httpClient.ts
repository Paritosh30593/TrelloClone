import axios from "axios";
import { ApiError } from "next/dist/server/api-utils";
import { InteractionRequiredAuthError } from "@azure/msal-browser";
import { msalInstance } from "@/providers/msal-provider";
import { protectedResources } from "@/authConfig";

const configuredBaseUrl = process.env.NEXT_PUBLIC_API_BASE_URL;
const API_BASE_URL = configuredBaseUrl
    ? configuredBaseUrl
    : "http://localhost:5002/api/";

const axiApi = axios.create({ baseURL: API_BASE_URL });

axiApi.interceptors.request.use(async (config) => {
    await msalInstance.initialize();
    const account = msalInstance.getActiveAccount() ?? msalInstance.getAllAccounts()[0];
    if (!account)
        return config;

    try {
        const tokenResponse = await msalInstance.acquireTokenSilent({
            scopes: protectedResources.api.scopes,
            account,
        });
        config.headers.Authorization = `Bearer ${tokenResponse.accessToken}`;
    }
    catch (error) {
        if (error instanceof InteractionRequiredAuthError) {
            await msalInstance.acquireTokenRedirect({
                scopes: protectedResources.api.scopes
            });
        }
        else {
            console.error("[httpClient] acquireTokenSilent failed:", error);
        }
    }
    return config;
});

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