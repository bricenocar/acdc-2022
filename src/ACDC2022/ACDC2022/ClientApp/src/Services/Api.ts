import { ApiResponse, create } from 'apisauce'
import { ParticipationDTO } from '../DTO/ParticipationDTO';
import { UserAuth } from '../Models/Auth/UserAuth';
import { User } from '../Models/User';

const baseURL = process.env.UNIFIEDAPIURL;
const authPath = 'api/auth';
const participationPath = 'api/participation';

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

/* PARTICIPATION */
export const participate = (data: ParticipationDTO): Promise<ApiResponse<User, User>> => api.post(`${participationPath}/participate`, data);
export const participationUrls = (): Promise<ApiResponse<string[], string[]>> => api.get(`${participationPath}/participationUrls`);

export default api;