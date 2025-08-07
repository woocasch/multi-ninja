import * as Contract from './auth.contract';

export class ServiceImplementation implements Contract.Service {
    SetTokenData(request: Contract.SetTokenDataRequest): Contract.SetTokenDataResponse {
        localStorage.setItem('TOKENDATA_USERID', request.userId ?? '---');
        localStorage.setItem('TOKENDATA_USERNAME', request.userName ?? '---');
        localStorage.setItem('TOKENDATA_DISPLAYNAME', `${request.firstName ?? '---'} ${request.lastName ?? '---'}`);
        return {};
    }
}

export const Service: Contract.Service = new ServiceImplementation();