import { DifficultyLevel, DifficultyLevelSettings } from "./difficulty_level_settings";
import { FactorGenerator } from "./factor_generator";

export interface Question {
    LeftFactor: number;
    RightFactor: number;
}

export interface AnsweredQuestion {
    Question: Question;
    Answer: number;
}

export interface StartGameParameters {
    level: DifficultyLevel;
    setQuestionCallback: (newQuestion: Question) => void;
    setAnswersCallback: (answeredQuestions: AnsweredQuestion[]) => void;
}

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
        this.gameSettings?.setAnswersCallback(this.previousQuestions);
        this.ProvideNewQuestion();
    }

    public StartGame(input: StartGameParameters): boolean {
        this.gameSettings = input;
        this.ProvideNewQuestion();
        return true;
    }

    public ProvideNewQuestion() {
        if (this.IsGameCompleted()) {
            return;
        }

        const newQuestion: Question = this.GenerateQuestion();
        this.currentQuestion = newQuestion;
        this.gameSettings?.setQuestionCallback(this.currentQuestion);
    }

    public IsGameCompleted(): boolean {
        const roundsToPlay = DifficultyLevelSettings.GetSettings(this.gameSettings!.level).settings.RoundsCount;
        if (this.previousQuestions.length <= roundsToPlay) {
            return false;
        }

        alert('Game complete');
        return true;
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
        const difficultyLevelSettings = DifficultyLevelSettings.GetSettings(this.gameSettings!.level);
        const combination = FactorGenerator.GetRandom(difficultyLevelSettings.settings.MinResult, difficultyLevelSettings.settings.MaxResult);
        return { left: combination.Left, right: combination.Right };
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
export { DifficultyLevel };
