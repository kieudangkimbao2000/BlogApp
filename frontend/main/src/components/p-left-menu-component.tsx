//libraries
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCamera } from "@fortawesome/free-solid-svg-icons";
import { useRef, type ChangeEvent } from "react";
//modules
import Constant from "../common/constant.tsx";
import AccountService from "../services/account-service";
import type { RequestBaseDTO } from "../models/request-base-dto";
import type { UploadAvatarReqDTO } from "../models/generated-interfaces";
import useMessage from "../hooks/useMessage";
import useLoading from "../hooks/useLoading";

const PLeftMenuComponent = () => {
    //hooks
    const imgRef = useRef<HTMLImageElement>(null);
    const inputRef = useRef<HTMLInputElement>(null);
    const {showMessage, MessageComponent} = useMessage();
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    //variables
    const service = new AccountService();

    const handleAvatarClick = () => {
        if (imgRef.current) {
            inputRef.current?.click();
        }
    }

    const handleSelectAvatar = async (event: ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (!file) return;

        const reader = new FileReader();
        reader.onload = async (e: ProgressEvent<FileReader>) => {
            const result = e.target?.result;
            
            const req: RequestBaseDTO<UploadAvatarReqDTO> = {
                datas: {
                    username: "bao-kdk",
                },
                base64Strings: [result as string],
            };

            showLoading();
            try {
                const resp = await service.UploadAvatar(req);
                hideLoading();
                if (!resp || resp.statusCode !== 200) {
                    await showMessage({
                        type: Constant.ERROR_MESSAGE_TYPE,
                        message: 'Failed to upload avatar. Please try again.'
                    });
                    return;
                }

                if (imgRef.current) {
                    imgRef.current.src = result as string;
                }
            } catch (error) {
                hideLoading();
                await showMessage({
                    type: Constant.ERROR_MESSAGE_TYPE,
                    message: 'An error occurred while uploading the avatar. Please try again.'
                });
            }
        }
        reader.readAsDataURL(file);
    }


    return (
        <>
            <MessageComponent />
            <LoadingComponent />
            <div className='p-left-menu'>
                <div className='p-left-menu-avatar'>
                        <img ref={imgRef} src='https://cdn-icons-png.flaticon.com/512/149/149071.png' alt='avatar'/>
                        <div className='p-left-menu-choose-avatar' onClick={handleAvatarClick}>
                            <FontAwesomeIcon icon={faCamera} style={{fontSize: "100px"}}/>
                        </div>
                        <input type='file' accept='image/*' ref={inputRef} style={{display: 'none'}} onChange={handleSelectAvatar}/>
                </div>
                <div className='p-left-menu-line'/>
                <div className='p-left-menu-item'>
                    Account
                </div>
                <div className='p-left-menu-item'>
                    Private
                </div>
                <div className='p-left-menu-line'/>
                <div className='p-left-menu-item'>
                    Blog Management
                </div>
                <div className='p-left-menu-item'>
                    Statistics
                </div>
                <div className='p-left-menu-footer'>
                    <div className='p-left-menu-item'>
                        Home
                    </div>
                    <div className='p-left-menu-item'>
                        Logout
                    </div>
                </div>
            </div>
        </>
    );
}

export default PLeftMenuComponent;