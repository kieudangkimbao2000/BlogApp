import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCamera, faAlignLeft, faAlignCenter, faAlignRight } from "@fortawesome/free-solid-svg-icons";
import { useRef, useState } from "react";
import {useEditor, EditorContent} from "@tiptap/react";
import StarterKit from '@tiptap/starter-kit'
//css
import '../assets/css/edit-blog-component.css';

const EditBlogComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    const [blogTitle, setBlogTitle] = useState('');
    const [blogCoverImg, setBlogCoverImg] = useState('');
    const imgRef = useRef<HTMLInputElement>(null);
    const editor = useEditor({
        extensions: [StarterKit],
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
                                    <button className="editor-toolbar-h1">H1</button>
                                    <button className="editor-toolbar-h2">H2</button>
                                    <button className="editor-toolbar-h3">H3</button>
                                    <button className="editor-toolbar-bold"><b>B</b></button>
                                    <button className="editor-toolbar-italic"><i>I</i></button>
                                    <button className="editor-toolbar-underline" style={{textDecoration: "underline"}}>U</button>
                                    <button className="editor-toolbar-align-left"><FontAwesomeIcon icon={faAlignLeft}/></button>
                                    <button className="editor-toolbar-align-center"><FontAwesomeIcon icon={faAlignCenter}/></button>
                                    <button className="editor-toolbar-align-right"><FontAwesomeIcon icon={faAlignRight}/></button>
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