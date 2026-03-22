import ApiClient from "../api/apiclient";
import type { BlogDTO } from "../models/blog-dto";
import type { BlogListRespDTO } from "../models/blog-list-resp-dto";
import type { CategoryListRespDTO } from "../models/category-list-resp-dto";
import type { SearchBlogDTO } from "../models/search-blog-dto";

class BlogService {

    async searchBlog(req: SearchBlogDTO) : Promise<SearchBlogDTO>
    {
        const resp = ApiClient.get<SearchBlogDTO>('blog/search', req);

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