import { useKeycloak } from "@react-keycloak/web";
import { useState } from "react";
import { NavLink } from "react-router";

export default function LoginButtonComponent() {
    const { keycloak, initialized } = useKeycloak();

    const [loginUrl, setLoginUrl] = useState<string>('');

    if (initialized) {
        keycloak.createLoginUrl()
            .then(v => setLoginUrl(v));
    }

    return (<NavLink to={loginUrl}>Zaloguj</NavLink>);
}
