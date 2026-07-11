import { Button, Modal } from "@mui/material";
import { useRef, useState } from "react";
import '../assets/css/message.css';
import Constant from "../common/constant";

const useMessage = () => {
    interface PropModel {
        type: number;
        message?: string;
        title?: string;
    };

    const resultRef = useRef<any>(false);

    const [show, setShow] = useState<boolean>(false);
    const [prop, setProp] = useState<PropModel>({type: Constant.SUCCESS_MESSAGE_TYPE, message: '', title: ''});

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
                case Constant.CONFIRM_MESSAGE_TYPE:
                    prop.title = (prop.title && prop.title != '') ? prop.title : 'Confirm';
                    break;
                case Constant.WARNING_MESSAGE_TYPE:
                    prop.title = (prop.title && prop.title != '') ? prop.title : 'Warning';
                    break;
                case Constant.ERROR_MESSAGE_TYPE:
                    prop.title = (prop.title && prop.title != '') ? prop.title : 'Error';
                    break;
                default:
                    prop.title = (prop.title && prop.title != '') ? prop.title : 'Alert';
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
                        (prop.type == Constant.ERROR_MESSAGE_TYPE ? "text-danger" : "text-primary")
                    }>
                    { prop.title }
                </div>
                <div style={{border: '1px solid'}} className="border-primary"></div>
                <div className={"message-box-content "  + 
                         (prop.type == Constant.ERROR_MESSAGE_TYPE ? "text-danger" : "text-primary")
                    }>
                    { prop.message }
                </div>
                <div style={{border: '1px solid'}} className="border-primary"></div>
                <div className="message-box-btn container">
                    <div className="row">
                        <div className="col cancel-btn">
                        { prop.type == Constant.CONFIRM_MESSAGE_TYPE ?
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