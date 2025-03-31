import React from "react";

export interface Properties {
    lifesLost: number;
    availableLifes: number;
}

export default function GameHudView(props: Properties) {
    return (
        <div className="hud">
            <div>
                Lifes: {props.lifesLost} / {props.availableLifes}
            </div>
        </div>
    );
}