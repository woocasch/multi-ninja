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

class GameManagerService {
    private questions: Question[] = [];

    private selectedLevel: DifficultyLevel = DifficultyLevel.None;
    
    constructor() {
    }

    public SelectDifficultyLevel(level: DifficultyLevel) {
        this.selectedLevel = level;
    }

    public GetAvailableDifficultyLevels(): DifficultyLevel[] {
        return [DifficultyLevel.Easy, DifficultyLevel.Medium, DifficultyLevel.Hard];
    }

    public StartGame(): boolean {
        return true;
    }
}

export const GameManager: GameManagerService = new GameManagerService();