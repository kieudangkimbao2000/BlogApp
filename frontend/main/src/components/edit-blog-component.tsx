import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCamera, faAlignLeft, faAlignCenter, faAlignRight, faImage } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useRef, useState } from "react";
import {useEditor, EditorContent} from "@tiptap/react";
import StarterKit from "@tiptap/starter-kit";
import Text from '@tiptap/extension-text';
import TextAlign from '@tiptap/extension-text-align';
import ImageResize from 'tiptap-extension-resize-image';
import Document from '@tiptap/extension-document';
import Paragraph from '@tiptap/extension-paragraph';
//modules
import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import useAuthen from "../hooks/useAuthen";
import TagService from "../services/tag-service";
import BlogService from "../services/blog-service";
import type { BlogDTO, TagDTO} from "../models/generated-interfaces";
import Constant from "../common/constant";
//css
import '../assets/css/edit-blog-component.css';
import type { RequestBaseDTO } from "../models/request-base-dto";
import type { ResponseBaseDTO } from "../models/response-base-dto";
import { BlogValidator } from "../validator/blog-validator";

const blogService = new BlogService();
const tagService = new TagService();

const EditBlogComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    const [user] = useAuthen();
    const [blogTitle, setBlogTitle] = useState<string>('');
    const [blogCoverImg, setBlogCoverImg] = useState<string>('');
    const imgRef = useRef<HTMLInputElement>(null);
    const [tags, setTags] = useState<string[]>([]);
    const [selectedTags, setSelectedTags] = useState<string[]>([]);
    const [blog, setBlog] = useState<BlogDTO>({});
    const [errMsg, setErrMsg] = useState<string>();
    const editor = useEditor({
        extensions: [
            StarterKit,
            Document,
            Paragraph,
            Text,
            ImageResize,
            TextAlign.configure({types: ['heading', 'paragraph'],})
        ],
        content: ''
    });

    useEffect(() => {
        getTags();
        loadSavedBlog();
    },[]);

    const getTags = async () => {
        try
        {
            const resp = await tagService.getCategs();

            if(!resp || !resp.datas || resp.statusCode != 200)
            {
                return;
            }

            setTags(resp.datas.map((x: TagDTO) => x.name ?? '') ?? []);
        }
        catch(err)
        {
            window.console.error(err);
        }
    }

    const loadSavedBlog = () => {
        const savedBlogString = localStorage.getItem('BlogApp-saveblog') ?? '';
        if(savedBlogString == '') return;

        const savedBlog = JSON.parse(savedBlogString);

        if(savedBlog.title != null && savedBlog.title != '')
        {
            setBlogTitle(savedBlog.title);
        }
        if(savedBlog.coverImage != null && savedBlog.coverImage != '')
        {
            setBlogCoverImg(savedBlog.coverImage);
        }
        if(savedBlog.content != null && savedBlog.content != '')
        {
            editor.commands.setContent(savedBlog.content);
        }
        if(savedBlog.tags != null && savedBlog.tags.length != 0)
        {
            setSelectedTags(savedBlog.tags);
        }
    };

    const handleSelectedImage = (e: React.ChangeEvent<HTMLInputElement>) => {
        if(e.target.files == null || e.target.files.length == 0) return;

        const file = e.target.files[0];
        const reader = new FileReader();
        reader.onload = (e) => {
            const result = e.target?.result;
            if(typeof(result) === "string")
            {
                setBlogCoverImg(result);
            }
        }
        reader.readAsDataURL(file);
    }
    
    const handleToolbarClick = (action: string) => {
        if(editor == null) return;
        
        switch(action)
        {            case "h1":
                editor.chain().focus().toggleHeading({level: 1}).run();
                break;
            case "h2":
                editor.chain().focus().toggleHeading({level: 2}).run();
                break;
            case "h3":
                editor.chain().focus().toggleHeading({level: 3}).run();
                break;
            case "bold":
                editor.chain().focus().toggleBold().run();
                break;
            case "italic":
                editor.chain().focus().toggleItalic().run();
                break;
            case "underline":
                editor.chain().focus().toggleUnderline().run();
                break;
            case "align-left":
                editor.chain().focus().setTextAlign('left').run();
                break;
            case "align-center":
                editor.chain().focus().setTextAlign('center').run();
                break;
            case "align-right":
                editor.chain().focus().setTextAlign('right').run();
                break
            case "insert-image":
                const ok = window.confirm('Do you want to insert image from URL? (Cancel to select image from device)');
                if(ok)
                {
                    const url = window.prompt('Enter image URL');
                    if(url)
                    {
                        editor.chain().focus().setImage({src: url}).run();
                    }
                }else
                {
                    const fileInput = document.createElement('input');
                    fileInput.type = 'file';
                    fileInput.accept = 'image/*';
                    fileInput.onchange = (e: any) => {
                        const file = e.target.files[0];
                        const reader = new FileReader();
                        reader.onload = (e) => {
                            const result = e.target?.result;
                            if(typeof(result) === "string")
                            {
                                editor.chain().focus().setImage({src: result}).run();
                            }
                        }
                        reader.readAsDataURL(file);
                    }
                    fileInput.click();
                }
                break;
            default:
                break;
        }
    }

    const handleDelete = async () => {
        const ok = await showMessage({
            type: Constant.CONFIRM_MESSAGE_TYPE,
            message: 'Are you sure you want to delete this blog?'
        });
        if(!ok) return;

        if(blog.id == null || blog.id == '')
        {
            await showMessage({
                type: Constant.ERROR_MESSAGE_TYPE,
                message: 'This blog is new. You cannot delete it!'
            });
            return;
        }
        
        try
        {
            showLoading();
            const resp = await blogService.deleteBlog(blog.id);
            hideLoading();

            if(resp.StatusCode != 200)
            {
                if(resp.StatusCode == 403)
                {
                    await showMessage({
                        type: Constant.ERROR_MESSAGE_TYPE,
                        message: 'Your request is denied'
                    });
                    window.location.href = '/login'
                }
                else
                {
                    await showMessage({
                        type: Constant.ERROR_MESSAGE_TYPE,
                        message: 'Failed to delete blog. Please try again.'
                    });
                }
            }
            else
            {
                setBlog({id: '', title: '', content: '', coverImage: '', tags: []});
                setBlogTitle('');
                setBlogCoverImg('');
                editor.commands.setContent('');
                setSelectedTags([]);
                localStorage.removeItem('BlogApp-saveblog');
                await showMessage({
                    type: Constant.SUCCESS_MESSAGE_TYPE,
                    message: 'Blog deleted successfully'
                });
            }
        }
        catch(err)
        {
            hideLoading();
            await showMessage({
                type: Constant.ERROR_MESSAGE_TYPE,
                message: 'Failed to delete blog. Please try again.'
            });
        }
    }

    const handleSave = () => {
        try
        {
            showLoading();
            const blogContent = editor.getHTML().toString() ?? '';
            const savedBlog = ({...blog,
                title: blogTitle,
                content: blogContent,
                coverImage: blogCoverImg ?? '',
                tags: selectedTags ?? []
            });
            localStorage.setItem('BlogApp-saveblog', JSON.stringify(savedBlog));
            showMessage({
                type: Constant.SUCCESS_MESSAGE_TYPE,
                message: 'Blog saved successfully'
            });
            hideLoading();
        }catch(err)
        {
            hideLoading();
            showMessage({
                type: Constant.ERROR_MESSAGE_TYPE,
                message: 'Failed to save blog. Please try again.'
            });
        }
    }

    const handleSubmit = async () => {
        const blogContent = editor.getHTML().toString() ?? '';

        const req = ({
            datas: {...blog,
                title: blogTitle,
                content: blogContent,
                authorId: user.Username,
                tags: [...selectedTags ?? []],
                state: Constant.SAVED_BLOG_STATE,
            } as BlogDTO,
            base64Strings: (imgRef.current?.files && imgRef.current.files.length > 0) ? [blogCoverImg] : []
        }) as RequestBaseDTO<BlogDTO>;

        try
        {
            showLoading();
            let resp : ResponseBaseDTO;
            const [isValid, errorMessage] = BlogValidator.validateAddBlogRequest(req.datas);
            if(!isValid) {
                hideLoading();
                setErrMsg(errorMessage ?? 'Invalid blog data. Please check your input.');
                window.scrollTo(0, 0);
                return;
            }

            resp = await blogService.addBlog(req);
            hideLoading();

            if(resp.StatusCode != 200)
            {
                if(resp.StatusCode == 403)
                {
                    await showMessage({
                        type: Constant.ERROR_MESSAGE_TYPE,
                        message: 'Your request is denied'
                    });
                    window.location.href = '/login'
                }
                else
                {
                    await showMessage({
                        type: Constant.ERROR_MESSAGE_TYPE,
                        message: 'Failed to save blog. Please try again!'
                    });
                }
            }
            else
            {
                setBlog({...blog, id: resp.datas?.id});
                await showMessage({
                    type: Constant.SUCCESS_MESSAGE_TYPE,
                    message: 'Blog saved successfully'
                });
            }
        }
        catch(ex)
        {
            hideLoading();
            await showMessage({
                type: Constant.ERROR_MESSAGE_TYPE,
                message: 'Failed to save blog. Please try again!'   
            });
        }
    };

    return (
        <>
            <LoadingComponent />
            <MessageComponent />
            <div className="container">
                <div className="row">
                    <div className="col">
                        <i className="err-msg-elm">{errMsg}</i>
                    </div>
                </div>
                <div className="row" style={{marginTop: "20px"}}>
                    <div className="col">
                        <textarea className="title-field" placeholder="Title..." value={blogTitle} onChange={(e) => setBlogTitle(e.target.value)}></textarea>
                    </div>
                    <div className="col">
                        <div style={{position: "relative", width: "100%", height: "100vh", maxHeight: "300px"}}>
                            <div className="select-image-affect" onClick={() => imgRef.current?.click()}>
                                <FontAwesomeIcon icon={faCamera} style={{fontSize: "100px"}}/>
                            </div>
                            <input type="file" accept="image/*" style={{display: "none"}} ref={imgRef} 
                                onChange={handleSelectedImage}/>
                            <img src={blogCoverImg || "https://thumbs.dreamstime.com/b/blog-woodn-dice-depicting-letters-stack-newspapers-leaning-dice-34801080.jpg"} 
                                className="select-image"/>
                        </div>
                    </div>
                </div>
                <div className="row" style={{marginTop: "20px"}}>
                    <div className="col">
                        <div className="row">
                            <div className="col">
                                <div className="editor-toolbar d-flex justify-content-center">
                                    <button className="editor-toolbar-h1" onClick={() => handleToolbarClick("h1")}>H1</button>
                                    <button className="editor-toolbar-h2" onClick={() => handleToolbarClick("h2")}>H2</button>
                                    <button className="editor-toolbar-h3" onClick={() => handleToolbarClick("h3")}>H3</button>
                                    <button className="editor-toolbar-bold" onClick={() => handleToolbarClick("bold")}>
                                        <b>B</b>
                                    </button>
                                    <button className="editor-toolbar-italic" onClick={() => handleToolbarClick("italic")}>
                                        <i>I</i>
                                    </button>
                                    <button className="editor-toolbar-underline" onClick={() => handleToolbarClick("underline")} style={{textDecoration: "underline"}}>
                                        U
                                    </button>
                                    <button className="editor-toolbar-align-left" onClick={() => handleToolbarClick("align-left")}>
                                        <FontAwesomeIcon icon={faAlignLeft}/>
                                    </button>
                                    <button className="editor-toolbar-align-center" onClick={() => handleToolbarClick("align-center")}>
                                        <FontAwesomeIcon icon={faAlignCenter}/>
                                    </button>
                                    <button className="editor-toolbar-align-right" onClick={() => handleToolbarClick("align-right")}>
                                        <FontAwesomeIcon icon={faAlignRight}/>
                                    </button>
                                    <button className="editor-toolbar-align-right" onClick={() => handleToolbarClick("insert-image")}>
                                        <FontAwesomeIcon icon={faImage}/>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col">
                                <EditorContent editor={editor} className="editor-content"/>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col">
                                <div className="tags-container">
                                    <ul className="tags-list">
                                        { tags.map(x => (
                                            <li 
                                                key={x} 
                                                className={`tag-item ${selectedTags.includes(x) ? "selected-tag-item" : ""}`}
                                                onClick={() => {
                                                    if (selectedTags.includes(x)) {
                                                        setSelectedTags(selectedTags.filter(tag => tag !== x));
                                                    } else {
                                                        setSelectedTags([...selectedTags, x]);
                                                    }
                                                }}
                                            >
                                                {x}
                                            </li>
                                        ))}
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col d-flex justify-content-end btn-area">
                                <button className="btn btn-danger" onClick={handleDelete}>Delete</button>
                                <button className="btn btn-primary" onClick={handleSave}>Save</button>
                                <button className="btn btn-success" onClick={handleSubmit}>Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default EditBlogComponent;