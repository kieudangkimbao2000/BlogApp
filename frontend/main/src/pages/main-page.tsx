//liraries
import { useEffect, useRef, useState } from 'react';
import { Link, Outlet, replace, useLocation, useNavigate } from 'react-router-dom';
import { Avatar } from '@mui/material';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
//modules
import useAuthen from '../hooks/useAuthen';
import MainService from '../services/main-service';
import useMessage from '../hooks/useMessage';
import BlogAppMessage from '../common/message';
import type { BlogDTO, TagDTO, SearchBlogReqDTO } from '../models/generated-interfaces';
//css
import '../assets/css/main.css';

const mainService = new MainService();

const MainPage = () => {
    const {showMessage, MessageComponent} = useMessage();
    const [topFiveCategs, setTopFiveCategs] = useState<TagDTO[]>();
    const [fiveLatestBlogs, setFiveLatestBlogs] = useState<BlogDTO[]>();
    const [topFiveBlogs, setTopFiveBlogs] = useState<BlogDTO[]>();
    const [searchTitle, setSearchTitle] = useState<string>('');
    const [isSearching, setIsSearching] = useState<boolean>(false);
    const [user] = useAuthen();
    const navigate = useNavigate();
    const searchRef = useRef<SearchBlogReqDTO>({    searchTitle: '',
                                                    tags: [],
                                                    searchFlag: 0,
                                                    curPage: 1
                                                });

    useEffect(()=> {
        handleGetNeeds();
    }, []);

    const handleGetNeeds = async ()=> {
        try
        {
            const needs = await mainService.GetNeeds();

            if(!needs) return;

            setTopFiveCategs(needs.topFiveTags);
            setFiveLatestBlogs(needs.fiveLatestBlogs);
            setTopFiveBlogs(needs.topFiveBlogs);
        }
        catch(err)
        {
            showMessage({
                type: BlogAppMessage.MSG_ERR_TYPE,
                message: err?.toString()  ??  ' '
            });
        }
    };

    const handleSearch = () => {
        navigate('');
        setIsSearching(!isSearching);
    };

    const setValueToSearchRef = (value: any, item: string) => {
        if(!searchRef.current) return;

        switch(item)
        {
            case 'new':
                searchRef.current = {searchTitle: '', searchFlag: value, tags: [], curPage: 1};
                break;
            case 'top':
                searchRef.current = {searchTitle: '', searchFlag: value, tags: [], curPage: 1};
                break;
            case 'categ':
                searchRef.current = {searchTitle: '', searchFlag: 0, tags: [value], curPage: 1};
                break;
        }
    };

    const handleOnChangeSearchTitle = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTitle(e.target.value);
        searchRef.current = {...searchRef.current, searchTitle: e.target.value, curPage: 1};
    }

    const handleSearchKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if(e.key === 'Enter')
        {
            handleSearch();
        }
    };

    return (
        <>
            <MessageComponent/>
            <div className="background">
            </div>
            <div className='menu-area container'>
                <div className='row'>
                    <div className='col'>
                        <div className='left-menu'>
                            <div className='left-link'  style={{paddingTop: '15px'}}>
                                <Link to={''} 
                                    onClick={() => {setValueToSearchRef(0, 'new'); handleSearch();}}
                                >New</Link>
                            </div>
                            <div className='left-link'>
                                <Link to={''} 
                                    onClick={() => {setValueToSearchRef(1, 'top'); handleSearch();}}
                                >Top</Link>
                            </div>
                            <div className='left-link'>
                                <Link to={'tags'}>Tags</Link>
                            </div>
                            { (topFiveCategs && topFiveCategs.length > 0) ? 
                                <>
                                    <div style={{border: '1px solid black', margin: '10px'}}></div>
                                    {topFiveCategs.map((categ) => (
                                        <div className='left-link'>
                                            <Link to={''} onClick={() => {
                                                setValueToSearchRef(categ.name, 'categ');
                                                handleSearch();}}>
                                                {categ.name}
                                            </Link>
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
                            <div className='right-title'  style={{paddingTop: '15px'}}>
                                New
                            </div>
                            <div style={{border: '1px solid black', margin: '0px 10px 10px 10px'}}></div>
                            {
                                (fiveLatestBlogs && fiveLatestBlogs.length > 0) ?
                                   fiveLatestBlogs.map((blog) => (
                                        <div className='right-link'>
                                             <Link to={'/blog/' + blog.id} title={blog.title}>{blog.title}1</Link>
                                        </div>
                                   )) : null
                            }
                            <div style={{height: '20px'}}></div>
                            <div className='right-title'>
                                Top
                            </div>
                            <div style={{border: '1px solid black', margin: '0px 10px 10px 10px'}}></div>
                            {
                                (topFiveBlogs && topFiveBlogs.length > 0) ?
                                   topFiveBlogs.map((blog) => (
                                        <div className='right-link'>
                                            <Link to={'/blog/' + blog.id} title={blog.title}>{blog.title}1</Link>
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
                                        value={searchTitle} onChange={handleOnChangeSearchTitle}
                                        onKeyDown={handleSearchKeyDown}
                                        className='search-input'></input>
                                <button className='btn-search' 
                                        onClick={handleSearch}>
                                    <FontAwesomeIcon  icon={faSearch} />
                                </button>
                            </div>
                            <div style={{border: '1px solid black'}}></div>
                            <Outlet context={{searchRef, isSearching, setIsSearching}} />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default MainPage;