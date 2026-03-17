export interface LoginReq
{
    username?: string;
    password?: string
}

export interface Resp{
    statusCode: number,
    message: string
}

export interface LoginResp extends Resp
{
    jwt?: string;    
}

export interface Blog{
    
}