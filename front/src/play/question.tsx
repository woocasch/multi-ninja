import React, { ChangeEvent, useState } from "react";
import './question.css';

export interface Properties {
    LeftFactor?: number;
    RightFactor?: number;
    Answers: number[];
    OnAnswerAcceptedNotification: (answer: number) => void;
}

export default function QuestionComponent(props: Properties) {
    const [answerText, setAnswerText] = useState<string>('');
    const [answer, setAnswer] = useState<number>(0);

    function onAnswerChange(e: ChangeEvent<HTMLInputElement>) {
        const parsed = parseInt(e.target.value);
        if (isNaN(parsed)) {
            setAnswerText('');
            setAnswer(0);
            return;
        }

        setAnswerText(e.target.value);
        setAnswer(parsed);
    }

    function onAnswerAccepted() {
        if (answerText != '') {
            props.OnAnswerAcceptedNotification(answer);
            setAnswerText('');
            setAnswer(0);
        }
    }

    return (
        <div className="task">
            <div className="question">
                {props.LeftFactor} * {props.RightFactor}
            </div>
            <div className="answer">
                <input type="text" value={answerText} onChange={onAnswerChange} />
            </div>
            <div className="answer">
                {props.Answers?.map((v, i) => (
                    <button key={i}>{v}</button>
                ))}
            </div>
            <div className="button">
                <button onClick={onAnswerAccepted}>Zatwierdź</button>
            </div>
        </div>
    );
}