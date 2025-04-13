import React, { useEffect, useMemo, useState } from 'react';
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

  const animationTrigger = useEffect(() => {
    if (props.lifesLost == 0) {
      return;
    }

    setContainerClass('lifes-display lifes-switched');
    setTimeout(() => setContainerClass('lifes-display'), 1000);
  }, [lifesRemaining]);

  const [containerClass, setContainerClass] = useState<string>('lifes-display');

  return (
    <div className={containerClass}>
      {[...Array(lifesRemaining)].map((x, i) => (
        <img key={i} src={remainingLifeImage} style={{ width: '20px' }} />
      ))}
      {[...Array(props.lifesLost)].map((x, i) => (
        <img key={i} src={lostLifeImage} style={{ width: '20px' }} />
      ))}
    </div>
  );
}
