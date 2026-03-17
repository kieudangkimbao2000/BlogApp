import type { BlogDTO } from "./blog-dto";
import type { CategoryDTO } from "./category-dto";

export interface MainNeedsDTO
{
    categs : CategoryDTO[];
    newBlogs: BlogDTO[];
    topBlog: BlogDTO[];    
}