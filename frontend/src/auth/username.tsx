import { useKeycloak } from "@react-keycloak/web";

export interface Properties {
    displayName: string;
}

export default function UserNameComponent(props: Properties){
    const { keycloak } = useKeycloak();
    return (<div>
        { props.displayName }
        <a onClick={() => keycloak.login()}>Zaloguj</a>)
        </div>);
}