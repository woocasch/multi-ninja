import { useState } from "react";
import { Service } from "./auth.service";
import LoginButtonComponent from "./login-button";
import UserNameComponent from "./username";

export default function UserDisplayComponent() {
    const [isAuthenticated, setIsAuthenticated] = useState<Boolean>(false);
    const [displayName, setDisplayName] = useState<string>('');

    Service.CheckCredentials({})
        .then(v => {
            setIsAuthenticated(v.isAuthenticated);
            setDisplayName(v.displayName!);
        })

    return isAuthenticated
        ? (<UserNameComponent displayName={displayName} />)
        : (<LoginButtonComponent />);
}