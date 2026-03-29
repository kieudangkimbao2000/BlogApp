import ApiClient from "../api/apiclient";
import type { BlogListRespDTO } from "../models/blog-list-resp-dto";
import type { SearchBlogDTO } from "../models/search-blog-dto";
import type { SearchBlogReqDTO } from "../models/search-blog-req-dto";
import type { SearchBlogRespDTO } from "../models/search-blog-resp-dto";

class BlogService {

    async searchBlog(req: SearchBlogReqDTO) : Promise<SearchBlogRespDTO>
    {
        const resp = await ApiClient.post<SearchBlogRespDTO>('blog/search', req);

        return resp;
    }
    
    async get5Latest() : Promise<BlogListRespDTO>
    {
        const resp = ApiClient.get<BlogListRespDTO>('blog/fivelatest', null);

        return resp;
    }

    async getTop5() : Promise<BlogListRespDTO>
    {
        const resp = ApiClient.get<BlogListRespDTO>('blog/topfive', null);

        return resp;
    } 
}

export default BlogService;