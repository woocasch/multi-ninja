import React, { useMemo, useState } from 'react';
import './results.css';
import * as Model from './types';

export interface Properties {
  answeredQuestions: Model.AnsweredQuestion[];
}

export default function ResultsComponent(props: Properties) {
  const answersToShow = useMemo(
    () => props.answeredQuestions.filter((v) => v.providedAnswers.length > 0),
    [props.answeredQuestions],
  );
  const totalAnswers = useMemo(() => answersToShow.length, [answersToShow]);
  const [currentIndex, setCurrentIndex] = useState<number>(0);
  const currentItem = useMemo(() => {
    return answersToShow[currentIndex];
  }, [currentIndex, answersToShow]);
  const hasPrevious = useMemo(() => currentIndex != 0, [currentIndex]);
  const hasNext = useMemo(
    () => currentIndex < totalAnswers - 1,
    [currentIndex, totalAnswers],
  );

  function providedAnswerClassName(
    expectedAnswer: number,
    currentAnswer: number,
  ) {
    return expectedAnswer == currentAnswer ? 'correct' : 'invalid';
  }

  function responseClassNames(
    answer: number,
    question: Model.AnsweredQuestion,
  ) {
    let className = '';
    if (question.providedAnswers.indexOf(answer) != -1) {
      if (answer == question.expectedAnswer) {
        className = 'correct';
      } else {
        className = 'invalid';
      }
    }

    return className;
  }

  function showNext() {
    setCurrentIndex((prev) => prev + 1);
  }

  function showPrev() {
    setCurrentIndex((prev) => prev - 1);
  }

  return (
    <div className="results">
      <div className="question">
        {currentItem.question.leftHand} * {currentItem.question.rightHand}
      </div>
      <div className="answer">
        {currentItem.question.answerPropositions?.map((v, i) => (
          <button key={i} className={responseClassNames(v, currentItem)}>
            {v}
          </button>
        ))}
      </div>
      <div className="browser">
        <button className="button" onClick={showPrev} disabled={!hasPrevious}>
          &lt;
        </button>
        {currentIndex + 1}
        <button className="button" onClick={showNext} disabled={!hasNext}>
          &gt;
        </button>
      </div>
    </div>
  );
}
