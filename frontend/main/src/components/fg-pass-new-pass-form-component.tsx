//libraries
import { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
//modules
import type { AuthenEmailReqDTO, ChangePasswordReqDTO } from '../models/generated-interfaces';
import useMessage from '../hooks/useMessage';
import useLoading from '../hooks/useLoading';
import AuthenService from '../services/authen-service';
import type { RequestBaseDTO } from '../models/request-base-dto';
import Constants from '../common/constant';
import { AuthenValidator } from '../validator/authen-validator';

//variables
var firstRender = true;

const FgPassNewPassFormComponent = () => {
    //hooks
    const [message, setMessage] = useState<string>('');
    const navigate = useNavigate();
    const {showMessage, MessageComponent} = useMessage();
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const location = useLocation();
    const [chgPassReq, setChgPassReq] = useState<RequestBaseDTO<ChangePasswordReqDTO>>({
                                                    datas: {
                                                        email: location.state?.auth?.email || '', 
                                                        newPassword: '', 
                                                        rePassword: ''
                                                    }});
    //variables
    var service = new AuthenService();

    const handleChangePassword = async () => {
        const [isValid, errorMessage] = AuthenValidator.validateChangePasswordRequest(chgPassReq.datas);
        if(!isValid){
            setMessage(errorMessage || 'Invalid input. Please check your entries.');
            return;
        }
   
        showLoading();
        try
        {
            const resp = await service.ChangePassword(chgPassReq);
            hideLoading();
            if(resp && resp.statusCode === 200){
                navigate('/login');
            } else {
                await showMessage({
                    type: Constants.ERROR_MESSAGE_TYPE, 
                    message: resp.message || 'Failed to send email. Please try again later.'});
            }
        } catch (error) {
            await showMessage({
                    type: Constants.ERROR_MESSAGE_TYPE, 
                    message: 'Failed to send email. Please try again later.'});
        }
    }

    return (
        <>
            <LoadingComponent/>
            <MessageComponent/>
            <div className='forgot-password' style={{zIndex: '1'}}>
                <div className='forgot-password-title'>
                    Forgot Password
                </div>
                <div className='container forgot-password-form'>
                    <div className='row' style={{padding: '60px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='password' 
                                placeholder='New Password' 
                                className='email-field' 
                                value={chgPassReq.datas.newPassword} 
                                onChange={(e) => {
                                    var chgPassReqInfo = {...chgPassReq, datas: {...chgPassReq.datas, newPassword: e.target.value}}; 
                                    setChgPassReq({...chgPassReqInfo});
                                }}/>
                        </div>
                    </div>
                    <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='password' 
                                placeholder='Repeat Password' 
                                className='email-field' 
                                value={chgPassReq.datas.rePassword} 
                                onChange={(e) => {
                                    var chgPassReqInfo = {...chgPassReq, datas: {...chgPassReq.datas, rePassword: e.target.value}}; 
                                    setChgPassReq({...chgPassReqInfo});
                                }}/>
                        </div>
                    </div>
                    {(message || message !== '') ? 
                        <div className='row' style={{padding: '0px 30px 0px 30px'}}>
                            <div className='col'>
                                <label style={{color: 'red',fontFamily: 'Dejavu Serif' }}>
                                    <i>{message}</i>
                                </label>
                            </div>
                        </div> : null
                    }
                    <div className='row' style={{padding: '25px 30px 10px 30px'}}>
                        <div className='col'>
                            <button className='forgot-password-btn' onClick={() => {window.history.back();}}>Cancel</button>
                        </div>
                        <div className='col' style={{textAlign: 'right'}}>
                            <button className='forgot-password-btn' onClick={handleChangePassword}>Send</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default FgPassNewPassFormComponent;