import axios from 'axios';
import * as Contracts from './auth.contracts';
import * as Payloads from './auth.payloads';

export class AuthApi {
    public async Authenticate(userName: string, password: string): Promise<Contracts.AuthenticateResponse> {
        const body = {
            userName: userName,
            password: password
        };
        const response = await axios.post<Payloads.CreateTokenOutput>(
            'http://localhost:5078/api/auth/createToken',
            body);

        if (response.status != 200) {
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