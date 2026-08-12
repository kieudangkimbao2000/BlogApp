class ApiClient
{
    static async post<T>(endpoint: string, req: any) : Promise<T>
    {
        const resp = await fetch(`${import.meta.env.VITE_API_URL}/${endpoint}`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('JWT')}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(req)
        }).then((resp) => {
            if(resp.headers.get('Content-Type')?.includes('application/json')) {
                return resp.json();
            } else {
                return {statusCode: resp.status, message: resp.statusText};
            }
        }).catch((err) => {
            return {statusCode: 500, message: err.message} as T;
        });

        return resp;
    }

    static async get<T>(endpoint: string) : Promise<T>
    {
        const resp = await fetch(`${import.meta.env.VITE_API_URL}/${endpoint}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('JWT')}`
            }
        }).then((resp) => {
            if(resp.headers.get('Content-Type')?.includes('application/json')) {
                return resp.json();
            } else {
                return {statusCode: resp.status, message: resp.statusText};
            }
        }).catch((err) => {
            return {statusCode: 500, message: err.message} as T;
        });

        return resp;
    }

    static async delete<T>(endpoint: string) : Promise<T>
    {
        const resp = await fetch(`${import.meta.env.VITE_API_URL}/${endpoint}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('JWT')}`
            }
        }).then((resp) => {
            if(resp.headers.get('Content-Type')?.includes('application/json')) {
                return resp.json();
            } else {
                return {statusCode: resp.status, message: resp.statusText};
            }
        }).catch((err) => {
            return {statusCode: 500, message: err.message} as T;
        });

        return resp;
    }
}

export default ApiClient;