import { ChangeEvent, useState } from "react";
import { DifficultyLevel, GameManager, StartGameParameters } from "./game_manager";
import { Localizations, StaticTexts } from "./localizations";

export interface Properties {
    startGameCallback: (parameters: StartGameParameters) => void;
}

export default function OptionsSelector(prop: Properties) {
    const [selectedLevel, setSelectedLevel] = useState<DifficultyLevel>(DifficultyLevel.Easy);
    function handleStartGame() {
        const params: StartGameParameters = {
            level: selectedLevel,
        };
        prop.startGameCallback(params);
    }

    function onSelectedLevelChange(e: ChangeEvent<HTMLSelectElement>) {
        const parsed = parseInt(e.target.value);
        if (isNaN(parsed)) {
            return;
        }

        setSelectedLevel(parsed);
    }

    return (
        <div>
            <select value={selectedLevel} onChange={onSelectedLevelChange}>
                {GameManager.GetAvailableDifficultyLevels().map(l => (
                    <option value={l} key={l}>{Localizations.GetDifficultyLevelText(l)}</option>
                ))}
            </select>
            <button onClick={handleStartGame}>{Localizations.TranslateStaticText(StaticTexts.BtnStartGame_Text)}</button>
        </div>);
}