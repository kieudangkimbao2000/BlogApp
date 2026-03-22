class ApiClient
{
    static async post<T>(endpoint: string, req: any) : Promise<T>
    {
        const resp = await fetch(`${import.meta.env.VITE_API_URL}/${endpoint}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('JWT')}`
            },
            body: JSON.stringify(req)
        });

        const data = await resp.json();

        return {...data, StatusCode: (!data.StatusCode || data.StatusCode == 0) ? 
                                                        resp.status : data.StatusCode} as T;
    }

    static async get<T>(endpoint: string, req: any) : Promise<T>
    {
        const resp = await fetch(`${import.meta.env.VITE_API_URL}/${endpoint}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('JWT')}`
            },
            // body: JSON.stringify(req ?? {})
        });
        const data = await resp.json();

        return {...data, StatusCode: (!data.StatusCode || data.StatusCode == 0) ? 
                                                        resp.status : data.StatusCode} as T;
    }
}

export default ApiClient;