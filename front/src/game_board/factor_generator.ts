interface Combination {
    Left: number;
    Right: number;
    Result: number;
}

class FactorGeneratorService {
    private static readonly size: number = 10;

    private _combinations: Combination[] = [];

    constructor() {
        for (let i = 0; i < FactorGeneratorService.size; i++) {
            for (let j = 0; j < FactorGeneratorService.size; j++) {
                const combination: Combination = {
                    Left: i,
                    Right: j,
                    Result: i * j,
                };
                this._combinations.push(combination);
            }
        }
    }

    public GetRandom(minResult: number, maxResult: number) {
        const matchingCombinations = this._combinations.filter(v => v.Result >= minResult && v.Result <= maxResult);
        const length = matchingCombinations.length;
        if (length == 0) {
            throw new Error(`No combinations found with result between '${minResult}' and '${maxResult}'.`);
        }

        const index = Math.floor(Math.random() * length);
        return matchingCombinations[index];
    }
}

export const FactorGenerator: FactorGeneratorService = new FactorGeneratorService();