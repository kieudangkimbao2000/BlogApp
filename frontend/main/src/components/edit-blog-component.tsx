import useLoading from "../hooks/useLoading";
import useMessage from "../hooks/useMessage";

const EditBlogComponent = () => {
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    
    return (
        <>
            <LoadingComponent />
            <MessageComponent />
            <div className="container">
                
            </div>
        </>
    );
};

export default EditBlogComponent;