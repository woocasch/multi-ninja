export interface AnsweredQuestion {
    LeftFactor: number;
    RightFactor: number;
    ExpectedAnswer: number;
    ProvidedAnswers: number[];
}

export class GameState {
    private _questionsAnswered: AnsweredQuestion[] = [];

    private _lifesLost: number = 0;

    public AddAnsweredQuestion(
        leftFactor: number,
        rightFactor: number,
        answersProvided: number[]) {
        const expectedAnswer = leftFactor * rightFactor;
        for (let i = 0; i < answersProvided.length; i++) {
            if (answersProvided[i] != expectedAnswer) {
                this.LooseLife();
            }
        }

        const question: AnsweredQuestion = {
            LeftFactor: leftFactor,
            RightFactor: rightFactor,
            ExpectedAnswer: expectedAnswer,
            ProvidedAnswers: answersProvided,
        };
        this._questionsAnswered.push(question);
    }

    public GetAllQuestions(): AnsweredQuestion[] {
        return this._questionsAnswered;
    }

    public GetLifesLost(): number {
        return this._lifesLost;
    }

    private LooseLife() {
        this._lifesLost++;
    }
}