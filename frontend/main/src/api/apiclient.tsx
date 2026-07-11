import type { forEach } from "@tiptap/core";

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
        });

        const data = await resp.json();

        return {...data, StatusCode: (!data.statusCode || data.statusCode == 0) ? 
                                                        resp.status : data.statusCode} as T;
    }

    static async get<T>(endpoint: string) : Promise<T>
    {
        const resp = await fetch(`${import.meta.env.VITE_API_URL}/${endpoint}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('JWT')}`
            }
        });
        const data = await resp.json();

        return {...data, StatusCode: (!data.StatusCode || data.StatusCode == 0) ? 
                                                        resp.status : data.StatusCode} as T;
    }

    static async delete<T>(endpoint: string) : Promise<T>
    {
        const resp = await fetch(`${import.meta.env.VITE_API_URL}/${endpoint}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('JWT')}`
            }
        });
        const data = await resp.json();

        return {...data, StatusCode: (!data.StatusCode || data.StatusCode == 0) ? 
                                                        resp.status : data.StatusCode} as T;
    }
}

export default ApiClient;