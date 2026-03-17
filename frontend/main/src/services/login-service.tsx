import ApiClient from "../api/apiclient";
import type { LoginReq, LoginResp, Resp } from "../common/model"

class LoginService{
    async Login(req:LoginReq) : Promise<LoginResp> {
        const resp = await ApiClient.post<LoginResp>('authen/login', req);

        return resp;
    }
}

export default LoginService;