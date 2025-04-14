import React, { useMemo, useState } from 'react';
import './results.scss';
import * as Model from './types';
import QuestionComponent, { DisplayMode } from './question';

export interface Properties {
  answeredQuestions: Model.AnsweredQuestion[];
  symbol: string;
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

  function showNext() {
    setCurrentIndex((prev) => prev + 1);
  }

  function showPrev() {
    setCurrentIndex((prev) => prev - 1);
  }

  return (
    <div className="results">
      <QuestionComponent
        leftHand={currentItem.question.leftHand}
        symbol={props.symbol}
        rightHand={currentItem.question.rightHand}
        availableAnswers={currentItem.question.answerPropositions}
        mode={DisplayMode.Review}
        providedAnswers={currentItem.providedAnswers}
        correctAnswer={currentItem.expectedAnswer}
      />
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
