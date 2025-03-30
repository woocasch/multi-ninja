import React from "react";
import { useState } from "react";

export interface Properties {
    lifesLost: number;
    availableLifes: number;
}

export default function GameHudView(props: Properties) {
    return (
        <div>
            <div>
                Lifes: {props.lifesLost} / {props.availableLifes}
            </div>
        </div>
    );
}