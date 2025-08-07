import { AuthApi } from './auth.api';
import * as Contract from './auth.contract';

export class ServiceImplementation implements Contract.Service {
    private readonly authApi: AuthApi = new AuthApi();

    async CheckCredentials(_: Contract.CheckCredentialsRequest): Promise<Contract.CheckCredentialsResponse> {
        const response = await this.authApi.Authenticate('woocasch', 'secret-password');
        if (!response.token) {
            return {
                isAuthenticated: false,
                userName: '',
                displayName: '',
            };
        }

        // SET TOKEN AS DEFAULT AUTHENTICATION AUTOMATICALLY
        return {
            isAuthenticated: true,
            userName: response.userName,
            displayName: response.displayName
        };
    }
}

export const Service: Contract.Service = new ServiceImplementation();