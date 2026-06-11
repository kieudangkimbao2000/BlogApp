import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCamera, faAlignLeft, faAlignCenter, faAlignRight, faImage } from "@fortawesome/free-solid-svg-icons";
import { useRef, useState } from "react";
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
//css
import '../assets/css/edit-blog-component.css';

const EditBlogComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    const [blogTitle, setBlogTitle] = useState('');
    const [blogCoverImg, setBlogCoverImg] = useState('');
    const imgRef = useRef<HTMLInputElement>(null);
    const editor = useEditor({
        extensions: [
            StarterKit,
            Document,
            Paragraph,
            Text,
            ImageResize,
            TextAlign.configure({types: ['heading', 'paragraph'],})
        ],
        content: '<p>Hello World! 🌎️</p>',
    });

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

    // const handleSelectedImageFromToolbar = (url: string) => {
        
    //     editor.chain().focus().setImage({src: url}).run();
    // }
    
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

    return (
        <>
            <LoadingComponent />
            <MessageComponent />
            <div className="container">
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
                    </div>
                </div>
            </div>
        </>
    );
};

export default EditBlogComponent;