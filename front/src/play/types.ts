export enum GameStatus {
  None = 0,
  NotStarted = 1,
  InProgress = 2,
  Completed = 3,
}

export enum DifficultyLevel {
  None = 0,
  Easy = 1,
  Medium = 2,
  Hard = 3,
}

export interface Question {
  leftHand: number;
  rightHand: number;
  answerPropositions: number[];
}

export interface AnsweredQuestion {
  question: Question;
  expectedAnswer: number;
  providedAnswers: number[];
}

export enum GameResult {
  None = 0,
  NotCompleted = 1,
  Won = 2,
  Lost = 3,
}
