import { useEffect, useState } from "react";
import  {jwtDecode, type JwtPayload} from 'jwt-decode';
import LoginService from "../services/login-service";

const useAuthen = () =>
{
    const [user, setUser] = useState<any>({});

    useEffect(() => {
        try{
            const payload = jwtDecode(localStorage.getItem('JWT')??'');
            setUser(payload);
        }catch(err)
        {
            
        }
    },[]);

    return [user]
};

export default useAuthen;

