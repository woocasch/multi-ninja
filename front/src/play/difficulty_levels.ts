import { DifficultyLevel } from "./types";

export interface DifficultyLevelDetails {
    level: DifficultyLevel;
    displayName: string;
}

export interface DifficultyLevelSettings {
    totalLifes: number;
    questionsToAnswer: number;
    minResult: number;
    maxResult: number;
    pointsEvolution: number[];
}

type Dictionary<T extends string | symbol | number, U> = {
    [K in T]: U;
};

const difficultyLevels: Dictionary<DifficultyLevel, DifficultyLevelSettings> = {
    [DifficultyLevel.None]: { minResult: -1, maxResult: -1, questionsToAnswer: -1, totalLifes: -1, pointsEvolution: [] },
    [DifficultyLevel.Easy]: { minResult: 1, maxResult: 30, questionsToAnswer: 10, totalLifes: 5, pointsEvolution: [5, 3, 1] },
    [DifficultyLevel.Medium]: { minResult: 1, maxResult: 60, questionsToAnswer: 15, totalLifes: 4, pointsEvolution: [5, 3, 0] },
    [DifficultyLevel.Hard]: { minResult: 1, maxResult: 100, questionsToAnswer: 20, totalLifes: 3, pointsEvolution: [5, 0] },
}

class DifficultyLevelsService {
    public GetDifficultyLevels(): DifficultyLevelDetails[] {
        return [
            { level: DifficultyLevel.Easy, displayName: 'Łatwy' },
            { level: DifficultyLevel.Medium, displayName: 'Średni', },
            { level: DifficultyLevel.Hard, displayName: 'Trudny' },
        ]
    }

    public GetDifficultyLevelSettings(level: DifficultyLevel): DifficultyLevelSettings {
        if (level == DifficultyLevel.None) {
            throw new Error('No difficulty level selected');
        }

        return difficultyLevels[level];
    }
}

export const DifficultyLevels: DifficultyLevelsService = new DifficultyLevelsService();