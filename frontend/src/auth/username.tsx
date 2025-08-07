export interface Properties {
    displayName: string;
}

export default function UserNameComponent(props: Properties){
    return (<div>{ props.displayName }</div>);
}