import ApiClient from "../api/apiclient";
import type { BlogDTO, SearchBlogReqDTO } from "../models/generated-interfaces";
import type { RequestBaseDTO } from "../models/request-base-dto";
import type { ResponseBaseDTO } from "../models/response-base-dto";

class BlogService {

    async searchBlog(req: RequestBaseDTO<SearchBlogReqDTO>) : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.post<ResponseBaseDTO>('blog/search', req);

        return resp;
    }
    
    async getFiveLatest() : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.get<ResponseBaseDTO>('blog/fivelatest');

        return resp;
    }

    async getTopFive() : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.get<ResponseBaseDTO>('blog/topfive');

        return resp;
    } 

    async deleteBlog(blogId: string) : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.delete<ResponseBaseDTO>(`blog/${blogId}`);

        return resp;
    }

    async updateBlog(req: RequestBaseDTO<BlogDTO>) : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.post<ResponseBaseDTO>('blog/update', req);

        return resp;
    }

    async addBlog(req: RequestBaseDTO<BlogDTO>) : Promise<ResponseBaseDTO>
    {
        const resp = await ApiClient.post<ResponseBaseDTO>('blog/add', req);

        return resp;
    }

    async getBeingEditedBlog(username: string) : Promise<BlogDTO>
    {
        const resp = await ApiClient.get<BlogDTO>(`blog/being-edited-blog/${username}`);

        return resp;
    }
}

export default BlogService;