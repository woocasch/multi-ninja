import React, { MouseEvent, useMemo } from 'react';
import './question.scss';

export enum DisplayMode {
    None = 0,
    Answer = 1,
    Review = 2,
}

export interface Properties {
    className?: string;
    leftHand: number;
    symbol: string;
    rightHand: number;
    availableAnswers: number[];
    mode: DisplayMode;
    wrongAnswers?: number[];
    correctAnswer?: number;
    onAnswerAccepted?: (answer: number) => void;
}

export default function QuestionComponent(props: Properties) {
    const resultingClasses = useMemo(() =>{
        if (!!props.className) {
            return `question ${props.className}`;
        }

        return 'question';
    }, [props.className]);
    function onAnswerSelected(e: MouseEvent) {
        const selectedAnswer = e.currentTarget.textContent!;
        const parsed = parseInt(selectedAnswer);
        if (isNaN(parsed)) {
            return;
        }

        if (!!props.onAnswerAccepted) {
            props.onAnswerAccepted(parsed);
        }
    }

    return (
        <div className='question'>
            <div className='operation'>
                {props.leftHand} {props.symbol} {props.rightHand}
            </div>
            <div className='answers'>
                {props.availableAnswers.map((v, i) => (
                    <div key={i}>
                        <button onClick={onAnswerSelected}>
                            {v}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    )
}