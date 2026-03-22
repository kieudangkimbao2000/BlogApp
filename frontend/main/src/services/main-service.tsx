import type { MainNeedsDTO } from "../models/common";
import type { MainNeeds } from "../models/model";
import BlogService from "./blog-service";
import CategoryService from "./categ-service";

class MainService
{
    async GetNeeds () : Promise<MainNeeds>
    {  
        const categService = new CategoryService();
        const blogService = new BlogService();

        const [resp1, resp2, resp3] = await Promise.all([categService.getTop5(), 
                                                        blogService.get5Latest(), 
                                                        blogService.getTop5()]);

        const needs : MainNeeds = {top5Categs: resp1.datas, 
                                    fiveLatestBlogs: resp2.datas,
                                    top5Blogs: resp3.datas};

        return needs;
    }
}

export default MainService;