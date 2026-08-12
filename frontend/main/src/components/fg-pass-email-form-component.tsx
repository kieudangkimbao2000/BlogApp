//libraries
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
//modules
import type { AuthenEmailReqDTO } from '../models/generated-interfaces';
import type { RequestBaseDTO } from '../models/request-base-dto';

const FgPassEmailFormComponent = () => {
    //hooks
    const [message, setMessage] = useState<string>('');
    const navigate = useNavigate();
    const [authenReq, setAuthenReq] = useState<RequestBaseDTO<AuthenEmailReqDTO>>({datas: {email: ''}});

    const handleSendEmail = async () => {
        if(!authenReq || !authenReq.datas || !authenReq.datas.email || 
                                                authenReq.datas.email === ''){
            setMessage('Email is required');
            return;
        }
   
        navigate('verify-email', {state: {auth: authenReq.datas}});
    }

    return (
        <>
            <div className='forgot-password' style={{zIndex: '1'}}>
                <div className='forgot-password-title'>
                    Forgot Password
                </div>
                <div className='container forgot-password-form'>
                    <div className='row' style={{padding: '60px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='text' 
                                placeholder='Email' 
                                className='email-field' 
                                value={authenReq.datas.email} 
                                onChange={(e) => {
                                    var authenReqInfo = {...authenReq, datas: {email: e.target.value}}; 
                                    setAuthenReq({...authenReqInfo});
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
                            <button className='forgot-password-btn' onClick={handleSendEmail}>Send</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default FgPassEmailFormComponent;