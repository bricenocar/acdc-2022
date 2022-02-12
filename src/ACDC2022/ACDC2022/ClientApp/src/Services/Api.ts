import { ApiResponse, create } from 'apisauce'
import { UserAuth } from '../Models/Auth/UserAuth';
import { Telemetry } from '../Models/Telemetry';
import { User } from '../Models/User';

//const baseURL = process.env.BACKENDURL;
const baseURL = 'https://app-acdc2022.azurewebsites.net';
const authPath = 'api/auth';
const telemetryPath = 'api/telemetry';

// define the api
const api = create({
    baseURL,
    timeout: 60000,
    withCredentials: true,
});

/* AUTHENTICATION */
export const signIn = (data: UserAuth): Promise<ApiResponse<User, User>> => api.post(`${authPath}/signin`, data);
export const signOut = (): Promise<ApiResponse<boolean, boolean>> => api.post(`${authPath}/signout`);
export const signCheck = (): Promise<ApiResponse<User, User>> => api.get(`${authPath}/signcheck`);

/* TELEMETRY */
export const telemetriesWithGeoLocation = (): Promise<ApiResponse<Telemetry[], Telemetry[]>> => api.get(`${telemetryPath}/gettelemetrieswithgeolocation`);

export default api;