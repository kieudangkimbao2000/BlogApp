import type { TagDTO, BlogDTO } from "../models/generated-interfaces";

export interface MainNeedsDTO
{
    topFiveTags?: TagDTO[];
    fiveLatestBlogs?: BlogDTO[];
    topFiveBlogs?: BlogDTO[];
}