import type { MainNeedsDTO } from "../models/main-needs-dto";
import BlogService from "./blog-service";
import TagService from "./categ-service";
import CategoryService from "./categ-service";

class MainService
{
    async GetNeeds () : Promise<MainNeedsDTO>
    {  
        const tagService = new TagService();
        const blogService = new BlogService();

        const [resp1, resp2, resp3] = await Promise.all([tagService.getTopFive(), 
                                                        blogService.getFiveLatest(), 
                                                        blogService.getTopFive()]);

        const needs : MainNeedsDTO = {topFiveTags: resp1.datas, 
                                    fiveLatestBlogs: resp2.datas,
                                    topFiveBlogs: resp3.datas};

        return needs;
    }
}

export default MainService;