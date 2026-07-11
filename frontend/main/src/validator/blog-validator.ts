import type { BlogDTO } from "../models/generated-interfaces";

export class BlogValidator
{
    private static ERROR_MESSAGES = {
        BLOG_REQUIRED: "Blog is null or undefined",
        TITLE_REQUIRED: "Title is required",
        CONTENT_REQUIRED: "Content is required",
        TAGS_REQUIRED: "At least one tag is required",
        AUTHOR_ID_REQUIRED: "Author ID is required",
        INVALID_AUTHOR_ID: "Invalid author ID",
        TITLE_MAX_LENGTH_EXCEEDED: "Title exceeds maximum length of 255 characters",
        CONTENT_MAX_LENGTH_EXCEEDED: "Content exceeds maximum length of 5000 characters"
    };

    public static validateAddBlogRequest = (blog: BlogDTO): [boolean, string?] => {
        let message: string = "";
        if (!blog) {
            message = this.ERROR_MESSAGES.BLOG_REQUIRED;
            return [false, message];
        }
        
        // Validate required fields
        if (!blog.title || blog.title.trim() === "") {
            message = this.ERROR_MESSAGES.TITLE_REQUIRED;
            return [false, message];
        }
        if (!blog.content || blog.content.trim() === "" || blog.content.trim() === "<p></p>") {
            message = this.ERROR_MESSAGES.CONTENT_REQUIRED;
            return [false, message];
        }
        if (!blog.tags || blog.tags.length === 0) {
            message = this.ERROR_MESSAGES.TAGS_REQUIRED;
            return [false, message];
        }
        if (!blog.authorId) {
            message = this.ERROR_MESSAGES.AUTHOR_ID_REQUIRED;
            return [false, message];
        }

        //validate max length of title and content
        if (blog.title.length > 255) {
            message = this.ERROR_MESSAGES.TITLE_MAX_LENGTH_EXCEEDED;
            return [false, message];
        }
        if (blog.content.length > 5000) {
            message = this.ERROR_MESSAGES.CONTENT_MAX_LENGTH_EXCEEDED;
            return [false, message];
        }

        return [true];
    }
};