import React, { useMemo, useState } from "react";
import * as Model from './types';
import * as Logic from "./game_logic";
import GameSettingsComponent from "./game_settings";
import QuestionComponent from "./question";

export default function PlayComponent() {
    const [gameStatus, setGameStatus] = useState<Model.GameStatus>(Model.GameStatus.NotStarted);
    const [gameResult, setGameResult] = useState<Model.GameResult>(Model.GameResult.NotCompleted);
    const [difficultyLevel, setDifficultyLevel] = useState<Model.DifficultyLevel>(Model.DifficultyLevel.Easy);
    const [totalLifes, setTotalLifes] = useState<number>(0);
    const [lifesLost, setLifesLost] = useState<number>(0);
    const [currentQuestion, setCurrentQuestion] = useState<Model.Question>({ leftHand: 0, rightHand: 0 });
    const [currentAnswers, setCurrentAnswers] = useState<number[]>([]);
    const [previousQuestions, setPreviousQuestions] = useState<Model.AnsweredQuestion[]>([]);
    const [questionsToAnswer, setQuestionsToAnswer] = useState<number>(0);
    const isGameNotStarted = createGameStatusMemo(Model.GameStatus.NotStarted);
    const isGameInProgress = createGameStatusMemo(Model.GameStatus.InProgress);
    const isGameCompleted = createGameStatusMemo(Model.GameStatus.Completed);
    const questionsAnswered = useMemo(() => previousQuestions.length, [previousQuestions]);

    function onStartGameRequested() {
        const startGameParameters: Logic.StartGameParameters = { difficultyLevel: difficultyLevel };
        const startGameResult = Logic.GameLogic.StartGame(startGameParameters);
        setTotalLifes(startGameResult.totalLifes);
        setLifesLost(0);
        setQuestionsToAnswer(startGameResult.questionsToAnswer);
        StartNextQuestion(true);
        setGameStatus(Model.GameStatus.InProgress);
        setGameResult(Model.GameResult.NotCompleted);
    }

    function StartNextQuestion(isFirstQuestion: boolean) {
        if (!isFirstQuestion) {
            const answerToStore: Model.AnsweredQuestion = {
                question: {
                    leftHand: currentQuestion.leftHand,
                    rightHand: currentQuestion.rightHand,
                },
                expectedAnswer: currentQuestion.leftHand * currentQuestion.rightHand,
                providedAnswers: currentAnswers,
            };
            setPreviousQuestions(old => [...old, answerToStore]);
        }
        const selectQuestionParams: Logic.SelectQuestionParameters = { difficultyLevel: difficultyLevel, previousQuestions: previousQuestions };
        const selectQuestionResult = Logic.GameLogic.SelectQuestion(selectQuestionParams);
        setCurrentQuestion(old => {
            old.leftHand = selectQuestionResult.nextQuestion.leftHand;
            old.rightHand = selectQuestionResult.nextQuestion.rightHand;
            return old;
        });
        setCurrentAnswers([]);
    }

    function onAnswerAccepted(value: number) {
        let lostLifes = lifesLost;
        let questionsAsked = previousQuestions.length;
        const params: Logic.ValidateAnswerParameters = { question: currentQuestion!, providedAnswer: value };
        const result = Logic.GameLogic.ValidateAnswer(params);
        setCurrentAnswers(old => [...old, value]);
        if (!result.isValid) {
            setLifesLost(old => old + 1);
            lostLifes++;
        }

        const completedParams: Logic.CheckGameCompletedParameters = {
            difficultyLevel: difficultyLevel,
            lifesLost: lostLifes,
            questionsAsked: questionsAsked,
        };
        const completedResult = Logic.GameLogic.CheckGameCompleted(completedParams);
        if (completedResult.isCompleted) {
            setGameStatus(Model.GameStatus.Completed);
            setGameResult(completedResult.result);
            return;
        }

        if (result.isValid) {
            StartNextQuestion(false);
        }
    }

    function createGameStatusMemo(status: Model.GameStatus) {
        return useMemo(() => gameStatus == status, [gameStatus]);
    }

    return (
        <div>
            {isGameNotStarted ? (<GameSettingsComponent difficultyLevel={difficultyLevel} setDifficultyLevel={setDifficultyLevel} onStartGameRequested={onStartGameRequested} />) : null}
            {isGameInProgress ? (<QuestionComponent LeftFactor={currentQuestion.leftHand} RightFactor={currentQuestion.rightHand} OnAnswerAcceptedNotification={onAnswerAccepted} />) : null}
            Current answers: {JSON.stringify(currentAnswers)}<br />
            Lifes lost: {lifesLost}<br />
            Questions answered: {questionsAnswered}<br />
            Game result: {gameResult}<br/>
            <pre>
                {JSON.stringify(previousQuestions)}
            </pre>
        </div>
    )
}