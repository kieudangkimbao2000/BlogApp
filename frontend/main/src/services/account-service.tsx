import type { RequestBaseDTO } from "../models/request-base-dto";
import type { ResponseBaseDTO } from "../models/response-base-dto";
import type { UploadAvatarReqDTO } from "../models/generated-interfaces";
import ApiClient from "../api/apiclient";

class AccountService
{
    async UploadAvatar(req: RequestBaseDTO<UploadAvatarReqDTO>): Promise<ResponseBaseDTO> {
        const response = await ApiClient.post<ResponseBaseDTO>('account/upload-avatar', req);

        return response.json();
    }
}

export default AccountService;