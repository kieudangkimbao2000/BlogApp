import ApiClient from "../api/apiclient";
import type { LoginReq, LoginResp, Resp } from "../common/model"
import type { ResponseBaseDTO } from "../models/generated-interfaces";

class AuthenService {
    async Login(req:LoginReq) : Promise<LoginResp> {
        const resp = await ApiClient.post<LoginResp>('authen/login', req);

        return resp;
    }

   async CheckValidToken() : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.get<ResponseBaseDTO>('authen/check-valid-token');

        return resp;
    }
}

export default AuthenService;