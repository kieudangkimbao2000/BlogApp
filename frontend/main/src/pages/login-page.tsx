import { useEffect, useState } from 'react';

import type { LoginReqDTO } from '../models/generated-interfaces';

import '../assets/css/common.css';
import '../assets/css/login.css';
import { Link, useNavigate } from 'react-router-dom';
import LoginService from '../services/authen-service';
import BlogAppMessage from '../common/message';
import useLoading from '../hooks/useLoading';
import useMessage from '../hooks/useMessage';
import { Modal } from '@mui/material';
import type { RequestBaseDTO } from '../models/request-base-dto';

const LoginPage = () => {
    const service = new LoginService();

    const [login, setLogin] = useState<LoginReqDTO>({username: '', password: ''});
    const [message, setMessage] = useState<string>('');
    const navigate = useNavigate();
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    
    const handleLogin = async () => {
        showLoading();
        //checkerror
        if(login.username == '')
        {
            hideLoading();
            setMessage(BlogAppMessage.LOGIN_FAIL_01);
            return;
        }
        if(login.password == '')
        {
            hideLoading();
            setMessage(BlogAppMessage.LOGIN_FAIL_02);
            return;
        }

        //main handle
        try
        {
            const req : RequestBaseDTO<LoginReqDTO> = {
                datas: login,
                base64Files: []
            };

            const resp = await service.Login(req);

            if(resp != null)
            {
                if(resp.statusCode == 200 && resp.datas != null)
                {
                    localStorage.setItem('JWT', resp.datas.token ?? '');
                    navigate('/blog');
                    
                    return;
                }
                else
                {
                    if(resp.statusCode == 400)
                    {
                        setMessage(BlogAppMessage.LOGIN_FAIL_03);
                    }
                    if(resp.statusCode == 500)
                    {
                        await showMessage({
                            type: BlogAppMessage.MSG_ERR_TYPE,
                            message: resp.message
                        });
                    }
                }
                hideLoading();
            }
        }catch(err)
        {
            hideLoading();
            console.log(err);
            setMessage(BlogAppMessage.LOGIN_FAIL_03);
        }
        }

    return(
        <>
            <div className="background">
            </div>
            <div style={{width: '100%', height: '100%', display: 'flex', justifyContent: 'center'}}>
                <div className='login' style={{zIndex: '1'}}>
                    <div className='login-title'>
                        Login
                    </div>
                    <div className='container login-form'>
                        <div className='row' style={{padding: '60px 30px 10px 30px'}}>
                            <div className='col'>
                                <input type='text' 
                                    placeholder='Tài khoản' 
                                    className='username-field' 
                                    value={login.username} 
                                    onChange={(e) => {var loginIngo = {...login, username: e.target.value}; setLogin({...loginIngo});}}
                                    onFocus={() => {
                                        const lb = document.getElementById('username');
                                        if(!lb) return;
                                        lb.style.visibility='hidden';
                                    }}
                                    onBlur={() => {
                                        const lb = document.getElementById('username');
                                        if(!lb) return;
                                        if(!login.username) lb.style.visibility='visible';
                                    }}/>
                            </div>
                        </div>
                        <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                            <div className='col'>
                                <input type='password'
                                    placeholder='Mật khẩu'
                                    className='password-field'
                                    value={login.password} 
                                    onChange={(e) => {var loginIngo = {...login, password: e.target.value}; setLogin({...loginIngo});}}
                                    onFocus={() => {
                                        const lb = document.getElementById('password');
                                        if(!lb) return;
                                        lb.style.visibility='hidden';
                                    }}
                                    onBlur={() => {
                                        const lb = document.getElementById('password');
                                        if(!lb) return;
                                        if(!login?.password) lb.style.visibility='visible';
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
                                <button className='login-btn' onClick={showMessage}>Đăng ký</button>
                            </div>
                            <div className='col' style={{textAlign: 'right'}}>
                                <button className='login-btn' onClick={handleLogin}>Đăng nhập</button>
                            </div>
                        </div>
                        <div className='row' style={{padding: '0px 30px 30px 30px'}}>
                                <div className='col' >
                                    <Link to={''} className='login-link'>Quên mật khẩu</Link>
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            <MessageComponent />
            <LoadingComponent />
        </>
    )
}

export default LoginPage;