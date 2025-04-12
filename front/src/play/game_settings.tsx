import React, { ChangeEvent, Dispatch, SetStateAction, useState } from 'react';
import { DifficultyLevels } from './difficulty_levels';
import { DifficultyLevel } from './types';

interface Properties {
  difficultyLevel: DifficultyLevel;
  setDifficultyLevel: Dispatch<SetStateAction<DifficultyLevel>>;
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
    <div>
      <div>
        Poziom trudności:
        <select value={props.difficultyLevel} onChange={onSelectedLevelChange}>
          {DifficultyLevels.GetDifficultyLevels().map((l) => (
            <option value={l.level} key={l.level}>
              {l.displayName}
            </option>
          ))}
        </select>
      </div>
      <div>
        <button onClick={props.onStartGameRequested}>Rozpocznij</button>
      </div>
    </div>
  );
}
