export interface SetTokenDataRequest {
    userId?: string;
    userName?: string;
    firstName?: string;
    lastName?: string;
}

export interface SetTokenDataResponse {
}

export interface Service {
    SetTokenData(request: SetTokenDataRequest): SetTokenDataResponse;
}
