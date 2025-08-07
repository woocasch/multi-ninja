import { useState } from "react";
// import { Service } from "./auth.service";
// import * as ServiceContracts from './auth.contract';
import LoginButtonComponent from "./login-button";
import UserNameComponent from "./username";
import { useKeycloak } from "@react-keycloak/web";

export default function UserDisplayComponent() {
    const [isAuthenticated, setIsAuthenticated] = useState<Boolean>(false);
    const [displayName, setDisplayName] = useState<string>('');
    const { keycloak, initialized } = useKeycloak();

    if (keycloak.authenticated) {
    //     const setTokenRequest: ServiceContracts.SetTokenDataRequest = {
    //         userId: keycloak.idTokenParsed?.sub,
    //         userName: keycloak.idTokenParsed?.preferred_username,
    //         firstName: keycloak.idTokenParsed?.given_name,
    //         lastName: keycloak.idTokenParsed?.family_name,
    //     };
    //     Service.SetTokenData(setTokenRequest);
    //     setDisplayName(setTokenRequest.firstName ?? '---');
    //     setIsAuthenticated(true);
    }

    return (
        <>
            {isAuthenticated
                ? (<UserNameComponent displayName={displayName} />)
                : (<LoginButtonComponent />)}
        </>);
}