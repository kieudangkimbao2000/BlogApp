import { useEffect, useState } from "react";
import type { BlogDTO } from "../models/blog-dto";
import BlogService from "../services/blog-service";
import type { PageDTO } from "../models/page-dto";
import PageComponent from "./page-component";

const blogService = new BlogService();
const currPage = 1 ;
const pages = Array(50).fill(null);
const pageSize = 23;

const MainListComponent = () => {
    
    const [blogs, setBlogs] = useState<PageDTO<BlogDTO>>();

    const handleSearchBlogs = () => {
        
    }

    return (
        <div className='container list-area'>
            {[...Array(10)].map((_, index) => (
                <div className='row item'>
                    <div className='col item-img' style={{width: '100%', height: '100%'}}>
                        <img  style={{width: '100%', height: '100%'}} src='https://thumbs.dreamstime.com/b/blog-woodn-dice-depicting-letters-stack-newspapers-leaning-dice-34801080.jpg' />
                    </div>
                    <div className='col-8 item-title'>
                        Blog Title
                    </div>
                    <div className='col-2 item-date'>
                        username
                        <br/> 
                        25/01/02 15:33
                    </div>
                </div>
            ))}
            <PageComponent curPage={blogs?.currPage ?? 1} totalPages={blogs?.pageSize ?? 1} />
        </div>
    );

};

export default MainListComponent;   