import ApiClient from "../api/apiclient";
import type { ResponseBaseDTO } from "../models/response-base-dto";

class TagService{

    async getCategs() : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.get<ResponseBaseDTO>('tags');

        return resp;
    }

    async getTopFive() : Promise<ResponseBaseDTO>
    {
        const resp =    await ApiClient.get<ResponseBaseDTO>('tags/topfive');

        return resp;
    }
}

export default TagService;