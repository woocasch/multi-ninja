"use client"
import { ChangeEvent, useState } from "react";
import { GameManager, Question, StartGameParameters } from "./game_manager";
import { Localizations, StaticTexts } from "./localizations";
import OptionsSelector, { SelectedOptions } from "./game_options";

export default function Page() {
    const [currentQuestion, setCurrentQuestion] = useState<Question | null>(null);
    const [answer, setAnswer] = useState('');
    const [isGameStarted, setIsGameStarted] = useState<boolean>(false);

    function handleAnswerChange(e: ChangeEvent<HTMLInputElement>) {
        setAnswer(e.target.value);
    }

    function handleAcceptClick() {
        const result = parseInt(answer);
        if (isNaN(result)) {
            setAnswer('');
            return;
        }

        GameManager.SetAnswer(result);
    }

    function handleStartGame(params: SelectedOptions) {
        const gameInput: StartGameParameters = {
            level: params.level,
            setQuestionCallback: (newQuestion) => { setCurrentQuestion(newQuestion); setAnswer(''); },
        }
        setIsGameStarted(GameManager.StartGame(gameInput));
    }

    return (
        <div>
            <OptionsSelector startGameCallback={handleStartGame} />
            {isGameStarted ?
                (<div>
                    <span className="leftHand">{currentQuestion?.LeftFactor}</span>
                    <span className="multiplicationSymbol">*</span>
                    <span className="rightHand">{currentQuestion?.RightFactor}</span>
                    <span className="equalsSymbol">=</span>
                    <input type="text" value={answer} onChange={handleAnswerChange} />
                    <button onClick={handleAcceptClick}>{Localizations.TranslateStaticText(StaticTexts.BtnAcceptRespose_Text)}</button>
                </div>) : null}
        </div>
    )
}