import ApiClient from "../api/apiclient";
import type { AuthenEmailReqDTO, LoginReqDTO, RegisterReqDTO, VerifyEmailOTPReqDTO } from "../models/generated-interfaces";
import type { RequestBaseDTO } from "../models/request-base-dto";
import type {ResponseBaseDTO } from "../models/response-base-dto";

class AuthenService {
    async Login(req: RequestBaseDTO<LoginReqDTO>) : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.post<ResponseBaseDTO>('authen/login', req);

        return resp;
    }

    async Register(req: RequestBaseDTO<RegisterReqDTO>) : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.post<ResponseBaseDTO>('authen/register', req);
        
        return resp;
    }

   async CheckValidToken() : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.get<ResponseBaseDTO>('authen/check-valid-token');

        return resp;
    }

    async AuthenticateEmail(req: RequestBaseDTO<AuthenEmailReqDTO>) : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.post<ResponseBaseDTO>('authen/authenticate-email', req);
        
        return resp;
    }

    async VerifyEmailOTP(req: RequestBaseDTO<VerifyEmailOTPReqDTO>) : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.post<ResponseBaseDTO>('authen/verify-email-otp', req);
        
        return resp;
    }

    async ValidateRegisterInfo(req: RequestBaseDTO<RegisterReqDTO>) : Promise<ResponseBaseDTO> {
        const resp = await ApiClient.post<ResponseBaseDTO>('authen/validate-register-info', req);
        
        return resp;
    }
}

export default AuthenService;