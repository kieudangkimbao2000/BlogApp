import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCamera, faAlignLeft, faAlignCenter, faAlignRight, faImage } from "@fortawesome/free-solid-svg-icons";
import { useContext, useEffect, useRef, useState } from "react";
import {useEditor, EditorContent, useEditorState} from "@tiptap/react";
import StarterKit from "@tiptap/starter-kit";
import Text from '@tiptap/extension-text';
import TextAlign from '@tiptap/extension-text-align';
import ImageResize from 'tiptap-extension-resize-image';
import Document from '@tiptap/extension-document';
import Paragraph from '@tiptap/extension-paragraph';
//modules
import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import TagService from "../services/tag-service";
import BlogService from "../services/blog-service";
import type { BlogDTO, TagDTO} from "../models/generated-interfaces";
import Constant from "../common/constant";
import { AuthContext } from "../App";
//css
import '../assets/css/edit-blog-component.css';
import type { RequestBaseDTO } from "../models/request-base-dto";
import type { ResponseBaseDTO } from "../models/response-base-dto";
import { BlogValidator } from "../validator/blog-validator";


const blogService = new BlogService();
const tagService = new TagService();

const ToolBarButton = ({editor, mark, children}: {editor: any, mark: string, children: React.ReactNode}) => {
    
    const isActive = useEditorState({
        editor,
        selector: ({editor}) => {
            switch(mark)
            {
                case "h1":
                    return editor.isActive('heading', { level: 1 });
                case "h2":
                    return editor.isActive('heading', { level: 2 });
                case "h3":
                    return editor.isActive('heading', { level: 3 });
                case "bold":
                    return editor.isActive('bold');
                case "italic":
                    return editor.isActive('italic');
                case "underline":
                    return editor.isActive('underline');
                case "align-left":
                    return editor.isActive({ textAlign: 'left' });
                case "align-center":
                    return editor.isActive({ textAlign: 'center' });
                case "align-right":
                    return editor.isActive({ textAlign: 'right' });
                case "insert-image":
                    return editor.isActive('image');
                default:
                    return false;
            }
        }
    });

    const handleToolbarClick = () => {
        if(editor == null) return;
        
        switch(mark)
        {            
            case "h1":
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
                break;
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

    
    return (
        <button
            className={'editor-toolbar-btn' + (isActive ? ' active' : '')}
            onClick={handleToolbarClick}
        >
            {children}
        </button>
    );
}

const EditBlogComponent = () => {
    const auth = useContext(AuthContext);
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
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
            ImageResize,
            TextAlign.configure({types: ['heading', 'paragraph'],})
        ],
        content: ''
    });

    useEffect(() => {
        // handleCheckUserAuth();
        getTags();
        loadSavedBlog();
    }, [auth?.user]);

    const handleCheckUserAuth = async () => {
        if(auth?.user == null) {
            window.location.href = '/login';
            return;
        };

        const username = auth?.user?.Username ?? auth?.user?.username;
        if(username == null || username == '') {
            window.location.href = '/login';
            return;
        }
    }

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
                authorId: auth?.user?.Username ?? auth?.user?.username ?? '',
                tags: [...selectedTags ?? []],
                state: Constant.UNPUBLISHED_BLOG_STATE,
            } as BlogDTO,
            base64Strings: (imgRef.current?.files && imgRef.current.files.length > 0) ? [blogCoverImg] : []
        }) as RequestBaseDTO<BlogDTO>;

        try
        {
            showLoading();
            let resp : ResponseBaseDTO;
            const [isValid, errorMessage] = BlogValidator.validateAddBlogRequest(req.datas, editor.getText().length ?? 0);
            if(!isValid) {
                hideLoading();
                setErrMsg(errorMessage ?? 'Invalid blog data. Please check your input.');
                window.scrollTo(0, 0);
                return;
            }

            resp = await blogService.addBlog(req);
            hideLoading();

            if(resp.statusCode != 200)
            {
                if(resp.statusCode == 403)
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
                setBlogTitle('');
                setBlogCoverImg('');
                editor.commands.setContent('');
                setSelectedTags([]);
                setErrMsg('');
                localStorage.removeItem('BlogApp-saveblog');
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
                                    <ToolBarButton editor={editor} mark="h1">H1</ToolBarButton>
                                    <ToolBarButton editor={editor} mark="h2">H2</ToolBarButton>
                                    <ToolBarButton editor={editor} mark="h3">H3</ToolBarButton>
                                    <ToolBarButton editor={editor} mark="bold"><b>B</b></ToolBarButton>
                                    <ToolBarButton editor={editor} mark="italic"><i>I</i></ToolBarButton>
                                    <ToolBarButton editor={editor} mark="underline"><u>U</u></ToolBarButton>
                                    <ToolBarButton editor={editor} mark="align-left"><FontAwesomeIcon icon={faAlignLeft}/></ToolBarButton>
                                    <ToolBarButton editor={editor} mark="align-center"><FontAwesomeIcon icon={faAlignCenter}/></ToolBarButton>
                                    <ToolBarButton editor={editor} mark="align-right"><FontAwesomeIcon icon={faAlignRight}/></ToolBarButton>
                                    <ToolBarButton editor={editor} mark="insert-image"><FontAwesomeIcon icon={faImage}/></ToolBarButton>
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