import { useCallback, useEffect, useState } from "react";
import  {jwtDecode, type JwtPayload} from 'jwt-decode';
import AuthenService from "../services/authen-service";

const useAuth = () =>
{
    //hooks
    const [user, setUser] = useState<any | null>(null);
    //services
    const authenService = new AuthenService();

    useEffect(() => {
        handleCheckValidToken();
    },[]);

    const handleCheckValidToken = async () => {
        try{
            const resp =  await authenService.CheckValidToken();
            
            if(resp.statusCode == 403)
            {
                localStorage.removeItem('JWT');
                setUser(null);
                window.location.href = '/login';
                return;
            }
            else
            {
                if(resp.statusCode != 200)
                {
                    localStorage.removeItem('JWT');
                    setUser(null);
                    return;
                }
            }

            const jwt = localStorage.getItem('JWT') ?? '';
            if(!jwt)
            {
                setUser(null);
                return;
            }

            const payload = jwtDecode(jwt) as JwtPayload & { Username?: string; username?: string; FullName?: string; Email?: string; Role?: string };
            setUser(payload);
        }
        catch(err)
        {
            localStorage.removeItem('JWT');
            setUser(null);
            window.location.href = '/error';
        }
    }
    
    const handleLoadingUserInfo = useCallback(async () => {
        const jwt = localStorage.getItem('JWT') ?? '';
        if(!jwt)
        {
            setUser(null);
            return;
        }

        const payload = jwtDecode(jwt) as JwtPayload & { Username?: string; username?: string; FullName?: string; Email?: string; Role?: string };
        setUser(payload);
    }, []);

    return {user, handleLoadingUserInfo};
};

export default useAuth;

