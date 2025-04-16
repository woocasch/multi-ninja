import * as QuestionsProvider from './questions_provider';
import * as Model from './types';

export interface GenerateQuestionsParameters {
  minResult: number;
  maxResult: number;
  questionMapper: (q: Model.Question) => Model.Question;
}

export interface GenerateQuestionsResult {
  questions: Model.Question[];
}

export interface Combination {
  leftHand: number;
  rightHand: number;
  result: number;
}

export interface IQuestionsGeneratorService {
  GenerateQuestions(
    params: GenerateQuestionsParameters,
  ): GenerateQuestionsResult;
}

export class QuestionsGeneratorService implements IQuestionsGeneratorService {
  private questionsProvider: QuestionsProvider.IQuestionsProviderService;
  constructor(questionsProvider: QuestionsProvider.IQuestionsProviderService) {
    this.questionsProvider = questionsProvider;
  }
  public GenerateQuestions(
    params: GenerateQuestionsParameters,
  ): GenerateQuestionsResult {
    const questions: Model.Question[] = this.questionsProvider
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
    return {
      questions: questions,
    };
  }
}

export const QuestionsGenerator: IQuestionsGeneratorService =
  new QuestionsGeneratorService(QuestionsProvider.QuestionsProvider);
