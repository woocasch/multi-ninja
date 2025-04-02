import React, { useEffect, useMemo, useState } from "react";
import * as Model from './types';
import * as Logic from "./game_logic";
import GameSettingsComponent from "./game_settings";
import QuestionComponent from "./question";
import LifesComponent from "./lifes";
import ResultsComponent from "./results";

export default function PlayComponent() {
    const [gameStatus, setGameStatus] = useState<Model.GameStatus>(Model.GameStatus.NotStarted);
    const [gameResult, setGameResult] = useState<Model.GameResult>(Model.GameResult.NotCompleted);
    const [difficultyLevel, setDifficultyLevel] = useState<Model.DifficultyLevel>(Model.DifficultyLevel.Easy);
    const [totalLifes, setTotalLifes] = useState<number>(0);
    const [lifesLost, setLifesLost] = useState<number>(0);
    const [currentQuestion, setCurrentQuestion] = useState<Model.Question>({ leftHand: 0, rightHand: 0, answerPropositions: [] });
    const [currentAnswers, setCurrentAnswers] = useState<number[]>([]);
    const [previousQuestions, setPreviousQuestions] = useState<Model.AnsweredQuestion[]>([]);
    const [questionsToAnswer, setQuestionsToAnswer] = useState<number>(0);
    const isGameNotStarted = createGameStatusMemo(Model.GameStatus.NotStarted);
    const isGameInProgress = createGameStatusMemo(Model.GameStatus.InProgress);
    const isGameCompleted = createGameStatusMemo(Model.GameStatus.Completed);
    const questionsAnswered = useMemo(() => previousQuestions.length, [previousQuestions]);
    const checkAnswerObserver = useEffect(() => {
        if (currentAnswers.length == 0) {
            return;
        }

        const currentAnswer = currentAnswers[currentAnswers.length - 1];
        const params: Logic.ValidateAnswerParameters = { question: currentQuestion!, providedAnswer: currentAnswer };
        const result = Logic.GameLogic.ValidateAnswer(params);
        if (!result.isValid) {
            setLifesLost(prev => prev + 1);
        }
        else {
            StartNextQuestion(false);
        }
    }, [currentQuestion, currentAnswers]);
    const gameCompletedTriggerRequired = useMemo(() => {
        if (gameStatus != Model.GameStatus.InProgress) {
            return false;
        }

        if (lifesLost >= totalLifes) {
            return true;
        }

        if (questionsAnswered >= questionsToAnswer) {
            return true;
        }

        return false;
    }, [lifesLost, totalLifes, questionsAnswered, questionsToAnswer]);

    const gameCompletedHandler = useMemo(() => {
        if (!gameCompletedTriggerRequired) {
            return;
        }

        const params: Logic.CheckGameCompletedParameters = { difficultyLevel: difficultyLevel, lifesLost: lifesLost, questionsAsked: questionsAnswered };
        const result = Logic.GameLogic.CheckGameCompleted(params);
        if (result.isCompleted) {
            setGameStatus(Model.GameStatus.Completed);
            setGameResult(result.result);
        }
        console.log('GAME IS DONE');
    }, [gameCompletedTriggerRequired]);

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
                    answerPropositions: currentQuestion.answerPropositions,
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
            old.answerPropositions = selectQuestionResult.nextQuestion.answerPropositions;
            return old;
        });
        setCurrentAnswers([]);
    }

    function onAnswerAccepted(value: number) {
        setCurrentAnswers(prev => [...prev, value]);
    }

    function createGameStatusMemo(status: Model.GameStatus) {
        return useMemo(() => gameStatus == status, [gameStatus]);
    }

    return (
        <div>
            {isGameNotStarted ? (<GameSettingsComponent difficultyLevel={difficultyLevel} setDifficultyLevel={setDifficultyLevel} onStartGameRequested={onStartGameRequested} />) : null}
            {isGameInProgress ?
                (
                    <div className="game_board">
                        <LifesComponent lifesLost={lifesLost} lifesAvailable={totalLifes} />
                        <QuestionComponent LeftFactor={currentQuestion.leftHand} RightFactor={currentQuestion.rightHand} Answers={currentQuestion.answerPropositions} OnAnswerAcceptedNotification={onAnswerAccepted} />
                    </div>
                ) : null}
            {isGameCompleted ? (<ResultsComponent answeredQuestions={previousQuestions} />) : null}
        </div>
    )
}