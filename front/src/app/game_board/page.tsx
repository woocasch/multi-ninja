"use client"
import { ChangeEvent, useState } from "react";
import { DifficultyLevel, GameManager } from "./game_manager";
import { Localizations, StaticTexts } from "./localizations";

interface Question {
    LeftHandSide: number;
    RightHandSide: number;
    ExpectedResult: number;
    ProvidedResult: number | null;
}

export default function Home() {
    const [questions, setQuestions] = useState<Question[]>([]);
    const [currentQuestion, setCurrentQuestion] = useState<Question | null>(null);
    const [answer, setAnswer] = useState('');
    const [temp, setTemp] = useState(0);
    const [isGameStarted, setIsGameStarted] = useState<boolean>(false);
    const [selectedDifficultyLevel, setSelectedDifficultyLevel] = useState<DifficultyLevel>(DifficultyLevel.Easy);

    function CheckNotRepeated(leftHand: number, rightHand: number) {
        for (let i = 0; i < questions.length; i++) {
            const current = questions[i];
            if (current.LeftHandSide != leftHand && current.RightHandSide != leftHand) {
                continue;
            }

            if (current.LeftHandSide != rightHand && current.RightHandSide != rightHand) {
                continue;
            }

            return false;
        }
    }

    function GenerateQuestion(): Question {
        const leftHand = Math.ceil(Math.random() * 10);
        const rightHand = Math.ceil(Math.random() * 10);
        if (CheckNotRepeated(leftHand, rightHand)) {
            return GenerateQuestion();
        }

        return {
            LeftHandSide: leftHand,
            RightHandSide: rightHand,
            ExpectedResult: leftHand * rightHand,
            ProvidedResult: null,
        };
    }

    function SwitchQuestion() {
        if (!!currentQuestion) {
            questions.push(currentQuestion);
        }

        setCurrentQuestion(GenerateQuestion());
        setQuestions(questions);
    }

    function handleAnswerChange(e: ChangeEvent<HTMLInputElement>) {
        setAnswer(e.target.value);
    }

    function handleAcceptClick() {
        const result = parseInt(answer);
        if (isNaN(result)) {
            setAnswer('');
            return;
        }

        currentQuestion!.ProvidedResult = result;
        SwitchQuestion();
        setAnswer('');
    }

    function handleStartGame() {
        GameManager.SelectDifficultyLevel(selectedDifficultyLevel);
        SwitchQuestion();
        setIsGameStarted(GameManager.StartGame());
    }

    return (
        <div>
            <div>
                <select>
                    {GameManager.GetAvailableDifficultyLevels().map(l => (
                        <option value={l} selected={l == selectedDifficultyLevel} key={l}>{Localizations.GetDifficultyLevelText(l)}</option>
                    ))}
                </select>
                <button onClick={handleStartGame}>{Localizations.TranslateStaticText(StaticTexts.BtnStartGame_Text)}</button>
            </div>
            {isGameStarted ?
                (<div>
                    <span className="leftHand">{currentQuestion?.LeftHandSide}</span>
                    <span className="multiplicationSymbol">*</span>
                    <span className="rightHand">{currentQuestion?.RightHandSide}</span>
                    <span className="equalsSymbol">=</span>
                    <input type="text" value={answer} onChange={handleAnswerChange} />
                    <button onClick={handleAcceptClick}>{Localizations.TranslateStaticText(StaticTexts.BtnAcceptRespose_Text)}</button>
                    <ul>
                        {questions.map((q, i) => (
                            <li key={i}>
                                {i + 1}. {q.LeftHandSide} * {q.RightHandSide} = {q.ProvidedResult} (expected: {q.ExpectedResult})
                            </li>
                        ))}
                    </ul>
                </div>) : null}
        </div>
    )
}