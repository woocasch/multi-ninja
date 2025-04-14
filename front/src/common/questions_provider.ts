export interface Entry {
  leftHand: number;
  rightHand: number;
  product: number;
}

export interface IQuestionsProviderService {
  getQuestionsInRange(minResult: number, maxResult: number): Entry[];
}

export class QuestionsProviderService implements IQuestionsProviderService {
  private entries: Entry[] = [];

  constructor() {
    for (let i = 1; i <= 10; i++) {
      for (let j = 1; j <= 10; j++) {
        const entry: Entry = {
          leftHand: i,
          rightHand: j,
          product: i * j,
        };
        this.entries.push(entry);
      }
    }
  }

  public getQuestionsInRange(minResult: number, maxResult: number): Entry[] {
    return this.entries.filter(
      (v) => v.product >= minResult && v.product <= maxResult,
    );
  }
}

export const QuestionsProvider: IQuestionsProviderService =
  new QuestionsProviderService();
