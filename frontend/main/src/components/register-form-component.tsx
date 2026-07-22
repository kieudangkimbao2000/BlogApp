import { useState } from 'react';
import { useNavigate, useOutletContext } from 'react-router-dom';
//modules
import type { RegisterReqDTO } from '../models/generated-interfaces';
import { AuthenValidator } from '../validator/authen-validator';
import useLoading from '../hooks/useLoading';
import type { RequestBaseDTO } from '../models/request-base-dto';
import AuthenService from '../services/authen-service';
import useMessage from '../hooks/useMessage';
import Constants from '../common/constant';

const RegisterFormComponent = () => {
    const {register, setRegister} = useOutletContext() as {register: RegisterReqDTO, 
            setRegister: React.Dispatch<React.SetStateAction<RegisterReqDTO>>};
    const [message, setMessage] = useState<string>('');
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage(); 
    const navigate = useNavigate();
    //variables
    const service  = new AuthenService();
    
    const handleRegister = async () => {
        //checkerror
        const [isOk, errorMessage] = AuthenValidator.validateRegisterRequest(register);
        if(!isOk) {
            setMessage(errorMessage ?? '');
            return;
        }

        showLoading();
        try {
            const req = {datas: register, base64Strings: []} as RequestBaseDTO<RegisterReqDTO>;
            const resp = await service.ValidateRegisterInfo(req);
            hideLoading();
            if(resp.statusCode === 200) {
                navigate('verify-email');
            }else {
                if(resp.datas && resp.datas.message && resp.datas.message !== '') {
                    setMessage(resp.datas.message);
                }
                else
                {
                    await showMessage({
                        type: Constants.ERROR_MESSAGE_TYPE,
                        message: 'An error occurred while processing your request.'
                    });
                }
            }
        } catch (error) {
            hideLoading();
            await showMessage({
                type: Constants.ERROR_MESSAGE_TYPE,
                message: 'An error occurred while processing your request.'
            });
            return;
        }
    }

    return (
        <>
            <LoadingComponent />
            <MessageComponent />
            <div className='register' style={{zIndex: '1'}}>
                <div className='register-title'>
                    Register
                </div>
                <div className='container register-form'>
                    <div className='row' style={{padding: '60px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='text' 
                                placeholder='Username' 
                                className='username-field' 
                                value={register.username} 
                                onChange={(e) => {var registerInfo = {...register, username: e.target.value}; setRegister({...registerInfo});}}
                                onFocus={() => {
                                    const lb = document.getElementById('username');
                                    if(!lb) return;
                                    lb.style.visibility='hidden';
                                }}
                                onBlur={() => {
                                    const lb = document.getElementById('username');
                                    if(!lb) return;
                                    if(!register.username) lb.style.visibility='visible';
                                }}/>
                        </div>
                    </div>
                    <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='password'
                                placeholder='Password'
                                className='password-field'
                                value={register.password} 
                                onChange={(e) => {var registerInfo = {...register, password: e.target.value}; setRegister({...registerInfo});}}
                                onFocus={() => {
                                    const lb = document.getElementById('password');
                                    if(!lb) return;
                                    lb.style.visibility='hidden';
                                }}
                                onBlur={() => {
                                    const lb = document.getElementById('password');
                                    if(!lb) return;
                                    if(!register?.password) lb.style.visibility='visible';
                                }}/>
                        </div>
                    </div>
                    <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='password'
                                placeholder='Repeat Password'
                                className='repassword-field'
                                value={register.repassword} 
                                onChange={(e) => {var registerInfo = {...register, repassword: e.target.value}; setRegister({...registerInfo});}}
                                onFocus={() => {
                                    const lb = document.getElementById('repassword');
                                    if(!lb) return;
                                    lb.style.visibility='hidden';
                                }}
                                onBlur={() => {
                                    const lb = document.getElementById('repassword');
                                    if(!lb) return;
                                    if(!register?.repassword) lb.style.visibility='visible';
                                }}/>
                        </div>
                    </div>
                    <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='text'
                                placeholder='Full Name'
                                className='fullname-field'
                                value={register.fullname} 
                                onChange={(e) => {var registerInfo = {...register, fullname: e.target.value}; setRegister({...registerInfo});}}
                                onFocus={() => {
                                    const lb = document.getElementById('fullname');
                                    if(!lb) return;
                                    lb.style.visibility='hidden';
                                }}
                                onBlur={() => {
                                    const lb = document.getElementById('fullname');
                                    if(!lb) return;
                                    if(!register?.fullname) lb.style.visibility='visible';
                                }}/>
                        </div>
                    </div>
                    <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                        <div className='col'>
                            <input type='text'
                                placeholder='Email'
                                className='email-field'
                                value={register.email} 
                                onChange={(e) => {var registerInfo = {...register, email: e.target.value}; setRegister({...registerInfo});}}
                                onFocus={() => {
                                    const lb = document.getElementById('email');
                                    if(!lb) return;
                                    lb.style.visibility='hidden';
                                }}
                                onBlur={() => {
                                    const lb = document.getElementById('email');
                                    if(!lb) return;
                                    if(!register?.email) lb.style.visibility='visible';
                                }}/>
                        </div>
                    </div>
                    {(message || message !== '') ? 
                        <div className='row' style={{padding: '0px 30px 10px 30px'}}>
                            <div className='col'>
                                <label style={{color: 'red',fontFamily: 'Dejavu Serif' }}>
                                    <i>{message}</i>
                                </label>
                            </div>
                        </div> : null
                    }
                    <div className='row' style={{padding: '0px 30px 20px 30px'}}>
                        <div className='col'>
                            <button className='register-btn' onClick={() => {window.history.back();}}>Cancel</button>
                        </div>
                        <div className='col' style={{textAlign: 'right'}}>
                            <button className='register-btn' onClick={handleRegister}>Register</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default RegisterFormComponent;