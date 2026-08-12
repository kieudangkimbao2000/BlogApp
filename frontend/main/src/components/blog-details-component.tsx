//Libraries
import { useEffect, useState } from "react";
import { useLocation, useOutletContext } from "react-router-dom";
import DOMPurify from "dompurify";
//Modules
import type { RequestBaseDTO } from "../models/request-base-dto";
import Constants from "../common/constant";
import type { BlogDTO } from "../models/generated-interfaces";
import BlogService from "../services/blog-service";
import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
//css
import '../assets/css/blog-details-page.css';

const blogService = new BlogService();

const BlogDetailsComponent = () => {
    const { showLoading, hideLoading, LoadingComponent } = useLoading();
    const { showMessage, MessageComponent } = useMessage();
    const [blog, setBlog] = useState<BlogDTO | null>(null);
    const location = useLocation();

    useEffect(() => {
        handleLoading();
    }, []);
    
    const handleLoading = async () => {
        const blogId = location.state?.blogId;
        if (blogId === undefined) {
            showMessage({
                type: Constants.ERROR_MESSAGE_TYPE,
                message: "Failed to load blog details!"
            });
            return;
        }
        showLoading();
        try {
            const resp = await blogService.getBlogDetails(blogId);
            hideLoading();
            if (resp.statusCode === 200) {
                setBlog(resp.datas);
            } else {
                showMessage({
                    type: Constants.ERROR_MESSAGE_TYPE,
                    message: resp.datas.message || "Failed to load blog details."
                });
            }
        } catch (error) {
            hideLoading();
            showMessage({
                type: Constants.ERROR_MESSAGE_TYPE,
                message: "An error occurred while loading the blog details."
            });
        }
    }

    const loadBlogContent = (content: string | undefined) => {
        const htmlContent = DOMPurify.sanitize(content || '');

        return { __html: htmlContent };
    }

    return (
        <>
            <MessageComponent />
            <LoadingComponent />
            <div className='container'>
                <div className='blog-detail-title'>
                    <h3>{blog?.title}</h3>
                </div>
                <div className='blog-detail-content'>
                    <div dangerouslySetInnerHTML={loadBlogContent(blog?.content)} />
                </div>
            </div>
        </>
    );

};

export default BlogDetailsComponent;