import { useEffect, useState } from "react";
import type { BlogDTO, SearchBlogReqDTO, PageDTO } from "../models/generated-interfaces";
import BlogService from "../services/blog-service";
import PageComponent from "./page-component";
import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import BlogAppMessage from "../common/message";
import { useOutletContext } from "react-router-dom";

const blogService = new BlogService();

const MainListComponent = () => {
    const [blogs, setBlogs] = useState<PageDTO<BlogDTO>>();
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    var {searchRef, isSearching} = useOutletContext() as {searchRef: React.RefObject<SearchBlogReqDTO>, isSearching: boolean};

    const handleSearchBlogs = async (page: number) => {
        showLoading();
        try {
            const search = searchRef?.current ? {...searchRef.current, currPage: page} : 
                                                {searchTitle: '', categories: [], searchFlag: 0, curPage: page};
            const resp = await blogService.searchBlog(search)
            
            setBlogs(resp.blogs);
            hideLoading();
        } catch (err) {
            hideLoading();
            await showMessage({
                type: BlogAppMessage.MSG_ERR_TYPE,
                message: 'Đã có lỗi xảy ra. Xin vui lòng thử reload lại trang.'
            });
        }
    }

    useEffect(() => {
        handleSearchBlogs(1);
    }, [isSearching]);

    return (
        <>
            <MessageComponent />
            <LoadingComponent />
            <div className='container list-area'>
                {blogs?.datas?.map((blog, index) => (
                    <div className='row item'>
                        <div className='col item-img' style={{width: '100%', height: '100%'}}>
                            <img  style={{width: '100%', height: '100%'}} src={(blog.coverPhoto && blog.coverPhoto.trim() != '') ? `data:image/png;base64,${blog.coverPhoto}`
                                : 'https://thumbs.dreamstime.com/b/blog-woodn-dice-depicting-letters-stack-newspapers-leaning-dice-34801080.jpg'} />
                        </div>
                        <div className='col-8 item-title'>
                            {blog.title}
                        </div>
                        <div className='col-2 item-date'>
                            {blog.authorName}
                            <br/> 
                            {new Date(blog.publishedAt ?? '01/01/1900').toLocaleDateString('ja-JP',{year: 'numeric', 
                                                                                    month: '2-digit', 
                                                                                    day: '2-digit',
                                                                                    hour: '2-digit',
                                                                                    minute: '2-digit',
                                                                                    second: '2-digit',
                                                                                    hour12: false})}
                        </div>
                    </div>
                ))}
                <PageComponent curPage={blogs?.currPage ?? 1} totalPages={blogs?.pageSize ?? 1} handleSearch={handleSearchBlogs}/>
            </div>
        </>
    );

};

export default MainListComponent;   