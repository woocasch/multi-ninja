export interface CheckCredentialsRequest {
}

export interface CheckCredentialsResponse {
    isAuthenticated: boolean;
    displayName?: string;
    userName?: string;
}

export interface Service {
    CheckCredentials(request: CheckCredentialsRequest): Promise<CheckCredentialsResponse>;
}
