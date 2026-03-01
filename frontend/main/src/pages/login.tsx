import { TextField } from '@mui/material';
import '../assets/css/common.css';
import '../assets/css/login.css';


const LoginPage = () => {
    return(
        <>
            <div className="background">
            </div>
            <div className='login'>
                <div className='login-title'>
                    Login
                </div>
                <div className='login-form'>
                    <input type='text' title='Username' className='username-field'/>
                    <input type='password' title='Username' className='password-field'/>
                </div>
            </div>
        </>
    )
}

export default LoginPage;