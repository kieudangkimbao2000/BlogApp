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
import type { BlogReqDTO } from "../models/generated-interfaces";
//css
import '../assets/css/edit-blog-component.css';

const blogService = new BlogService();
const tagService = new TagService();

const EditBlogComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    const [user] = useAuthen();
    const [blogTitle, setBlogTitle] = useState('');
    const [blogCoverImg, setBlogCoverImg] = useState('');
    const imgRef = useRef<HTMLInputElement>(null);
    const [tags, setTags] = useState<string[]>([]);
    const [selectedTags, setSelectedTags] = useState<string[]>([]);
    const [blog, setBlog] = useState<BlogReqDTO>({});
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
    },[]);

    const getTags = async () => {
        try
        {
            const resp = await tagService.getCategs();
            setTags(resp.datas?.map(x => x.name ?? '') ?? []);
        }
        catch(err)
        {
            await showMessage({
                type: 'error',
                message: 'Lỗi hệ thống. Hãy thử load lại trang.'
            });
        }
    }

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

    const handleDeleteBlog = async () => {
        const ok = window.confirm('Are you sure you want to delete this blog?');
        if(!ok) return;

        if(blog.blog == null || blog.blog.id == null)
        {
            await showMessage({
                type: 'error',
                message: 'Blog ID is not exist. Cannot delete blog.'
            });
            return;
        }
        
        try
        {
            showLoading();
            const resp = await blogService.deleteBlog(blog.blog.id);
            hideLoading();

            if(resp.StatusCode != 200)
            {
                if(resp.statusCode == 403)
                {
                    await showMessage({
                        type: 'error',
                        message: 'Your request is denied'
                    });
                    window.location.href = '/login'
                }
                else
                {
                    await showMessage({
                        type: 'error',
                        message: 'Failed to delete blog. Please try again.'
                    });
                }
            }
            else
            {
                await showMessage({
                    type: 'success',
                    message: 'Blog deleted successfully'
                });
            }
        }
        catch(err)
        {
            hideLoading();
            await showMessage({
                type: 'error',
                message: 'Failed to delete blog. Please try again.'
            });
        }
    }

    const handleSaveBlog = async () => {
        
    };

    const checkRequirement = () : boolean => {
        const content = editor.getHTML() ?? '';
        if(blogTitle == null || blogTitle == '')
        {
            return false;
        }
        if(content == '')
        {
            return false;
        }

        return true;
    };

    return (
        <>
            <LoadingComponent />
            <MessageComponent />
            <div className="container">
                <div className="row">
                    <div className="col err-msg-elm">
                        {errMsg}
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
                                <button className="btn btn-danger" onClick={handleDeleteBlog}>Delete</button>
                                <button className="btn btn-primary">Save</button>
                                <button className="btn btn-success">Submit</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default EditBlogComponent;