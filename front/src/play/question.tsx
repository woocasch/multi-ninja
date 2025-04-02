import React, { ChangeEvent, MouseEvent, useState } from "react";
import './question.css';

export interface Properties {
    LeftFactor?: number;
    RightFactor?: number;
    Answers: number[];
    OnAnswerAcceptedNotification: (answer: number) => void;
}

export default function QuestionComponent(props: Properties) {
    function onAnswerSelected(e: MouseEvent) {
        const selectedAnswer = e.currentTarget.textContent!;
        const parsed = parseInt(selectedAnswer);
        if (isNaN(parsed)) {
            return;
        }

        props.OnAnswerAcceptedNotification(parsed);
    }

    return (
        <div className="task">
            <div className="question">
                {props.LeftFactor} * {props.RightFactor}
            </div>
            <div className="answer">
                {props.Answers?.map((v, i) => (
                    <button key={i} onClick={onAnswerSelected}>{v}</button>
                ))}
            </div>
        </div>
    );
}