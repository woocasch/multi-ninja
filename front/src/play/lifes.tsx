import React from "react";

export interface Properties {
    lifesLost: number;
    lifesAvailable: number;
}

export default function LifesComponent(props: Properties) {
    return (
        <div>
            {props.lifesLost}/{props.lifesAvailable}
        </div>
    );
}