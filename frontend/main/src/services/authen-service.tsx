import ApiClient from "../api/apiclient";
import type { LoginDTO, LoginRespDTO, ResponseBaseDTO } from "../models/generated-interfaces"

class AuthenService {
    async Login(req:LoginDTO) : Promise<LoginRespDTO> {
        const resp = await ApiClient.post<LoginRespDTO>('authen/login', req);

        return resp;
    }

   async CheckValidToken() : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.get<ResponseBaseDTO>('authen/check-valid-token');

        return resp;
    }
}

export default AuthenService;