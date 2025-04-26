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

export class CommonGameLogicService {
  private symbol: string;
  private questionMapper: (source: Model.Question) => Model.Question;
  private operation: (left: number, right: number) => number;

  constructor(
    symbol: string,
    questionMapper: (source: Model.Question) => Model.Question,
    operation: (left: number, right: number) => number,
  ) {
    this.symbol = symbol;
    this.questionMapper = questionMapper;
    this.operation = operation;
  }

  get Symbol(): string {
    return this.symbol;
  }

  get Operation(): (left: number, right: number) => number {
    return this.operation;
  }

  public StartGame(params: StartGameParameters): StartGameResult {
    const settings =
      DifficultyLevels.DifficultyLevels.GetDifficultyLevelSettings(
        params.difficultyLevel,
      );
    const initializeParams: QuestionGeneration.InitializeParameters = {
      minResult: settings.minResult,
      maxResult: settings.maxResult,
      questionMapper: this.questionMapper,
    };
    QuestionGeneration.QuestionsGenerator.Initialize(initializeParams);
    const result: StartGameResult = {
      totalLifes: settings.totalLifes,
      questionsToAnswer: settings.questionsToAnswer,
    };

    return result;
  }

  public SelectQuestion(
    params: SelectQuestionParameters,
  ): SelectQuestionResult {
    const getQuestionParams: QuestionGeneration.GetQuestionParameters = {
      previousQuestions: params.previousQuestions,
    };
    const getQuestionResult =
      QuestionGeneration.QuestionsGenerator.GetQuestion(getQuestionParams);
    const selectedQuestion = getQuestionResult.question;
    this.AddAnswers(selectedQuestion);
    return {
      nextQuestion: selectedQuestion,
    };
  }

  public ValidateAnswer(
    params: ValidateAnswerParameters,
  ): ValidateAnswerResult {
    const isCorrect =
      this.Operation(params.question.leftHand, params.question.rightHand) ==
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
    const correctAnswer = this.operation(question.leftHand, question.rightHand);
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
