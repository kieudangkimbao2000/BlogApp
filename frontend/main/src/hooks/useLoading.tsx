import Modal from "@mui/material/Modal";
import React from "react";
import { useCallback, useState } from "react"
import loadingIcon from '../assets/images/loading.gif'

const useLoading = () => {
    const [show, setShow] = useState<boolean>(false);

    const showLoading = useCallback(() => {setShow(true)}, []);
    const hideLoading = useCallback(() => {setShow(false)}, []);

    const LoadingComponent = React.memo(() =>{
        return (
            <Modal open={show}>
                <div className='loading'>
                    <img    src={loadingIcon} />
                </div>
            </Modal>
        );
    });

    return {showLoading, hideLoading, LoadingComponent};
}

export default useLoading;