import React from "react";
import { useState } from "react";
import { DifficultyLevel } from "./game_manager";
import { Localizations, StaticTexts } from "./localizations";
import LevelSelector from "./level_selector";

export interface Properties {
    startGameCallback: (parameters: SelectedOptions) => void;
}

export interface SelectedOptions {
    level: DifficultyLevel;
}

export default function GameOptions(prop: Properties) {
    const [selectedLevel, setSelectedLevel] = useState<DifficultyLevel>(DifficultyLevel.Easy);
    function handleStartGame() {
        const params: SelectedOptions = {
            level: selectedLevel,
        };
        prop.startGameCallback(params);
    }

    function onSelectedLevelChange(value: DifficultyLevel) {
        setSelectedLevel(value);
    }

    return (
        <div>
            <LevelSelector value={selectedLevel} onChange={onSelectedLevelChange} />
            <button onClick={handleStartGame}>{Localizations.TranslateStaticText(StaticTexts.BtnStartGame_Text)}</button>
        </div>);
}
