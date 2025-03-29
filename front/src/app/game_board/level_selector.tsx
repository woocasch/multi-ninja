import { ChangeEvent, useState } from "react";
import { DifficultyLevel, GameManager } from "./game_manager";
import { Localizations } from "./localizations";

export interface Properties {
    value: DifficultyLevel,
    onChange: (val: DifficultyLevel) => void,
}

export default function LevelSelector(props: Properties) {
    function onSelectedLevelChange(e: ChangeEvent<HTMLSelectElement>) {
        const parsed = parseInt(e.target.value);
        if (isNaN(parsed)) {
            return;
        }

        props.onChange(parsed);
    }

    return (
        <select value={props.value} onChange={onSelectedLevelChange}>
            {GameManager.GetAvailableDifficultyLevels().map(l => (
                <option value={l} key={l}>{Localizations.GetDifficultyLevelText(l)}</option>
            ))}
        </select>);
}