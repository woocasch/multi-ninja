import * as Model from './types';
import * as DifficultyLevels from './difficulty_levels';
import * as QuestionGeneration from './questions_generator';

export interface StartGameParameters {
  difficultyLevel: Model.DifficultyLevel;
}

export interface StartGameResult {
  totalLifes: number;
  questionsToAnswer: number;
}

export interface SelectQuestionParameters {
  difficultyLevel: Model.DifficultyLevel;
  previousQuestions: Model.AnsweredQuestion[];
}

export interface SelectQuestionResult {
  nextQuestion: Model.Question;
}

export interface ValidateAnswerParameters {
  question: Model.Question;
  providedAnswer: number;
}

export interface ValidateAnswerResult {
  isValid: boolean;
}

export interface CheckGameCompletedParameters {
  difficultyLevel: Model.DifficultyLevel;
  lifesLost: number;
  questionsAsked: number;
}

export interface CheckGameCompletedResult {
  isCompleted: boolean;
  result: Model.GameResult;
}

export interface CalculateGameScoreParameters {
  difficultyLevel: Model.DifficultyLevel;
  questions: Model.AnsweredQuestion[];
}

export interface CalculateGameScoreResult {
  isPerfect: boolean;
  points: number;
}

class GameLogicService {
  public StartGame(params: StartGameParameters): StartGameResult {
    const settings =
      DifficultyLevels.DifficultyLevels.GetDifficultyLevelSettings(
        params.difficultyLevel,
      );
    const result: StartGameResult = {
      totalLifes: settings.totalLifes,
      questionsToAnswer: settings.questionsToAnswer,
    };

    return result;
  }

  public SelectQuestion(
    params: SelectQuestionParameters,
  ): SelectQuestionResult {
    const settings =
      DifficultyLevels.DifficultyLevels.GetDifficultyLevelSettings(
        params.difficultyLevel,
      );
    const generateQuestionsParams: QuestionGeneration.GenerateQuestionsParameters =
      {
        minResult: settings.minResult,
        maxResult: settings.maxResult,
      };
    const questions = QuestionGeneration.QuestionsGenerator.GenerateQuestions(
      generateQuestionsParams,
    );
    let index = Math.floor(Math.random() * questions.questions.length);
    let selectedQuestion = questions.questions[index];
    let remainingRetries = 3;
    while (
      this.IsQuestionRepeated(
        selectedQuestion,
        params.previousQuestions.map((q) => q.question),
      ) &&
      remainingRetries > 0
    ) {
      index = Math.floor(Math.random() * questions.questions.length);
      selectedQuestion = questions.questions[index];
    }

    this.AddAnswers(selectedQuestion);
    return {
      nextQuestion: selectedQuestion,
    };
  }

  public ValidateAnswer(
    params: ValidateAnswerParameters,
  ): ValidateAnswerResult {
    const isCorrect =
      params.question.leftHand * params.question.rightHand ==
      params.providedAnswer;
    return {
      isValid: isCorrect,
    };
  }

  public CheckGameCompleted(
    params: CheckGameCompletedParameters,
  ): CheckGameCompletedResult {
    const settings =
      DifficultyLevels.DifficultyLevels.GetDifficultyLevelSettings(
        params.difficultyLevel,
      );
    if (params.lifesLost >= settings.totalLifes) {
      return {
        isCompleted: true,
        result: Model.GameResult.Lost,
      };
    }

    if (params.questionsAsked >= settings.questionsToAnswer) {
      return {
        isCompleted: true,
        result: Model.GameResult.Won,
      };
    }

    return {
      isCompleted: false,
      result: Model.GameResult.NotCompleted,
    };
  }

  public CalculateGameScore(
    params: CalculateGameScoreParameters,
  ): CalculateGameScoreResult {
    let isPerfect: boolean = true;
    let score: number = 0;
    for (let i = 0; i < params.questions.length; i++) {
      const currentQuestion = params.questions[i];
      const currentQuestionScore = this.CalculateQuestionScore(
        params.difficultyLevel,
        currentQuestion,
      );
      if (!currentQuestionScore.isPerfect) {
        isPerfect = false;
      }

      score += currentQuestionScore.points;
    }
    return {
      isPerfect: isPerfect,
      points: score,
    };
  }

  private AddAnswers(question: Model.Question) {
    const offset_length = 10;
    const max = 100;
    const correctAnswer = question.leftHand * question.rightHand;
    let result = [correctAnswer];
    let offset_adjustement = 0;
    if (correctAnswer <= offset_length / 2) {
      offset_adjustement = offset_length / 2 - correctAnswer + 1;
    }

    if (correctAnswer > max - offset_length / 2) {
      offset_adjustement = offset_length / 2 - (max - correctAnswer);
    }

    function generateWrongAnswer() {
      let offset = Math.floor(Math.random() * 10) - 5;
      offset += offset_adjustement;
      return offset === 0 ? correctAnswer + 1 : correctAnswer + offset;
    }

    let testCount = 0;
    while (result.length < 6) {
      const wrongAnswer = generateWrongAnswer();
      if (
        wrongAnswer !== correctAnswer &&
        !result.includes(wrongAnswer) &&
        wrongAnswer > 0
      ) {
        result.push(wrongAnswer);
      }

      testCount++;
      if (testCount > 20) {
        throw new Error('Problem generating answers for question');
      }
    }

    result = result.sort(() => Math.random() - 0.5);
    question.answerPropositions = result;
  }

  private IsQuestionRepeated(
    selected: Model.Question,
    previousQuestions: Model.Question[],
  ): boolean {
    for (const current of previousQuestions) {
      if (
        current.leftHand == selected.leftHand &&
        current.rightHand == selected.rightHand
      ) {
        return true;
      }

      if (
        current.leftHand == selected.rightHand &&
        current.rightHand == selected.leftHand
      ) {
        return true;
      }
    }

    return false;
  }

  private CalculateQuestionScore(
    difficultyLevel: Model.DifficultyLevel,
    question: Model.AnsweredQuestion,
  ): CalculateGameScoreResult {
    let isPerfect: boolean = false;
    if (
      question.providedAnswers.length == 1 &&
      question.providedAnswers[0] == question.expectedAnswer
    ) {
      isPerfect = true;
    }

    const settings =
      DifficultyLevels.DifficultyLevels.GetDifficultyLevelSettings(
        difficultyLevel,
      );
    const correctAnswerIndex = Math.min(
      question.providedAnswers.indexOf(question.expectedAnswer),
      settings.pointsEvolution.length - 1,
    );
    const points = settings.pointsEvolution[correctAnswerIndex];
    return {
      isPerfect: isPerfect,
      points: points,
    };
  }
}

export const GameLogic: GameLogicService = new GameLogicService();
