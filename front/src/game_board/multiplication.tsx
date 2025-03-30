import React, { ChangeEvent, useState } from "react";
import { Localizations, StaticTexts } from "./localizations";
import './multiplication.css';

export interface MultiplicationResultModel {
    AnswersProvided: number[];
}

export interface Properties {
    LeftFactor: number;
    RightFactor: number;
    RemainingErrors: number;
    ResultCallback: (result: MultiplicationResultModel) => void;
}

export default function MultiplicationView(props: Properties) {
    const [answer, setAnswer] = useState('');
    const [answersProvided, setAnswersProvided] = useState<number[]>([]);

    function onAnswerChange(e: ChangeEvent<HTMLInputElement>) {
        setAnswer(e.target.value);
    }

    function onAcceptClick() {
        const parsed = parseInt(answer);
        if (isNaN(parsed)) {
            setAnswer('');
            return;
        }

        const expected = props.LeftFactor * props.RightFactor;
        const answerValid = expected == parsed;
        if (!answerValid) {
            if (answersProvided.length + 1 >= props.RemainingErrors) {
                answersProvided.push(parsed);
                props.ResultCallback({ AnswersProvided : answersProvided });
                ClearForNextQuestion();
            }
            else {
                setAnswersProvided(prevItems => [...prevItems, parsed]);
                setAnswer('');
            }

            return;
        }

        answersProvided.push(parsed);
        props.ResultCallback({ AnswersProvided: answersProvided });
        ClearForNextQuestion();
    }

    function ClearForNextQuestion() {
        setAnswersProvided([]);
        setAnswer('');
    }

    return (
        <div className="multiplication_display">
            <div className="leftFactor">
                {props.LeftFactor}
            </div>
            <div className="multiplicationSign">
                *
            </div>
            <div className="rightFactor">
                {props.RightFactor}
            </div>
            <div className="equalsSign">
                =
            </div>
            <div className="result">
                <input type="text" value={answer} onChange={onAnswerChange} />
            </div>
            <div className="button">
                <button onClick={onAcceptClick}>{Localizations.TranslateStaticText(StaticTexts.BtnAcceptRespose_Text)}</button>
            </div>
        </div>
    )
}