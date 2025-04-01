import React from 'react';
import './results.css';
import * as Model from './types';

export interface Properties {
    answeredQuestions: Model.AnsweredQuestion[];
}

export default function ResultsComponent(props: Properties) {
    function providedAnswerClassName(expectedAnswer: number, currentAnswer: number) {
        return expectedAnswer == currentAnswer ? 'correct' : 'invalid';
    }

    return (
        <div>
            <ul>
                {props.answeredQuestions.map((q, i) => (
                    <li key={i}>
                        <p>Zadanie {i + 1}: {q.question.leftHand} * {q.question.rightHand}</p>
                        <p>Poprawna odpowiedź: {q.expectedAnswer}</p>
                        <p>Twoje odpowiedzi:</p>
                            <ul>
                                {q.providedAnswers.map((a, j) => (<li key={`${i}_${j}`} className={providedAnswerClassName(q.expectedAnswer, a)}>{a}</li>))}
                            </ul>
                    </li>
                ))}
            </ul>
        </div>
    )
}