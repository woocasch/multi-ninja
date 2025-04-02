import React, { useMemo } from "react";
import './lifes.css';
import lostLifeImage from '../assets/shuriken-empty.svg';
import remainingLifeImage from '../assets/shuriken-full.svg';

export interface Properties {
    lifesLost: number;
    lifesAvailable: number;
}

export default function LifesComponent(props: Properties) {
    const lifesRemaining = useMemo(() => {
        return props.lifesAvailable - props.lifesLost;
    }, [props.lifesLost, props.lifesAvailable]);

    return (
        <div class="lifes-display">
            {[...Array(lifesRemaining)].map((x, i) => (
                <img key={i} src={remainingLifeImage} style={{ width: '20px' }} />
            ))}
            {[...Array(props.lifesLost)].map((x, i) => (
                <img key={i} src={lostLifeImage} style={{ width: '20px' }} />
            ))}
        </div>
    );
}