//libraries
import { useEffect, useState } from 'react';
import { useNavigate, useOutletContext } from 'react-router-dom';
import {faCircleCheck, faSpinner} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
//modules
import type { AuthenEmailReqDTO, RegisterReqDTO, VerifyEmailOTPReqDTO } from '../models/generated-interfaces';
import useMessage from '../hooks/useMessage';
import useLoading from '../hooks/useLoading';
import AuthenService from '../services/authen-service';
import type { RequestBaseDTO } from '../models/request-base-dto';
import Constants from '../common/constant';

//variables
var firstRender = true;

const VerifyEmailFormComponent = () => {
    const {register, setRegister} = useOutletContext() as {register: RegisterReqDTO, 
            setRegister: React.Dispatch<React.SetStateAction<RegisterReqDTO>>};
    //hooks
    const [message, setMessage] = useState<string>('');
    const [vrfReq, setVrfReq] = useState<VerifyEmailOTPReqDTO>({otp: '', sessionId: ''} as VerifyEmailOTPReqDTO);
    const [seconds, setSeconds] = useState<number>(0);
    const navigate = useNavigate();
    const {showMessage, MessageComponent} = useMessage();
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const [isVerified, setIsVerified] = useState<boolean>(false);
    const [isSendingOTP, setIsSendingOTP] = useState<boolean>(false);
    //variables
    var service = new AuthenService();

    useEffect(() => {
        if(!register || !register.email || register.email === '') {
            window.history.back();
        }

        if(firstRender) {
            firstRender = false;
            handleSendOTP();
        }
    }, []);

    useEffect(() => {
        if (seconds > 0) {
            const timer = setTimeout(() => setSeconds(seconds - 1), 1000);
            return () => clearTimeout(timer);
        }
    }, [seconds]);

    const handleSendOTP = async () => {
        showLoading();
        try {
            const authEmail = {email: register.email} as AuthenEmailReqDTO;
            const req = {datas: authEmail} as RequestBaseDTO<AuthenEmailReqDTO>;
            const resp = await service.AuthenticateEmail(req);
            hideLoading();
            if(resp.StatusCode != 200) {
                await showMessage({
                    type: Constants.ERROR_MESSAGE_TYPE,
                    message: 'Failed to send OTP. Please try again later.'
                });
                window.history.back();
            }
            else {
                setVrfReq({...vrfReq, sessionId: resp.datas.sessionId});
            }
        }
        catch (error) {
            hideLoading();
            await showMessage({
                type: Constants.ERROR_MESSAGE_TYPE,
                message: 'Failed to send OTP. Please try again later.'
            });
            window.history.back();
        }
        //reset seconds
        setSeconds(60);
    }
    
    const handleVerifyEmail = async () => {
        //checkerror
        if(vrfReq.otp === '') {
            setMessage('OTP is required');
            return;
        }

        setIsSendingOTP(true);
        try {
            const req = {datas: vrfReq} as RequestBaseDTO<VerifyEmailOTPReqDTO>;
            const resp = await service.VerifyEmailOTP(req);
            setIsSendingOTP(false);
            if(resp.StatusCode != 200) {
                if(resp.StatusCode === 400 && resp.datas.message != null && resp.datas.message !== '') {
                        setMessage(resp.datas.message);
                }
                else {
                      await showMessage({
                        type: Constants.ERROR_MESSAGE_TYPE,
                        message: 'Failed to verify email. Please try again later.'
                    });
                    return;
               }
            }
            else {
                setIsVerified(true);
                setTimeout(() => {
                    navigate('/register/implement-register');
                }, 2000);
            }
        }
        catch (error) {
            setIsSendingOTP(false);
            await showMessage({
                type: Constants.ERROR_MESSAGE_TYPE,
                message: 'Failed to verify email. Please try again later.'
            });
            return;
        }
    };

    return (
        <>
            <LoadingComponent/>
            <MessageComponent/>
            <div className='register' style={{zIndex: '1'}}>
                <div className='register-title'>
                    Verify Email
                </div>
                <div className='container register-form'>
                <div className='row' style={{padding: '60px 30px 10px 30px'}}>
                        <div className='col text-center'>
                            {isVerified ?
                                <FontAwesomeIcon icon={faCircleCheck} style={{color: 'green', fontSize: '50px'}}/>
                                :
                                isSendingOTP ?
                                    <FontAwesomeIcon icon={faSpinner} spin style={{color: 'blue', fontSize: '50px'}}/>
                                    :
                                    seconds === 0 ?
                                        <button className='register-btn' onClick={handleSendOTP}>Resend OTP</button>
                                        :
                                        <label style={{fontFamily: 'Dejavu Serif', color: 'red' }}>{seconds + "s"}</label>
                                
                            }
                        </div>
                    </div>
                    <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='text' 
                                placeholder='OTP' 
                                className='otp-field text-center' 
                                value={vrfReq.otp} 
                                onChange={(e) => {
                                    if(e.target.value.length > 6) {
                                        return;
                                    }
                                    var vrfReqInfo = {...vrfReq, otp: e.target.value}; 
                                    setVrfReq({...vrfReqInfo});
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
                    <div className='row' style={{padding: '10px 30px 20px 30px'}}>  
                        <div className='col text-center'>
                            <label style={{fontFamily: 'Dejavu Serif', color: 'black', maxWidth: '400px', wordWrap: 'break-word'}}>
                                <i>An OTP has been sent to your email. Please check your inbox and enter the OTP below.</i>
                            </label>
                        </div>
                    </div>
                    <div className='row' style={{padding: '0px 30px 20px 30px'}}>
                        <div className='col'>
                            <button className='register-btn' onClick={() => {window.history.back();}}>Cancel</button>
                        </div>
                        <div className='col' style={{textAlign: 'right'}}>
                            <button className='register-btn' onClick={handleVerifyEmail}>Send</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default VerifyEmailFormComponent;