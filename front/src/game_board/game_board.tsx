"use client"
import React from "react";
import './game_board.css';
import { useState } from "react";
import { GameManager, Question, StartGameParameters } from "./game_manager";
import OptionsSelector, { SelectedOptions } from "./game_options";
import MultiplicationView, { MultiplicationResultModel } from "./multiplication";
import GameHudView from './game_hud';
import { GameStatus } from "./game_state";
import PostGameSummaryView from "./post-game-summary";

export default function GameBoard() {
    const [currentQuestion, setCurrentQuestion] = useState<Question | null>(null);
    const [_, setAnswer] = useState('');
    const [isGameStarted, setIsGameStarted] = useState<boolean>(false);
    const [lifesLost, setLifesLost] = useState<number>(0);
    const [gameStatus, setGameStatus] = useState<GameStatus>(GameStatus.None);

    function handleStartGame(params: SelectedOptions) {
        const gameInput: StartGameParameters = {
            level: params.level,
            setQuestionCallback: (newQuestion) => { setCurrentQuestion(newQuestion); setAnswer(''); },
            updateGameStatusCallback: (model) => { setLifesLost(model.LifesLost); setGameStatus(model.GameStatus); },
        }
        setIsGameStarted(GameManager.StartGame(gameInput));
    }

    function questionResultCallback(model: MultiplicationResultModel) {
        GameManager.SetAnswers(model.AnswersProvided);
    }

    return (
        <div className="game_area">
            {!isGameStarted ?
                (
                    <OptionsSelector startGameCallback={handleStartGame} />
                ) : null}
            {isGameStarted ?
                (
                    <>
                        {gameStatus == GameStatus.Started ?
                            (
                                <div className="active_game">
                                    <MultiplicationView
                                        LeftFactor={currentQuestion!.LeftFactor}
                                        RightFactor={currentQuestion!.RightFactor}
                                        RemainingErrors={GameManager.GetNumberOfLifes() - lifesLost}
                                        ResultCallback={questionResultCallback} />
                                    <GameHudView
                                        lifesLost={lifesLost}
                                        availableLifes={GameManager.GetNumberOfLifes()} />
                                </div>
                            ) : null}
                        {gameStatus == GameStatus.Completed ?
                            (
                                <PostGameSummaryView />
                            ) : null}
                    </>
                ) : null}
        </div>
    )
}