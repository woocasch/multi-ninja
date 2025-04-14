import React, { useEffect, useMemo, useState } from 'react';
import './multiplication.scss';
import * as Model from '../common/types';
import * as Logic from './game_logic';
import GameSettingsComponent from './game_settings';
import QuestionComponent, { DisplayMode } from '../common/question';
import LifesComponent from '../common/lifes';
import ResultsComponent from './results';
import RemainingQuestionsComponent from '../common/remaining_questions';
import FlawlessVictoryComponent from '../common/flawless-victory';

export default function MultiplicationComponent() {
  const [gameStatus, setGameStatus] = useState<Model.GameStatus>(
    Model.GameStatus.NotStarted,
  );
  const [gameResult, setGameResult] = useState<Model.GameResult>(
    Model.GameResult.NotCompleted,
  );
  const [difficultyLevel, setDifficultyLevel] = useState<Model.DifficultyLevel>(
    Model.DifficultyLevel.Easy,
  );
  const [totalLifes, setTotalLifes] = useState<number>(0);
  const [lifesLost, setLifesLost] = useState<number>(0);
  const [isPerfectGame, setIsPerfectGame] = useState<boolean>(false);
  const [pointsScored, setPointsScored] = useState<number>(0);
  const [currentQuestion, setCurrentQuestion] = useState<Model.Question>({
    leftHand: 0,
    rightHand: 0,
    answerPropositions: [],
  });
  const [currentAnswers, setCurrentAnswers] = useState<number[]>([]);
  const [previousQuestions, setPreviousQuestions] = useState<
    Model.AnsweredQuestion[]
  >([]);
  const [questionsToAnswer, setQuestionsToAnswer] = useState<number>(0);
  const isGameNotStarted = createGameStatusMemo(Model.GameStatus.NotStarted);
  const isGameInProgress = createGameStatusMemo(Model.GameStatus.InProgress);
  const isGameCompleted = createGameStatusMemo(Model.GameStatus.Completed);
  const isPerfectGameCompleted = useMemo(() => {
    return isGameCompleted && isPerfectGame;
  }, [isGameCompleted, isPerfectGame]);
  const isNotPerfectGameCompleted = useMemo(() => {
    return isGameCompleted && !isPerfectGame;
  }, [isGameCompleted, isPerfectGame]);
  const checkAnswerObserver = useEffect(() => {
    if (currentAnswers.length == 0) {
      return;
    }

    const currentAnswer = currentAnswers[currentAnswers.length - 1];
    const params: Logic.ValidateAnswerParameters = {
      question: currentQuestion!,
      providedAnswer: currentAnswer,
    };
    const result = Logic.GameLogic.ValidateAnswer(params);
    if (!result.isValid) {
      setLifesLost((prev) => prev + 1);
    } else {
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

    if (previousQuestions.length >= questionsToAnswer) {
      return true;
    }

    return false;
  }, [lifesLost, totalLifes, previousQuestions, questionsToAnswer]);

  const gameCompletedHandler = useMemo(() => {
    if (!gameCompletedTriggerRequired) {
      return;
    }

    const params: Logic.CheckGameCompletedParameters = {
      difficultyLevel: difficultyLevel,
      lifesLost: lifesLost,
      questionsAsked: previousQuestions.length,
    };
    const result = Logic.GameLogic.CheckGameCompleted(params);
    if (result.isCompleted) {
      const calculateScoreParams: Logic.CalculateGameScoreParameters = {
        difficultyLevel: difficultyLevel,
        questions: previousQuestions,
      };
      setGameStatus(Model.GameStatus.Completed);
      setGameResult(result.result);
      const calculateScoreResult =
        Logic.GameLogic.CalculateGameScore(calculateScoreParams);
      setIsPerfectGame(calculateScoreResult.isPerfect);
      setPointsScored(calculateScoreResult.points);
    }
  }, [gameCompletedTriggerRequired]);

  function onStartGameRequested() {
    const startGameParameters: Logic.StartGameParameters = {
      difficultyLevel: difficultyLevel,
    };
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
      setPreviousQuestions((old) => [...old, answerToStore]);
    }
    const selectQuestionParams: Logic.SelectQuestionParameters = {
      difficultyLevel: difficultyLevel,
      previousQuestions: previousQuestions,
    };
    const selectQuestionResult =
      Logic.GameLogic.SelectQuestion(selectQuestionParams);
    setCurrentQuestion((old) => {
      old.leftHand = selectQuestionResult.nextQuestion.leftHand;
      old.rightHand = selectQuestionResult.nextQuestion.rightHand;
      old.answerPropositions =
        selectQuestionResult.nextQuestion.answerPropositions;
      return old;
    });
    setCurrentAnswers([]);
  }

  function onAnswerAccepted(value: number) {
    setCurrentAnswers((prev) => [...prev, value]);
  }

  function onNewGameRequested() {
    ResetToStartConfiguration();
    onStartGameRequested();
  }

  function onGameConfigurationRequested() {
    ResetToStartConfiguration();
    setGameStatus(Model.GameStatus.NotStarted);
  }

  function ResetToStartConfiguration() {
    setGameResult(Model.GameResult.NotCompleted);
    setLifesLost(0);
    setIsPerfectGame(false);
    setPointsScored(0);
    setCurrentQuestion((q) => {
      q.leftHand = 0;
      q.rightHand = 0;
      q.answerPropositions.length = 0;
      return q;
    });
    setCurrentAnswers((a) => {
      a.length = 0;
      return a;
    });
    setPreviousQuestions((q) => {
      q.length = 0;
      return q;
    });
    setGameStatus(Model.GameStatus.None);
  }

  function createGameStatusMemo(status: Model.GameStatus) {
    return useMemo(() => gameStatus == status, [gameStatus]);
  }

  return (
    <div>
      {isGameNotStarted ? (
        <GameSettingsComponent
          difficultyLevel={difficultyLevel}
          setDifficultyLevel={setDifficultyLevel}
          onStartGameRequested={onStartGameRequested}
        />
      ) : null}
      {isGameInProgress ? (
        <div className="game-board">
          <LifesComponent lifesLost={lifesLost} lifesAvailable={totalLifes} />
          <RemainingQuestionsComponent
            answeredQuestions={previousQuestions.length}
            totalQuestions={questionsToAnswer}
          />
          <QuestionComponent
            leftHand={currentQuestion.leftHand}
            symbol='*'
            rightHand={currentQuestion.rightHand}
            availableAnswers={currentQuestion.answerPropositions}
            mode={DisplayMode.Answer}
            onAnswerAccepted={onAnswerAccepted}
          />
        </div>
      ) : null}
      {isNotPerfectGameCompleted ? (
        <ResultsComponent answeredQuestions={previousQuestions} />
      ) : null}
      {isPerfectGameCompleted ? (
        <FlawlessVictoryComponent
          pointsScored={pointsScored}
          onNewGameRequested={onNewGameRequested}
          onGameConfigurationRequested={onGameConfigurationRequested}
        />
      ) : null}
    </div>
  );
}
