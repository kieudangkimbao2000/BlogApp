//libraries
import { useEffect, useState } from 'react';
import { useNavigate, useOutletContext } from 'react-router-dom';
import {faCircleCheck, faSpinner} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
//modules
import type { RegisterReqDTO } from '../models/generated-interfaces';
import useMessage from '../hooks/useMessage';
import AuthenService from '../services/authen-service';
import type { RequestBaseDTO } from '../models/request-base-dto';
import Constants from '../common/constant';
import { AuthenValidator } from '../validator/authen-validator';

//variables
var firstRender = true;

const ImplementRegisterComponent = () => {
    const {register, setRegister} = useOutletContext() as {register: RegisterReqDTO, 
            setRegister: React.Dispatch<React.SetStateAction<RegisterReqDTO>>};
    //hooks
    const navigate = useNavigate();
    const {showMessage, MessageComponent} = useMessage();
    const [isRegistering, setIsRegistering] = useState<boolean>(false);
    //variables
    var service = new AuthenService();

    useEffect(() => {
        const [isValid, errorMessage] = AuthenValidator.validateRegisterRequest(register);
        if(!isValid) {
            window.alert('There is an error in the registration data!');
            navigate('/register');
            return;
        }

        if(firstRender) {
            firstRender = false;
            handleRegister();
        }
    }, []);
    
    const handleRegister = async () => {
        setIsRegistering(true);
        try {
            const req = {datas: register} as RequestBaseDTO<RegisterReqDTO>;
            const resp = await service.Register(req);
            setIsRegistering(false);
            if(resp.StatusCode != 200) {
                await showMessage({
                    type: Constants.ERROR_MESSAGE_TYPE,
                    message: 'Failed to register. Please try again later.'
                });
                navigate('/register');
                return;
            }
            else {
                localStorage.setItem('JWT', resp.datas.token ?? '');
                setTimeout(() => {
                    navigate('/blog');
                }, 2000);
                return;
            }
        }
        catch (error) {
            setIsRegistering(false);
            await showMessage({
                type: Constants.ERROR_MESSAGE_TYPE,
                message: 'Failed to register. Please try again later.'
            });
            navigate('/register');
            return;
        }
    };

    return (
        <>
            <MessageComponent/>
            <div className='register' style={{zIndex: '1'}}>
                <div className='register-title'>
                    Implement Register
                </div>
                <div className='container register-form'>
                    <div className='row' style={{padding: '60px 30px 10px 30px'}}>
                            <div className='col text-center'>
                                {isRegistering ?
                                    <FontAwesomeIcon icon={faSpinner} spin style={{color: 'blue', fontSize: '50px'}}/>
                                    :
                                    <FontAwesomeIcon icon={faCircleCheck} style={{color: 'green', fontSize: '50px'}}/>
                                }
                            </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default ImplementRegisterComponent;