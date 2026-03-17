import { Button, Modal } from "@mui/material";
import { useRef, useState } from "react";
import '../assets/css/message.css';
import BlogAppMessage from "../common/message";

const useMessage = () => {
    interface PropModel {
        type: string;
        message?: string;
        title?: string;
    };

    const resultRef = useRef<any>(false);

    const [show, setShow] = useState<boolean>(false);
    const [prop, setProp] = useState<PropModel>({type: '', message: '', title: ''});

    const handleClose = () => {
        setShow(false);
        resultRef.current?.(false);
    };

    const handleOk = () => {
        setShow(false);
        resultRef.current?.(true);
    };

    const showMessage = (prop: PropModel) => {
        return new Promise ((resolve) => {
            setShow(true);

            switch(prop.type)
            {
                case BlogAppMessage.MSG_CONFIRM_TYPE:
                    prop.title = (prop.title && prop.title != '') ? prop.title : 
                                                        BlogAppMessage.DEFAULT_TITLE_CONFIRM;
                    break;
                case BlogAppMessage.MSG_INFO_TYPE:
                    prop.title = (prop.title && prop.title != '') ? prop.title : 
                                                        BlogAppMessage.DEFAULT_TITLE_INFO;
                    break;
                case BlogAppMessage.MSG_ERR_TYPE:
                    prop.title = (prop.title && prop.title != '') ? prop.title : 
                                                        BlogAppMessage.DEFAULT_TITLE_ERR;
                    break;
                default:
                    prop.title = (prop.title && prop.title != '') ? prop.title : '';
                    break;
            }
            
            setProp({...prop});

            resultRef.current = resolve;
        });
    };

    const MessageComponent = () => (
        <Modal open={show} className="message-box">
            <div className="message-box-body border-primary">
                <div className={"message-box-title " + 
                        (prop.type == BlogAppMessage.MSG_ERR_TYPE ? "text-danger" : "text-primary")
                    }>
                    { prop.title }
                </div>
                <div style={{border: '1px solid'}} className="border-primary"></div>
                <div className={"message-box-content "  + 
                         (prop.type == BlogAppMessage.MSG_ERR_TYPE ? "text-danger" : "text-primary")
                    }>
                    { prop.message }
                </div>
                <div style={{border: '1px solid'}} className="border-primary"></div>
                <div className="message-box-btn container">
                    <div className="row">
                        <div className="col cancel-btn">
                        { prop.type == BlogAppMessage.MSG_CONFIRM_TYPE ?
                            <button className="btn btn-danger"
                                onClick={handleClose}>
                                    Cancel
                            </button> : null
                        }
                        </div>
                        <div className="col ok-btn">
                            <button className="btn btn-success"
                                onClick={handleOk}>
                                    OK
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </Modal>
    );

    return { showMessage, MessageComponent};
};

export default useMessage;