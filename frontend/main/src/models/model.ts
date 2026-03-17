import type { Blog } from "../common/model";
import type { BlogDTO } from "./blog-dto";
import type { CategoryDTO } from "./category-dto";

export interface MainNeeds
{
    top5Categs? : CategoryDTO[];
    fiveLatestBlogs? : BlogDTO[];
    top5Blogs? : BlogDTO[];
}