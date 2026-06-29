import ApiClient from "../api/apiclient";
import type { BlogListRespDTO, SearchBlogReqDTO, ResponseBaseDTO, 
                BlogPageRespDTO, BlogReqDTO, BlogRespDTO} from "../models/generated-interfaces";

class BlogService {

    async searchBlog(req: SearchBlogReqDTO) : Promise<BlogPageRespDTO>
    {
        const resp = await ApiClient.post<BlogPageRespDTO>('blog/search', req);

        return resp;
    }
    
    async getFiveLatest() : Promise<BlogListRespDTO>
    {
        const resp = await ApiClient.get<BlogListRespDTO>('blog/fivelatest');

        return resp;
    }

    async getTopFive() : Promise<BlogListRespDTO>
    {
        const resp = await ApiClient.get<BlogListRespDTO>('blog/topfive');

        return resp;
    } 

    async deleteBlog(blogId: string) : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.delete<ResponseBaseDTO>(`blog/${blogId}`);

        return resp;
    }

    async updateBlog(req: BlogReqDTO) : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.post<ResponseBaseDTO>('blog/update', req);

        return resp;
    }

    async createBlog(req: BlogReqDTO) : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.post<ResponseBaseDTO>('blog/add', req);

        return resp;
    }

    async getBeingEditedBlog(username: string) : Promise<BlogRespDTO>
    {
        const resp = await ApiClient.get<BlogRespDTO>(`blog/being-edited-blog/${username}`);

        return resp;
    }
}

export default BlogService;Response