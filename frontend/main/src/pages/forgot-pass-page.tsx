//css
import '../assets/css/common.css';
import '../assets/css/forgot-pass.css';
import { Outlet } from 'react-router-dom';
import { useState } from 'react';

const ForgotPassPage = () => {
    const [register, setRegister] = useState({username: '', password: '', repassword: '', fullName: '', email: ''});

    return(
        <>
            <div className="background">
            </div>
            <div style={{width: '100%', height: '100%', display: 'flex', justifyContent: 'center'}}>
                <Outlet context={{register, setRegister}}/>
            </div>
        </>
    )
}

export default ForgotPassPage;