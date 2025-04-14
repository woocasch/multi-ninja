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

class QuestionsGeneratorService {
  private allQuestions: Combination[] = [];

  constructor() {
    for (let i = 1; i <= 10; i++) {
      for (let j = 1; j <= 10; j++) {
        const combination: Combination = {
          leftHand: i,
          rightHand: j,
          result: i * j,
        };
        this.allQuestions.push(combination);
      }
    }
  }
  public GenerateQuestions(
    params: GenerateQuestionsParameters,
  ): GenerateQuestionsResult {
    const questions: Model.Question[] = this.allQuestions
      .filter(
        (c) => c.result >= params.minResult && c.result <= params.maxResult,
      )
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

export const QuestionsGenerator: QuestionsGeneratorService =
  new QuestionsGeneratorService();
