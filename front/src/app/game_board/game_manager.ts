export interface Question {
    LeftFactor: number;
    RightFactor: number;
}

export interface AnsweredQuestion {
    Question: Question;
    Answer: number;
}

export enum DifficultyLevel {
    None = 0,
    Easy = 1,
    Medium = 2,
    Hard = 3,
}

export interface StartGameParameters {
    level: DifficultyLevel;
    setQuestionCallback: (newQuestion: Question) => void;
    setHistoryCallback: (answeredQuestions: AnsweredQuestion[]) => void;
}

type EnumDictionary<T extends string | symbol | number, U> = {
    [K in T]: U;
};

class GameManagerService {
    private previousQuestions: AnsweredQuestion[] = [];

    private currentQuestion: Question | null = null;

    private gameSettings: StartGameParameters | null = null;

    public GetAvailableDifficultyLevels(): DifficultyLevel[] {
        return [DifficultyLevel.Easy, DifficultyLevel.Medium, DifficultyLevel.Hard];
    }

    public SetAnswer(answer: number) {
        const answeredQuestion: AnsweredQuestion = {
            Question: this.currentQuestion!,
            Answer: answer,
        };
        this.previousQuestions.push(answeredQuestion);
        this.gameSettings?.setHistoryCallback(this.previousQuestions);
        this.ProvideNewQuestion();
    }

    public StartGame(input: StartGameParameters): boolean {
        this.gameSettings = input;
        this.ProvideNewQuestion();
        return true;
    }

    public ProvideNewQuestion() {
        const newQuestion: Question = this.GenerateQuestion();
        this.currentQuestion = newQuestion;
        this.gameSettings?.setQuestionCallback(this.currentQuestion);
    }

    public GenerateQuestion(): Question {
        let { left, right } = this.GenerateFactors();
        let attemptsRemaining = 3;
        while (this.AreFactorsTaken(left, right) && attemptsRemaining > 0) {
            ({ left, right } = this.GenerateFactors());
            attemptsRemaining--;
        }

        return { LeftFactor: left, RightFactor: right };
    }

    public GenerateFactors(): { left: number, right: number } {
        const left = this.GenerateFactor();
        const right = this.GenerateFactor();
        return { left: left, right: right };
    }

    public GenerateFactor(): number {
        const ranges: EnumDictionary<DifficultyLevel, { size: number, offset: number }> = {
            [DifficultyLevel.None]: { size: -1, offset: -1 },
            [DifficultyLevel.Easy]: { size: 5, offset: 0 },
            [DifficultyLevel.Medium]: { size: 6, offset: 1 },
            [DifficultyLevel.Hard]: { size: 8, offset: 2 },
        };
        const range = ranges[this.gameSettings!.level];
        return Math.ceil(Math.random() * range.size + range.offset);
    }

    public AreFactorsTaken(left: number, right: number): boolean {
        for (const current of this.previousQuestions.map(q => q.Question)) {
            if (current.LeftFactor == left && current.RightFactor == right) {
                return true;
            }

            if (current.LeftFactor == right && current.RightFactor == left) {
                return true;
            }
        }

        if (this.currentQuestion?.LeftFactor == left || this.currentQuestion?.RightFactor == left) {
            return true;
        }

        if (this.currentQuestion?.LeftFactor == right || this.currentQuestion?.RightFactor == right) {
            return true;
        }

        return false;
    }
}

export const GameManager: GameManagerService = new GameManagerService();