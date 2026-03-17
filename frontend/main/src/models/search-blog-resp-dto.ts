/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { RespDTO } from "./resp-dto";
import { PageDTO } from "./page-dto";
import { BlogDTO } from "./blog-dto";

export interface SearchBlogRespDTO extends RespDTO {
    blogs: PageDTO<BlogDTO>;
}
