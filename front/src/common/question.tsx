import React, { MouseEvent, useMemo } from 'react';
import './question.scss';
import * as Model from './types';

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
  providedAnswers?: number[];
  onAnswerAccepted?: (answer: number) => void;
}

export default function QuestionComponent(props: Properties) {
  const resultingClasses = useMemo(() => {
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

  function responseClassNames(answer: number) {
    if (props.mode == DisplayMode.Answer) {
      return '';
    }

    if (!props.providedAnswers) {
      return '';
    }

    if (props.providedAnswers.indexOf(answer) != -1) {
      if (answer == props.correctAnswer) {
        return 'correct';
      } else {
        return 'invalid';
      }
    }

    return '';
  }

  return (
    <div className="question">
      <div className="operation">
        {props.leftHand} {props.symbol} {props.rightHand}
      </div>
      <div className="answers">
        {props.availableAnswers.map((v, i) => (
          <div key={i}>
            <button
              onClick={onAnswerSelected}
              className={responseClassNames(v)}
            >
              {v}
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
