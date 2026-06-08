import ApiClient from "../api/apiclient";
import type { BlogListRespDTO, SearchBlogReqDTO,  SearchBlogRespDTO} from "../models/generated-interfaces";

class BlogService {

    async searchBlog(req: SearchBlogReqDTO) : Promise<SearchBlogRespDTO>
    {
        const resp = await ApiClient.post<SearchBlogRespDTO>('blog/search', req);

        return resp;
    }
    
    async getFiveLatest() : Promise<BlogListRespDTO>
    {
        const resp = ApiClient.get<BlogListRespDTO>('blog/fivelatest', null);

        return resp;
    }

    async getTopFive() : Promise<BlogListRespDTO>
    {
        const resp = ApiClient.get<BlogListRespDTO>('blog/topfive', null);

        return resp;
    } 
}

export default BlogService;