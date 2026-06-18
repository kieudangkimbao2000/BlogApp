import ApiClient from "../api/apiclient";
import type { TagListRespDTO } from "../models/generated-interfaces";

class TagService{

    async getCategs() : Promise<TagListRespDTO>
    {
        const resp = await ApiClient.get<TagListRespDTO>('tags');

        return resp;
    }

    async getTopFive() : Promise<TagListRespDTO>
    {
        const resp =    await ApiClient.get<TagListRespDTO>('tags/topfive');

        return resp;
    }
}

export default TagService;