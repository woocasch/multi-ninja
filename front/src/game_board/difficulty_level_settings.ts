export enum DifficultyLevel {
    None = 0,
    Easy = 1,
    Medium = 2,
    Hard = 3,
}

interface DifficultyLevelSettings {
    MinResult: number;
    MaxResult: number;
    RoundsCount: number;
    LifesCount: number;
}

interface DifficultyLevelDefinition {
    level: DifficultyLevel;
    settings: DifficultyLevelSettings;
}

const difficultyLevels: DifficultyLevelDefinition[] = [
    {
        level: DifficultyLevel.Easy,
        settings: {
            MinResult: 1,
            MaxResult: 30,
            RoundsCount: 10,
            LifesCount: 5,
        },
    },
    {
        level: DifficultyLevel.Medium,
        settings: {
            MinResult: 1,
            MaxResult: 60,
            RoundsCount: 15,
            LifesCount: 4,
        },
    },
    {
        level: DifficultyLevel.Hard,
        settings: {
            MinResult: 1,
            MaxResult: 100,
            RoundsCount: 20,
            LifesCount: 3,
        },
    }
]

class DifficultyLevelSettingsProvider {
    public GetSettings(level: DifficultyLevel) {
        const settingsIndex = difficultyLevels.findIndex(v => v.level == level);
        if (settingsIndex == -1) {
            throw new Error(`Settings for difficulty level '${level}' not found.`);
        }

        return difficultyLevels[settingsIndex];
    }
}

export const DifficultyLevelSettings = new DifficultyLevelSettingsProvider();