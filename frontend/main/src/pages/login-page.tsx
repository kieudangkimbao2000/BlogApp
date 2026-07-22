//libraries
import { Link, useNavigate } from 'react-router-dom';
//modules
import { useEffect, useState } from 'react';
import { AuthenValidator } from '../validator/authen-validator';
import type { LoginReqDTO } from '../models/generated-interfaces';
import LoginService from '../services/authen-service';
import useLoading from '../hooks/useLoading';
import useMessage from '../hooks/useMessage';
import type { RequestBaseDTO } from '../models/request-base-dto';
import Constant from '../common/constant';
//css
import '../assets/css/common.css';
import '../assets/css/login.css';

const LoginPage = () => {
    const service = new LoginService();

    const [login, setLogin] = useState<LoginReqDTO>({username: '', password: ''});
    const [message, setMessage] = useState<string>('');
    const navigate = useNavigate();
    const {showLoading, hideLoading, LoadingComponent} = useLoading();
    const {showMessage, MessageComponent} = useMessage();
    
    const handleLogin = async () => {
        showLoading();
        const [isOk, errorMessage] = AuthenValidator.validateLoginRequest(login);
        if(!isOk) {
            setMessage(errorMessage ?? '');
            hideLoading();
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
                    if(resp.statusCode == 500)
                    {
                        await showMessage({
                            type: Constant.ERROR_MESSAGE_TYPE,
                            message: resp.message ?? 'Internal server error'
                        });
                    }
                }
                hideLoading();
            }
        }
        catch(err)
        {
            hideLoading();
            console.log(err);
            await showMessage({
                type: Constant.ERROR_MESSAGE_TYPE,
                message: 'Internal server error'
            });
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
                                    placeholder='Username' 
                                    className='username-field' 
                                    value={login.username} 
                                    onChange={(e) => {var loginIngo = {...login, username: e.target.value}; setLogin({...loginIngo});}}
                                />
                            </div>
                        </div>
                        <div className='row' style={{padding: '10px 30px 10px 30px'}}>
                            <div className='col'>
                                <input type='password'
                                    placeholder='Password'
                                    className='password-field'
                                    value={login.password} 
                                    onChange={(e) => {var loginIngo = {...login, password: e.target.value}; setLogin({...loginIngo});}}
                                />
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
                                <button className='login-btn' onClick={() => navigate('/register')}>Register</button>
                            </div>
                            <div className='col' style={{textAlign: 'right'}}>
                                <button className='login-btn' onClick={handleLogin}>Login</button>
                            </div>
                        </div>
                        <div className='row' style={{padding: '0px 30px 30px 30px'}}>
                                <div className='col' >
                                    <Link to={''} className='login-link'>Forgot password</Link>
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