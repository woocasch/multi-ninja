import axios from 'axios';

export interface AuthenticateResponse {
    userName?: string;
    displayName?: string;
    token?: string;
}

interface CreateTokenOutput {
    userName: string;
    displayName: string;
}

export class AuthApi {
    public async Authenticate(userName: string, password: string): Promise<AuthenticateResponse> {
        const body = {
            userName: userName,
            password: password
        };
        const response = await axios.post<CreateTokenOutput>(
            'http://localhost:5078/api/auth/createToken',
            body);
        console.log(response);
        if (response.status != 200){
            return {
                token: '',
                userName: '',
                displayName: '',
            };
        }

        return {
            token: 'TBD',
            userName: response.data.userName,
            displayName: response.data.displayName,
        };
    }
}