import { useEffect, useState } from "react";
import type { BlogDTO, SearchBlogReqDTO } from "../models/generated-interfaces";
import type { PageDTO } from "../models/page-dto";
import BlogService from "../services/blog-service";
import PageComponent from "./page-component";
import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import { useOutletContext } from "react-router-dom";
import type { RequestBaseDTO } from "../models/request-base-dto";
import Constant from "../common/constant";

const blogService = new BlogService();

const MainListComponent = () => {
    const [page, setPage] = useState<PageDTO<BlogDTO>>();
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    var {searchRef, isSearching} = useOutletContext() as {searchRef: React.RefObject<SearchBlogReqDTO>, isSearching: boolean};

    const handleSearchBlogs = async (page: number) => {
        showLoading();
        try {
            const search = searchRef?.current ? {...searchRef.current, curPage: page} : 
                                                {searchTitle: '', tags: [], searchFlag: 0, curPage: page};
            const req: RequestBaseDTO<SearchBlogReqDTO> = {
                datas: search,
                base64Strings: []
            };
            const resp = await blogService.searchBlog(req);
            
            if(!resp || !resp.datas || resp.statusCode != 200)
            {
                hideLoading();
                return;
            }

            setPage(resp.datas as PageDTO<BlogDTO>);
            hideLoading();
        } catch (err) {
            hideLoading();
            await showMessage({
                type: Constant.ERROR_MESSAGE_TYPE,
                message: 'A error occured. Please try to reload page!'
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
                {page?.datas?.map((blog, index) => (
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
                <PageComponent curPage={page?.curPage ?? 1} totalPages={page?.pageSize ?? 1} handleSearch={handleSearchBlogs}/>
            </div>
        </>
    );

};

export default MainListComponent;   