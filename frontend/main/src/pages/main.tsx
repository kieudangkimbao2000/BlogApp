import { useState } from 'react';
import '../assets/css/blog.css';
import { Link } from 'react-router-dom';
import useAuthen from '../hooks/useAuthen';
import { Avatar } from '@mui/material';
import type { CategoryDTO } from '../models/category-dto';
import type { BlogDTO } from '../models/blog-dto';
import MainListComponent from '../components/main-list-component';

const MainPage = () => {
    const [search, setSearch] = useState<string>('');
    const [categs, setCategs] = useState<CategoryDTO[]>();
    const [fiveLatestBlogs, setFiveLatestBlogs] = useState<BlogDTO[]>();
    const [topFiveBlogs, setTopFiveBlogs] = useState<BlogDTO[]>();
    const [user] = useAuthen();

    return (
        <>
            <div className="background">
            </div>
            <div className='menu-area container'>
                <div className='row'>
                    <div className='col'>
                        <div className='left-menu'>
                            <div className='left-link'  style={{paddingTop: '15px'}}>
                                <Link to={'/new'}>New</Link>
                            </div>
                            <div className='left-link'>
                                <Link to={'/tags'}>Top</Link>
                            </div>
                            <div className='left-link'>
                                <Link to={'/tags'}>Tag</Link>
                            </div>
                            { (categs && categs.length > 0) ? 
                                <>
                                    <div style={{border: '1px solid black', margin: '10px'}}></div>
                                    {categs.map((categ, index) => (
                                        <div className='left-link'>
                                            <Link to={''}>categ.name</Link>
                                        </div>
                                    ))}
                                </> : null
                            }
                            <div style={{border: '1px solid black', margin: '10px'}}></div>
                            {user?.Username ? 
                                <>
                                    <div className='left-link d-flex' style={{justifyContent: 'center'}}>
                                        <Avatar>K</Avatar>
                                    </div>
                                    <div className='left-link'>
                                        <Link to={'/myblogs'}>My Blogs</Link>
                                    </div>
                                    <div className='left-link'>
                                        <Link to={'/myblogs'}>Blog Management</Link>
                                    </div>
                                    <div className='left-link'>
                                        <Link to={'/login'} onClick={() => {
                                            localStorage.setItem('JWT', '');
                                            }}>Logout</Link>
                                    </div>
                                </> :
                                <>
                                    <div className='left-link'>
                                        <Link to={'/login'}>Login</Link>
                                    </div>
                                    <div className='left-link'>
                                        <Link to={'/register'}>Register</Link>
                                    </div>
                                </>
                            }
                        </div>
                    </div>
                    <div className='col' style={{display: 'flex', justifyContent: 'end'}}>
                        <div className='right-menu'>
                            <div className='right-link'  style={{paddingTop: '15px'}}>
                                New
                            </div>
                            <div style={{border: '1px solid black', margin: '0px 10px 10px 10px'}}></div>
                            {
                                (fiveLatestBlogs && fiveLatestBlogs.length > 0) ?
                                   fiveLatestBlogs.map((blog, index) => (
                                        <div className='right-link'>
                                            <Link to={'/new'}>{blog.title}</Link>
                                        </div>
                                   )) : null
                            }
                            <div style={{height: '20px'}}></div>
                            <div className='right-link'>
                                Top
                            </div>
                            <div style={{border: '1px solid black', margin: '0px 10px 10px 10px'}}></div>
                            {
                                (topFiveBlogs && topFiveBlogs.length > 0) ?
                                   topFiveBlogs.map((blog, index) => (
                                        <div className='right-link'>
                                            <Link to={'/new'}>{blog.title}1</Link>
                                        </div>
                                   )) : null
                            }
                        </div>
                    </div>
                </div>
            </div>
            <div className="d-flex" style={{justifyContent: 'center'}}>
                <div className="main-area">
                    <div className="blog-title">Blog</div>
                    <div className='middle-area' style={{paddingTop: '45px'}}>
                        <div style={{margin: '0px 40px 0px 40px'}}>
                            <div className='search-title'>
                                <input type='text' placeholder='Search...' 
                                        className='search-input'></input>
                            </div>
                            <div style={{border: '1px solid black'}}></div>
                            <MainListComponent />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default MainPage;