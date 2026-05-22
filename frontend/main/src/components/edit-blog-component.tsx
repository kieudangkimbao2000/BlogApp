import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCamera } from "@fortawesome/free-solid-svg-icons";
import { useRef, useState } from "react";
//css
import '../assets/css/edit-blog-component.css';

const EditBlogComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    const [blogTitle, setBlogTitle] = useState('');
    const [blogCoverImg, setBlogCoverImg] = useState('');
    const imgRef = useRef<HTMLInputElement>(null);

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
            </div>
        </>
    );
};

export default EditBlogComponent;