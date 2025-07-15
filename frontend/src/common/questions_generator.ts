import * as QuestionsProvider from './questions_provider';
import * as Model from './types';

export interface InitializeParameters {
  minResult: number;
  maxResult: number;
  questionMapper: (q: Model.Question) => Model.Question;
}

export interface GetQuestionParameters {}

export interface GetQuestionResult {
  question: Model.Question;
}

export interface Combination {
  leftHand: number;
  rightHand: number;
  result: number;
}

export interface IQuestionsGeneratorService {
  Initialize(params: InitializeParameters): void;

  GetQuestion(params: GetQuestionParameters): GetQuestionResult;
}

export class QuestionsGeneratorService implements IQuestionsGeneratorService {
  private questionsProvider: QuestionsProvider.IQuestionsProviderService;

  private availableQuestions: Model.Question[] = [];
  private generatedQuestions: Model.Question[] = [];

  constructor(questionsProvider: QuestionsProvider.IQuestionsProviderService) {
    this.questionsProvider = questionsProvider;
  }

  public Initialize(params: InitializeParameters) {
    this.availableQuestions = this.questionsProvider
      .getQuestionsInRange(params.minResult, params.maxResult)
      .map(
        (c) =>
          <Model.Question>{
            leftHand: c.leftHand,
            rightHand: c.rightHand,
            answerPropositions: [],
          },
      )
      .map((q) => params.questionMapper(q));
    this.generatedQuestions = [];
  }

  public GetQuestion(params: GetQuestionParameters): GetQuestionResult {
    let index = Math.floor(Math.random() * this.availableQuestions.length);
    let selectedQuestion = this.availableQuestions[index];
    let remainingRetries = 3;
    while (this.IsQuestionRepeated(selectedQuestion) && remainingRetries > 0) {
      index = Math.floor(Math.random() * this.availableQuestions.length);
      selectedQuestion = this.availableQuestions[index];
      remainingRetries--;
    }

    this.generatedQuestions.push(selectedQuestion);
    this.availableQuestions.splice(index, 1);
    const reversedIndex = this.availableQuestions.findIndex(
      (q) =>
        q.rightHand == selectedQuestion.leftHand &&
        q.leftHand == selectedQuestion.rightHand,
    );
    if (reversedIndex != -1) {
      this.availableQuestions.splice(reversedIndex, 1);
    }

    return {
      question: selectedQuestion,
    };
  }

  private IsQuestionRepeated(selected: Model.Question): boolean {
    const lastQuestion =
      this.generatedQuestions.length > 0
        ? this.generatedQuestions[this.generatedQuestions.length - 1]
        : null;
    if (lastQuestion && selected) {
      if (
        lastQuestion.leftHand == selected.leftHand ||
        lastQuestion.rightHand == selected.leftHand
      ) {
        return true;
      }

      if (
        lastQuestion.leftHand == selected.rightHand ||
        lastQuestion.rightHand == selected.rightHand
      ) {
        return true;
      }
    }

    return false;
  }
}

export function generatorFactory(): IQuestionsGeneratorService {
  return new QuestionsGeneratorService(QuestionsProvider.QuestionsProvider);
}
