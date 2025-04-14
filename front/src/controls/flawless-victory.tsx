import React from 'react';
import buzzlightyear from '../assets/victory-images/buzzlightyear.png';
import './flawless-victory.scss';

export interface Properties {
  pointsScored: number;
  onNewGameRequested: () => void;
  onGameConfigurationRequested: () => void;
}

export default function FlawlessVictoryComponent(props: Properties) {
  return (
    <div className='flawless-victory'>
      <div className="star-decoration star-1"></div>
      <div className="star-decoration star-2"></div>
      <div className="star-decoration star-3"></div>
      <div className="star-decoration star-4"></div>
      <div className="image-container">
        <img
          src={buzzlightyear}
          alt="Nagroda"
          className="reward-image"
          id="rewardImage"
        />
      </div>

      <div className="score-display">
        Zdobyłeś <span id="finalScore">{props.pointsScored}</span> punktów!
      </div>

      <p className="message">
        Świetnie opanowałeś tabliczkę mnożenia! Kontynuuj naukę, aby zostać
        mistrzem matematyki!
      </p>

      <div className="buttons-container">
        <button
          className="button button-restart"
          id="restartButton"
          onClick={props.onNewGameRequested}
        >
          Zagraj ponownie
        </button>
        <button
          className="button"
          id="nextLevelButton"
          onClick={props.onGameConfigurationRequested}
        >
          Nowa gra
        </button>
      </div>
      <div className="pyro">
        <div className="before"></div>
        <div className="after"></div>
      </div>
    </div>
  );
}
