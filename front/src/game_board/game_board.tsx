"use client"
import React from "react";
import './game_board.css';
import { ChangeEvent, useState } from "react";
import { AnsweredQuestion, GameManager, Question, StartGameParameters } from "./game_manager";
import { Localizations, StaticTexts } from "./localizations";
import OptionsSelector, { SelectedOptions } from "./game_options";
import MultiplicationView, { MultiplicationResultModel } from "./multiplication";

export default function GameBoard() {
    const [currentQuestion, setCurrentQuestion] = useState<Question | null>(null);
    const [answer, setAnswer] = useState('');
    const [isGameStarted, setIsGameStarted] = useState<boolean>(false);

    function handleStartGame(params: SelectedOptions) {
        const gameInput: StartGameParameters = {
            level: params.level,
            setQuestionCallback: (newQuestion) => { setCurrentQuestion(newQuestion); setAnswer(''); },
        }
        setIsGameStarted(GameManager.StartGame(gameInput));
    }

    function questionResultCallback(model: MultiplicationResultModel) {
        GameManager.SetAnswers(model.AnswersProvided);
    }

    return (
        <div className="game_area">
            <OptionsSelector startGameCallback={handleStartGame} />
            {isGameStarted ?
                (
                    <MultiplicationView LeftFactor={currentQuestion!.LeftFactor} RightFactor={currentQuestion!.RightFactor} RemainingErrors={3} ResultCallback={questionResultCallback} />
                ) : null}
        </div>
    )
}