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
}

class GameManagerService {
    private previousQuestions: AnsweredQuestion[] = [];

    private currentQuestion: Question | null = null;

    private selectedLevel: DifficultyLevel = DifficultyLevel.None;

    private gameSettings: StartGameParameters | null = null;
    
    constructor() {
    }

    public SelectDifficultyLevel(level: DifficultyLevel) {
        this.selectedLevel = level;
    }

    public GetAvailableDifficultyLevels(): DifficultyLevel[] {
        return [DifficultyLevel.Easy, DifficultyLevel.Medium, DifficultyLevel.Hard];
    }

    public SetAnswer(answer: number) {
        const answeredQuestion: AnsweredQuestion = {
            Question: this.currentQuestion!,
            Answer: answer,
        };
        this.previousQuestions.push(answeredQuestion);
        const newQuestion: Question = { 
            LeftFactor: Math.ceil(Math.random() * 10),
            RightFactor: Math.ceil(Math.random() * 9 + 1)
        };
        this.currentQuestion = newQuestion;
        this.gameSettings?.setQuestionCallback(this.currentQuestion);
    }

    public StartGame(input: StartGameParameters): boolean {
        this.gameSettings = input;
        return true;
    }
}

export const GameManager: GameManagerService = new GameManagerService();