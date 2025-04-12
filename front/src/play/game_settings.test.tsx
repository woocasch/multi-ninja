import { describe, expect, it } from 'vitest';
import { render, screen } from '@testing-library/react';
import GameSettingsComponent from './game_settings';
import * as Model from './types';
import React from 'react';

describe('GameSettingsComponent', () => {
    let difficultyLevel: Model.DifficultyLevel= Model.DifficultyLevel.Easy;
    const setDifficultyLevel:(difLev: Model.DifficultyLevel) => void = (l) => difficultyLevel = l;
    const onStartRequested: () => void = () => {};
    it('should should have difficulty level selector', () => {
        const { container } = render(<GameSettingsComponent difficultyLevel={Model.DifficultyLevel.Easy} setDifficultyLevel={setDifficultyLevel} onStartGameRequested={onStartRequested} />);
        const levelSelector: HTMLSelectElement = container.querySelector("select[id='difficultyLevel'")!;
        expect(levelSelector.options.length).toBe(3);
    })
})

// test('renders Hello component', () => {
//   render(<Hello />);
//   expect(screen.getByText('Hello, Vitest!')).toBeInTheDocument();
// });
