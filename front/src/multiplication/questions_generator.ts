import * as QuestionsProvider from '../common/questions_provider';
import * as Model from '../common/types';

export interface GenerateQuestionsParameters {
  minResult: number;
  maxResult: number;
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
  GenerateQuestions(params: GenerateQuestionsParameters): GenerateQuestionsResult
}

export class QuestionsGeneratorService implements IQuestionsGeneratorService {
  private questionsProvider: QuestionsProvider.IQuestionsProviderService;
  constructor(questionsProvider: QuestionsProvider.IQuestionsProviderService) {
    this.questionsProvider = questionsProvider;
  }
  public GenerateQuestions(
    params: GenerateQuestionsParameters,
  ): GenerateQuestionsResult {
    const questions: Model.Question[] = this.questionsProvider.getQuestionsInRange(
      params.minResult,
      params.maxResult)
      .map(
        (c) =>
          <Model.Question>{
            leftHand: c.leftHand,
            rightHand: c.rightHand,
            answerPropositions: [],
          },
      );
    return {
      questions: questions,
    };
  }
}

export const QuestionsGenerator: IQuestionsGeneratorService =
  new QuestionsGeneratorService(QuestionsProvider.QuestionsProvider);
