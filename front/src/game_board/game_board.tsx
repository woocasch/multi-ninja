"use client"
import React from "react";
import './game_board.css';
import { ChangeEvent, useState } from "react";
import { AnsweredQuestion, GameManager, Question, StartGameParameters } from "./game_manager";
import { Localizations, StaticTexts } from "./localizations";
import OptionsSelector, { SelectedOptions } from "./game_options";
import MultiplicationView, { MultiplicationResultModel } from "./multiplication";
import GameHudView from './game_hud';

export default function GameBoard() {
    const [currentQuestion, setCurrentQuestion] = useState<Question | null>(null);
    const [_, setAnswer] = useState('');
    const [isGameStarted, setIsGameStarted] = useState<boolean>(false);
    const [lifesLost, setLifesLost] = useState<number>(0);

    function handleStartGame(params: SelectedOptions) {
        const gameInput: StartGameParameters = {
            level: params.level,
            setQuestionCallback: (newQuestion) => { setCurrentQuestion(newQuestion); setAnswer(''); },
            updateGameStatusCallback: (model) => { setLifesLost(model.LifesLost); },
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
                    <>
                        <MultiplicationView LeftFactor={currentQuestion!.LeftFactor} RightFactor={currentQuestion!.RightFactor} RemainingErrors={3} ResultCallback={questionResultCallback} />
                        <GameHudView lifesLost={lifesLost} availableLifes={GameManager.GetNumberOfLifes()} />
                    </>
                ) : null}
        </div>
    )
}