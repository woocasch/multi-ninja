import React, { ChangeEvent, Dispatch, SetStateAction, useState } from 'react';
import './game_settings.scss';
import { DifficultyLevels } from './difficulty_levels';
import { DifficultyLevel } from '../common/types';

interface Properties {
  difficultyLevel: DifficultyLevel;
  setDifficultyLevel: (difLev: DifficultyLevel) => void;
  onStartGameRequested: () => void;
}

export default function GameSettingsComponent(props: Properties) {
  function onSelectedLevelChange(e: ChangeEvent<HTMLSelectElement>) {
    const parsed = parseInt(e.target.value);
    if (isNaN(parsed)) {
      return;
    }
    props.setDifficultyLevel(parsed);
  }

  return (
    <div className='game-settings'>
      <div>
        Wybierz poziom trudności:
      </div>
      <div className='difficulty-level'>
        <select
          value={props.difficultyLevel}
          id="difficultyLevel"
          onChange={onSelectedLevelChange}
          className='difficulty-level'
        >
          {DifficultyLevels.GetDifficultyLevels().map((l) => (
            <option value={l.level} key={l.level}>
              {l.displayName}
            </option>
          ))}
        </select>
      </div>
      <div className='buttons'>
        <button onClick={props.onStartGameRequested}>Rozpocznij</button>
      </div>
    </div>
  );
}
