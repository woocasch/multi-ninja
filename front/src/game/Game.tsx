import { ChangeEvent, useState } from "react";

interface Question {
    LeftHandSide: number;
    RightHandSide: number;
    ExpectedResult: number;
    ProvidedResult: number | null;
}

export default function Game() {
    const [questions, setQuestions] = useState<Question[]>([]);
    const [currentQuestion, setCurrentQuestion] = useState<Question>(GenerateQuestion());
    const [answer, setAnswer] = useState('');
    const [temp, setTemp] = useState(0);

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
        questions.push(currentQuestion);
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

        currentQuestion.ProvidedResult = result;
        SwitchQuestion();
        setAnswer('');
    }

    return (
        <div>
            <div>
                <span className="leftHand">{currentQuestion?.LeftHandSide}</span>
                <span className="multiplicationSymbol">*</span>
                <span className="rightHand">{currentQuestion?.RightHandSide}</span>
                <span className="equalsSymbol">=</span>
                <input type="text" value={answer} onChange={handleAnswerChange} />
                <button onClick={handleAcceptClick}>Accept</button>
                <ul>
                    {questions.map((q, i) => (
                        <li>
                            {i + 1}. {q.LeftHandSide} * {q.RightHandSide} = {q.ProvidedResult} (expected: {q.ExpectedResult})
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
}