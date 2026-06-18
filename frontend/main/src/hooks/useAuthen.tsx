import { useEffect, useState } from "react";
import  {jwtDecode, type JwtPayload} from 'jwt-decode';
import AuthenService from "../services/authen-service";

const useAuthen = () =>
{
    //hooks
    const [user, setUser] = useState<any>({});
    //services
    const authenService = new AuthenService();

    useEffect(() => {
        const jwt = localStorage.getItem('JWT') ?? '';

        const validToken =  handleCheckValidToken();

        if(!validToken)
        {
            setUser({Username: ''});
            return;
        }

        const payload = jwtDecode(jwt);
        setUser(payload);
    },[]);

    const handleCheckValidToken = async () : Promise<boolean> => {
        try{
            const resp =  await authenService.CheckValidToken();
            
            if(resp.statusCode == 403)
            {
                localStorage.removeItem('JWT');
                window.location.href = '/login';
                return false;
            }
            else
            {
                if(resp.statusCode != 200)
                {
                    localStorage.removeItem('JWT');
                    return false;
                }
            }

            return true;
        }
        catch(err)
        {
            localStorage.removeItem('JWT');
            window.location.href = '/error';
            return false;
        }
    }
        
    return [user];
};

export default useAuthen;

