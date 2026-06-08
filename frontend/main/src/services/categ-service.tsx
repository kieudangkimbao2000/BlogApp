import ApiClient from "../api/apiclient";
import type { TagListRespDTO } from "../models/generated-interfaces";

class TagService{

    async getCategs() : Promise<TagListRespDTO>
    {
        const resp = await ApiClient.get<TagListRespDTO>('tags', null);

        return resp;
    }

    async getTopFive() : Promise<TagListRespDTO>
    {
        const resp =    await ApiClient.get<TagListRespDTO>('tags/topfive', null);

        return resp;
    }
}

export default TagService;