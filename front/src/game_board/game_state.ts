export enum GameStatus {
    None = 0,
    NotStarted = 1,
    Started = 2,
    Completed = 3,
}

export interface AnsweredQuestion {
    LeftFactor: number;
    RightFactor: number;
    ExpectedAnswer: number;
    ProvidedAnswers: number[];
}

export class GameState {
    private _questionsAnswered: AnsweredQuestion[] = [];

    private _lifesLost: number = 0;

    private _gameStatus: GameStatus = GameStatus.NotStarted;

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

    public GetGameStatus(): GameStatus {
        return this._gameStatus;
    }

    public GetLifesLost(): number {
        return this._lifesLost;
    }

    public StartGame() {
        this._gameStatus = GameStatus.Started;
    }

    public CompleteGame() {
        this._gameStatus = GameStatus.Completed;
    }

    private LooseLife() {
        this._lifesLost++;
    }
}