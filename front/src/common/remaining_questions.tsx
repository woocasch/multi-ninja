import React, { useEffect, useMemo, useState } from 'react';
import './remaining_questions.css';

export interface Properties {
  answeredQuestions: number;
  totalQuestions: number;
}

export default function RemainingQuestionsComponent(props: Properties) {
  const [containerClass, setContainerClass] = useState<string>('questions-display');

  const questionsRemaining = useMemo(() => {
    return props.totalQuestions - props.answeredQuestions;
  }, [props.totalQuestions, props.answeredQuestions]);

  const animationTrigger = useEffect(() => {
    if (props.answeredQuestions == 0) {
      return;
    }

    setContainerClass('questions-display question-switched');
    setTimeout(() => setContainerClass('questions-display'), 1000);
  }, [questionsRemaining]);

  return <div className={containerClass}>{questionsRemaining}</div>;
}
